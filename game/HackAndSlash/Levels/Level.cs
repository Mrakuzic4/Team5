using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HackAndSlash
{
    public class Level : ILevel
    {
        //private bool test = true;

        public bool transitioning = false;
        public bool transFinsihed = false;
        private Vector2 position = new Vector2(0, 0);
        private int timer = 0;

        private const int ALL_MIGHT_DIV = 16;
        private const int ALL_MIGH_COUNT = 256;
        private const int EDGE_PRESERVE = 17 * 4;
        private const int UPDATE_DELAY = 5; 
        private const int TRANSITION_STEP = 10; 

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        public Texture2D levelTexture; // Can be accessed to make map transitioning 
        public Texture2D levelOverlay;
        public Texture2D nextLevel; 
        private Texture2D blockAllMight;
        //private Texture2D doors; 
        private Color defaultColor = Color.Black;
        private Color defaultTint = Color.White;

        private int defaultBlockIndex; 
        private int[,] mapMatrix; 

        // Up, Bottom, Left Right 
        private bool[] doorOpen =   { false, false, false, false };    // Highest priority 
        private bool[] doorHole=    { false, false, false, false };    // Second in command 
        private bool[] doorLocked = { false, false, false, false };    // Lowest priority  
        private bool[] doorHiden =  { false, false, false, false };    // Does not draw 
        private Dictionary<int, int> doorDirMapping = new Dictionary<int, int>(){
            {0, 0},
            {1, 3},
            {2, 1},
            {3, 2}
            };  // Mapping the direction Enum to the image of th4 doors 
        private int[] iter = { 0, 1, 2, 3 }; // Minimize magic number in door iteration 
        private const int DOOR_OPEN_INDEX = 1;
        private const int DOOR_LOCKED_INDEX = 2;
        private const int DOOR_MYS_INDEX = 3;
        private const int DOOR_HOLE_INDEX = 4;

        public Level(GraphicsDevice graphics, SpriteBatch spriteBatch, int[,] Arrangement, int Defaultblock,
            bool[] DO, bool[] DH, bool[] DL)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.defaultBlockIndex = Defaultblock;
            this.doorOpen = DO;
            this.doorHiden = DH;
            this.doorLocked = DL;

            levelTexture = GenerateTexture(GlobalSettings.GAME_AREA_WIDTH, GlobalSettings.GAME_AREA_HEIGHT, pixel => defaultColor);
            mapMatrix = Arrangement;

            blockAllMight = SpriteFactory.Instance.getBlockAllMight();

            AlterTexture();
            GenerateOverlay();

            AddHole(1);
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
            UpdateDoors();
        }

        // Update the doors, used both in initialization and when new doors are being dynamically added 
        public void UpdateDoors()
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
                // Pre-launch check 
                Col = 0;
                if (doorLocked[Dir]) Col = DOOR_LOCKED_INDEX;
                if (doorHole[Dir])   Col = DOOR_HOLE_INDEX;
                if (doorOpen[Dir])   Col = DOOR_OPEN_INDEX;
                switch (Dir) {
                    case 0:
                        DestRectangle.X = HorizontalPos;
                        DestRectangle.Y = TopPosition;
                        break;
                    case 1:
                        DestRectangle.X = HorizontalPos;
                        DestRectangle.Y = ButtPosition;
                        break;
                    case 2:
                        DestRectangle.X = LeftPosition;
                        DestRectangle.Y = VerticalPos;
                        break;
                    case 3:
                        DestRectangle.X = RightPosition;
                        DestRectangle.Y = VerticalPos;
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

        // Up, Bottom, Left Right 
        public bool canGoThrough(int Dir)
        {
            return doorOpen[Dir] || doorHole[Dir];
        }

        public void AddHole(int Dir)
        {
            doorHole[Dir] = true;
            UpdateDoors();
        }


        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > UPDATE_DELAY)
            {
                position.Y += TRANSITION_STEP;
                timer = 0; 
            }
            if (position.Y > GlobalSettings.GAME_AREA_HEIGHT + TRANSITION_STEP)
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
                spriteBatch.Draw(levelTexture, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY) + position, defaultTint);
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
                spriteBatch.Draw(levelOverlay, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY) + position, defaultTint);
            }
            else
            {
                spriteBatch.Draw(levelOverlay, new Vector2(0, GlobalSettings.HEADSUP_DISPLAY), defaultTint);
            }
                
        }
    }
}
