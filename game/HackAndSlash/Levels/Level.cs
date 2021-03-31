using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


/// <summary>
/// Largley due to the lack of consideration at the start, 
/// word "level" and "map" are used interchangeable in many locations 
/// </summary>


namespace HackAndSlash
{
    public class Level : ILevel
    {
        private const int ALL_MIGHT_DIV = 16;
        private const int ALL_MIGH_COUNT = 256;
        private const int EDGE_PRESERVE = 17 * 4;
        private const int UPDATE_DELAY = 5; 
        private const int TRANSITION_STEP_Y = 8;
        private const int TRANSITION_STEP_X = 16;
        private const int DOOR_OPEN_INDEX = 1;
        private const int DOOR_LOCKED_INDEX = 2;
        private const int DOOR_MYS_INDEX = 3;
        private const int DOOR_HOLE_INDEX = 4;

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;

        // Textures 
        public Texture2D headsUpFill; 
        public Texture2D levelOverlay;
        public Texture2D nextLevelTexture;
        public Texture2D levelTexture;   // Can be accessed to make map transitioning 
        private Texture2D blockAllMight; // Containing all blocks 

        // Transiton related 
        public int TransDir = (int)GlobalSettings.Direction.Left;
        public bool transitioning = false;
        public bool transFinsihed = false;
        public Vector2[] nextLvPos = { 
            new Vector2(-GlobalSettings.GAME_AREA_WIDTH, GlobalSettings.HEADSUP_DISPLAY ), // Go through left door 
            new Vector2(GlobalSettings.GAME_AREA_WIDTH, GlobalSettings.HEADSUP_DISPLAY ),  // Go through right door 
            new Vector2(0, -GlobalSettings.GAME_AREA_HEIGHT + GlobalSettings.HEADSUP_DISPLAY), // Go through top door 
            new Vector2(0, GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.GAME_AREA_HEIGHT) // Go through bottom door 
        }; // Position for next level 
        private Vector2 delta = new Vector2(0, 0); 
        private int timer = 0;

        // For generating maps 
        private int defaultBlockIndex; 
        private int[,] mapMatrix;
        public Map currentMapInfo { get;set; }
        private Map[] neighbors { get; set; }
        public int[] mapIndex = GlobalSettings.STRAT_UP_INDEX;

        public LevelCycling levelCycler = new LevelCycling(); 

        // Doors related 
        // Left, Right, Up, Down as in global settings 
        public bool[] transDirList = { true, false, false, false };
        private bool[] doorOpen =    { false, false, false, false };    // Highest priority 
        private bool[] doorHole=     { false, false, false, false };    // Second in command 
        private bool[] doorLocked =  { false, false, false, false };    // Lowest priority  
        private bool[] doorHiden =   { false, false, false, false };    // Does not draw 
        private Dictionary<int, int> doorDirMapping = new Dictionary<int, int>(){
            {0, 1},
            {1, 2},
            {2, 0},
            {3, 3}
            };  // Mapping the direction Enum to the image of the doors 
        private int[] iter = { 0, 1, 2, 3 }; // Minimize magic number in door iteration 

        // Misc 
        private Color defaultColor = Color.Black;
        private Color defaultTint = Color.White;


        /* ============================================================
         * ======================== Methods ===========================
         * ============================================================ */


        public Level(GraphicsDevice Graphics, SpriteBatch SB)
        {
            // Primaries 
            graphics = Graphics;
            spriteBatch = SB;
            levelCycler = new LevelCycling();
        }   

        public void FirstTimeStartUp()
        {
            currentMapInfo = levelCycler.StartUpLevel();
        }

        public void Generate()
        {
            // Error checking on non-existent doors 
            foreach (int Direction in iter)
            {
                if (!levelCycler.HasNextRoom(Direction))
                {
                    doorOpen[Direction] = false;
                    doorHole[Direction] = false;
                    doorLocked[Direction] = false;
                    doorHiden[Direction] = false;
                }
            }

            // Derivatives 
            defaultBlockIndex = currentMapInfo.DefaultBlock;
            doorOpen = currentMapInfo.OpenDoors;
            doorHiden = currentMapInfo.HiddenDoors;
            doorLocked = currentMapInfo.LockedDoors;
            mapMatrix = currentMapInfo.Arrangement;

            // Generate placeholder textures 
            headsUpFill = GenerateTexture(GlobalSettings.GAME_AREA_WIDTH, GlobalSettings.HEADSUP_DISPLAY, pixel => defaultColor);
            levelTexture = GenerateTexture(GlobalSettings.GAME_AREA_WIDTH, GlobalSettings.GAME_AREA_HEIGHT, pixel => defaultColor);

            // Read blocks texture 
            blockAllMight = SpriteFactory.Instance.getBlockAllMight();

            // Set neighbors 
            UpdateNeighbors();

            // Fill textures  
            AlterTexture();
            GenerateOverlay();
        }
        
        // Generate a texture filled with default color 
        public Texture2D GenerateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(graphics, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
                data[pixel] = paint(pixel);

            texture.SetData(data);
            return texture;
        }

        // Crop from the all might texture 
        private Texture2D getBlockByIndex(int index)
        {
            Texture2D block = GenerateTexture(GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR, pixel => defaultColor);
            int row = index / ALL_MIGHT_DIV; 
            int col = index % ALL_MIGHT_DIV;

            Rectangle SourceRectangle = new Rectangle(
                col * GlobalSettings.BASE_SCALAR,
                row * GlobalSettings.BASE_SCALAR,
                GlobalSettings.BASE_SCALAR,
                GlobalSettings.BASE_SCALAR);

            Color[] data = new Color[SourceRectangle.Width * SourceRectangle.Height];
            blockAllMight.GetData<Color>(0, SourceRectangle, data, 0, data.Length);
            block.SetData(data);

            return block;
        }

        // Overwrite the texture, add the border and tiles 
        private void AlterTexture()
        {
            Texture2D Border = SpriteFactory.Instance.GetLevelEagleBorder();
            int CountBorder = Border.Width * Border.Height;
            Color[] RawDataBorder = new Color[CountBorder];
            Border.GetData<Color>(RawDataBorder);
            levelTexture.SetData(0, new Rectangle(0, 0, Border.Width, Border.Height), RawDataBorder, 0, CountBorder);

            // Add tiles 
            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    Vector2 StartPoint = new Vector2(GlobalSettings.BORDER_OFFSET + c * GlobalSettings.BASE_SCALAR,
                        GlobalSettings.BORDER_OFFSET + r * GlobalSettings.BASE_SCALAR);

                    int TileTypeNow = (mapMatrix[r, c] >= 0 && mapMatrix[r, c] < ALL_MIGH_COUNT) ? 
                        mapMatrix[r, c] : defaultBlockIndex;
                    Texture2D TextureNow = getBlockByIndex(TileTypeNow);
                    
                    int CountNow = TextureNow.Width * TextureNow.Height;
                    Color[] RawDataNow = new Color[CountNow];
                    TextureNow.GetData<Color>(RawDataNow);

                    // Paste the data of the tiles 
                    levelTexture.SetData(0, new Rectangle(
                        (int)StartPoint.X, (int)StartPoint.Y, 
                        GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR),
                        RawDataNow, 0, CountNow);
                }
            }
            UpdateDrawDoors();
        }

        // Update the doors, used both in initialization and when new doors are being dynamically added 
        public void UpdateDrawDoors()
        {
            Texture2D doors = SpriteFactory.Instance.GetLevelEagleDoors();
            int DoorSizeUnit = GlobalSettings.BASE_SCALAR * 2;

            int HorizontalPos = GlobalSettings.GAME_AREA_WIDTH / 2 - GlobalSettings.BASE_SCALAR;
            int VerticalPos = GlobalSettings.GAME_AREA_HEIGHT / 2 - GlobalSettings.BASE_SCALAR;
            int TopPosition = 0;
            int ButtPosition = GlobalSettings.GAME_AREA_HEIGHT - GlobalSettings.BORDER_OFFSET;

            int LeftPosition = 0;
            int RightPosition = GlobalSettings.GAME_AREA_WIDTH - GlobalSettings.BORDER_OFFSET;

            int Col = 0;
            Rectangle SourceRectangle, DestRectangle = new Rectangle(0, 0, DoorSizeUnit, DoorSizeUnit); 

            
            foreach (int Dir in iter)
            {
                // Pre-launch check to pick up proper clipping area 
                Col = 0;
                if (doorLocked[Dir]) Col = DOOR_LOCKED_INDEX;
                if (doorHole[Dir])   Col = DOOR_HOLE_INDEX;
                if (doorOpen[Dir])   Col = DOOR_OPEN_INDEX;
                switch (Dir) {
                    case (int)GlobalSettings.Direction.Left:
                        DestRectangle.X = LeftPosition;
                        DestRectangle.Y = VerticalPos;
                        break;
                    case (int)GlobalSettings.Direction.Right:
                        DestRectangle.X = RightPosition;
                        DestRectangle.Y = VerticalPos;
                        break;
                    case (int)GlobalSettings.Direction.Up:
                        DestRectangle.X = HorizontalPos;
                        DestRectangle.Y = TopPosition;
                        break;
                    case (int)GlobalSettings.Direction.Down:
                        DestRectangle.X = HorizontalPos;
                        DestRectangle.Y = ButtPosition;
                        break;
                    default: break; // Not possible 
                }
                SourceRectangle = new Rectangle(
                    Col * DoorSizeUnit, doorDirMapping[Dir] * DoorSizeUnit,
                    DoorSizeUnit, DoorSizeUnit);

                // Copy 
                Color[] data = new Color[SourceRectangle.Width * SourceRectangle.Height];
                doors.GetData<Color>(0, SourceRectangle, data, 0, data.Length);

                // Paste 
                levelTexture.SetData(0, DestRectangle,
                        data, 0, data.Length);
            }
        }

        public void UpdateNeighbors()
        {
            neighbors = new Map[] {
                levelCycler.GetNextRoom((int)GlobalSettings.Direction.Left),
                levelCycler.GetNextRoom((int)GlobalSettings.Direction.Right),
                levelCycler.GetNextRoom((int)GlobalSettings.Direction.Up),
                levelCycler.GetNextRoom((int)GlobalSettings.Direction.Down)
            };

        }
        // Create a overlay texture for the layering effect when player goes through doors 
        private Texture2D GenerateOverlay()
        { 
            Texture2D TranspBlock = GenerateTexture(GlobalSettings.GAME_AREA_WIDTH - EDGE_PRESERVE*2, 
                GlobalSettings.GAME_AREA_HEIGHT - EDGE_PRESERVE*2, pixel => Color.Transparent);

            // MonoGame cannot just use assigment to copy texture 
            levelOverlay = GenerateTexture(GlobalSettings.GAME_AREA_WIDTH, GlobalSettings.GAME_AREA_HEIGHT, pixel => Color.Transparent);
            Color[] SrcData = new Color[levelTexture.Width * levelTexture.Height];
            levelTexture.GetData<Color>(SrcData);
            levelOverlay.SetData(SrcData);

            Color[] TranspData = new Color[TranspBlock.Width * TranspBlock.Height];
            TranspBlock.GetData<Color>(TranspData);
            levelOverlay.SetData(0, new Rectangle(EDGE_PRESERVE, EDGE_PRESERVE, TranspBlock.Width, TranspBlock.Height),
                TranspData, 0, TranspData.Length);

            return levelOverlay; 
        }

        /// <summary>
        /// The contract is that:
        ///     "If the player can go through, then there must be another room."
        /// </summary>
        public bool CanGoThrough(int Dir)
        {
            // Up, Bottom, Left Right 
            return (doorOpen[Dir] || doorHole[Dir]) && (neighbors[Dir] != null);
        }
        public Map NextRoom(int Dir)
        {
            return neighbors[Dir]; 
        }
        public void MoveToRoom(int Dir)
        {
            int MoveX = 0, MoveY = 0;

            MoveX = ((Dir == (int)GlobalSettings.Direction.Left) ? -1 : 0);
            MoveX = ((Dir == (int)GlobalSettings.Direction.Right) ? 1 : MoveX);

            MoveY = ((Dir == (int)GlobalSettings.Direction.Up) ? -1 : 0);
            MoveY = ((Dir == (int)GlobalSettings.Direction.Down) ? 1 : MoveY);

            levelCycler.currentLocationIndex[0] += MoveY;
            levelCycler.currentLocationIndex[1] += MoveX;
        }

        public void ResetTransDir()
        {
            transDirList = new bool[] { false, false, false, false };
        }
        
        public void AddHole(int Dir)
        {
            doorHole[Dir] = true;
            UpdateDrawDoors();
        }


        // Only useful during map transition 
        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > UPDATE_DELAY)
            {
                if (TransDir == (int)GlobalSettings.Direction.Up 
                    || TransDir == (int)GlobalSettings.Direction.Down) // up and down 
                    delta.Y += (TransDir == (int)GlobalSettings.Direction.Up) ? TRANSITION_STEP_Y : -TRANSITION_STEP_Y;
                else
                    delta.X += (TransDir == (int)GlobalSettings.Direction.Left) ? TRANSITION_STEP_X : -TRANSITION_STEP_X;
                timer = 0; 
            }

            if ((delta.Y> 0? delta.Y: -delta.Y) > GlobalSettings.GAME_AREA_HEIGHT ||
                (delta.X > 0 ? delta.X : -delta.X) > GlobalSettings.GAME_AREA_WIDTH)
            {
                transFinsihed = true;
                transitioning = false;
            }
        }

        // Designed for the transitioning animation 


        public void Draw()
        {
            if (transitioning)
            {
                spriteBatch.Draw(levelTexture, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY) + delta, defaultTint);
                spriteBatch.Draw(nextLevelTexture, nextLvPos[TransDir] + delta, defaultTint);
            }
            else
            {
                spriteBatch.Draw(levelTexture, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY), defaultTint);
            }
        }

        public void DrawOverlay()
        {
            if (transitioning)
            {
                spriteBatch.Draw(levelOverlay, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY) + delta, defaultTint);
                spriteBatch.Draw(headsUpFill, new Vector2(0, 0), defaultTint);
            }
            else
            {
                spriteBatch.Draw(levelOverlay, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY), defaultTint);
            }
                
        }
    }
}
