
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;




namespace HackAndSlash
{
    public class Minimap
    {
        private bool dev = false;

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private LevelCycling levelCycler;

        private bool[,] AllRoom;        // If there's a room at that position 
        private bool[,] RoomVisibility; // If that room has been explored 
        private int[,] RoomTypeIndex; 

        // Textures 
        private Texture2D minimap;
        private Texture2D singleRoom;
        private Texture2D horizontalBridge;
        private Texture2D verticalBridge;
        private Texture2D playerNotation;
        private Texture2D borderLines;
        private Texture2D merchantRoom;
        private Texture2D bossRoom; 

        // The size of the current maps 
        private int roomRowCount;
        private int roomColCount;

        // Regarding the drawing of the texture 
        public int[] currentFocusIndex { get; set; }
        private double clipX = 0;
        private double clipY = 0;
        private Vector2 playerOffset = new Vector2(0, 0); // Offset for replicating player movement
        private Vector2 transOffset = new Vector2(0, 0);  // Offset for transition animation 
        private Vector2 playerEdgeRelocate = new Vector2(0, 0);
        private float layer = 0.9f; 

        // Const 
        private const bool DISCOVER_MODE_FLAG = true;
        private const int SCALE_INDEX = 4;
        private const int PLAYER_NOTATION_SIZE = 4;
        private const int BORDER_WIDTH = 1; 
        private const int UNIT_WIDTH = 12;
        private const int UNIT_HEIGHT = 7;
        private const int WHOLE_WIDTH = 14;
        private const int WHOLE_HEIGHT = 9;
        private const int BRIDGE_LEN_0 = 2;
        private const int BRIDGE_LEN_1 = 1;
        private const int DRAW_POSITION_X = 8;
        private const int DRAW_POSITION_Y = 6;
        private const int DISPLAY_REGION_X = 3 * WHOLE_WIDTH;
        private const int DISPLAY_REGION_Y = 3 * WHOLE_HEIGHT;
        public const double SIZE_RATIO = (double)WHOLE_WIDTH / (double)(GlobalSettings.GAME_AREA_WIDTH);
        private const double TRANSITION_STEP_Y = 8 * SIZE_RATIO;  // From class Level
        private const double TRANSITION_STEP_X = 16 * SIZE_RATIO;
        private const int MAP_DISPLAY_X = 320; 
        private const int MAP_DISPLAY_Y = 256;
        private const int SCALE_FACTOR = 4; 


        //Misc 
        private Color defaultTint = Color.White;  // Draw() method tint 
        private Color transp = Color.Transparent; // Texture generation placeholder colod 
        private Color fillColor = Color.Brown;    // Minimap room color
        private Color playerFill = Color.Orange;  // Player box color 
        private Color borderFill = Color.Crimson; // Border color 
        private Color merchantRoomColor = Color.Bisque;
        private Color bossRoomColor = Color.CornflowerBlue;
        private enum RoomTypes {Default, Merchant, Boss};

        public Minimap(GraphicsDevice Graphics, SpriteBatch SB, LevelCycling LC)
        {
            graphics = Graphics;
            spriteBatch = SB;
            levelCycler = LC;

            roomRowCount = levelCycler.currentMapSet.GetLength(0);
            roomColCount = levelCycler.currentMapSet.GetLength(1);

            InitlizeMinimap();
            GenerateBorder(); 
            UpdateMinimap();
        }

        // Util method of generating a texture2D filled with certain color 
        public Texture2D GenerateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(graphics, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
                data[pixel] = paint(pixel);

            texture.SetData(data);
            return texture;
        }

        // Create/initilize all the textures and matrices 
        private void InitlizeMinimap()
        {
            AllRoom = new bool[roomRowCount, roomColCount];
            RoomVisibility = new bool[roomRowCount, roomColCount];
            RoomTypeIndex = new int[roomRowCount, roomColCount];

            minimap = GenerateTexture(roomColCount * WHOLE_WIDTH, roomRowCount * WHOLE_HEIGHT, pixel => transp);
            singleRoom = GenerateTexture(UNIT_WIDTH, UNIT_HEIGHT, pixel => fillColor);
            horizontalBridge = GenerateTexture(BRIDGE_LEN_0, BRIDGE_LEN_1, pixel => fillColor);
            verticalBridge = GenerateTexture(BRIDGE_LEN_0, BRIDGE_LEN_0, pixel => fillColor);
            playerNotation = GenerateTexture(PLAYER_NOTATION_SIZE, PLAYER_NOTATION_SIZE, pixel => playerFill);
            merchantRoom = GenerateTexture(UNIT_WIDTH, UNIT_HEIGHT, pixel => merchantRoomColor);
            bossRoom = GenerateTexture(UNIT_WIDTH, UNIT_HEIGHT, pixel => bossRoomColor);

            for (int i = 0; i < roomRowCount; i++) {
                for (int j = 0; j < roomColCount; j++)
                {
                    AllRoom[i, j] = (levelCycler.currentMapSet[i, j] != null);
                    RoomTypeIndex[i, j] = 0; 
                    RoomVisibility[i, j] = (i == levelCycler.currentLocationIndex[0] && j == levelCycler.currentLocationIndex[1]);
                }
            }
        }

        // Update minimap depending on which room has been explored 
        private void UpdateMinimap()
        {
            Color[] RoomData = new Color[singleRoom.Width * singleRoom.Height];
            Color[] BridgeVerData = new Color[verticalBridge.Width * verticalBridge.Height];
            Color[] BridgeHorData = new Color[horizontalBridge.Width * horizontalBridge.Height];

            verticalBridge.GetData<Color>(BridgeVerData);
            horizontalBridge.GetData<Color>(BridgeHorData);

            for (int i = 0; i < roomRowCount; i++)
            {
                for (int j = 0; j < roomColCount; j++)
                {
                    if (RoomVisibility[i, j])
                    {
                        int PosX = j * WHOLE_WIDTH + 1;
                        int PosY = i * WHOLE_HEIGHT + 1;

                        switch (RoomTypeIndex[i, j]){
                            case (int)RoomTypes.Merchant:
                                merchantRoom.GetData<Color>(RoomData);
                                break;
                            case (int)RoomTypes.Boss:
                                bossRoom.GetData<Color>(RoomData);
                                break;
                            default:
                                singleRoom.GetData<Color>(RoomData);
                                break;
                        }

                        minimap.SetData(0, new Rectangle(PosX, PosY,
                            singleRoom.Width, singleRoom.Height), RoomData,
                            0, RoomData.Length);
                    }
                }
            }
        }

        // Generate border texture 
        private void GenerateBorder()
        {
            Texture2D BorderInnerTransp = GenerateTexture(DISPLAY_REGION_X, DISPLAY_REGION_Y, pixel => transp);
            Color[] InnerFillData = new Color[BorderInnerTransp.Width * BorderInnerTransp.Height];

            borderLines = GenerateTexture(DISPLAY_REGION_X + BORDER_WIDTH * 2, 
                DISPLAY_REGION_Y + BORDER_WIDTH * 2, pixel => borderFill);

            BorderInnerTransp.GetData(InnerFillData);
            borderLines.SetData(0, new Rectangle(1, 1, BorderInnerTransp.Width, BorderInnerTransp.Height),
                InnerFillData, 0, InnerFillData.Length); 
        }

        // Add the connection between 2 minimap blocks 
        private void AddBridge(int Direction)
        {
            const int HORIZONTAL_OFFST = 6;
            const int VERTICAL_OFFSET = 4; 

            Texture2D Bridge = (Direction == (int)GlobalSettings.Direction.Up || Direction == (int)GlobalSettings.Direction.Down) ?
                verticalBridge : horizontalBridge;
            Color[] BridgeData = new Color[Bridge.Width * Bridge.Height];
            Bridge.GetData(BridgeData);

            int VerticalPosition = currentFocusIndex[0] * WHOLE_HEIGHT;
            int HorizontalPosition = currentFocusIndex[1] * WHOLE_WIDTH ;

            if (Direction == (int)GlobalSettings.Direction.Left || Direction == (int)GlobalSettings.Direction.Right)
            {
                HorizontalPosition += ((Direction == (int)GlobalSettings.Direction.Left)? 0 : WHOLE_WIDTH) - 1;
                VerticalPosition += VERTICAL_OFFSET; 
            }
            else if (Direction == (int)GlobalSettings.Direction.Up || Direction == (int)GlobalSettings.Direction.Down)
            {
                VerticalPosition += ((Direction == (int)GlobalSettings.Direction.Up) ? 0 : WHOLE_HEIGHT) - 1;
                HorizontalPosition += HORIZONTAL_OFFST;
            }

            minimap.SetData(0, new Rectangle(HorizontalPosition, VerticalPosition, Bridge.Width, Bridge.Height), 
                BridgeData, 0, BridgeData.Length);
        }

        // Mark a room as explored and make it visible in minimap 
        public void FlagExplored(int[] Index, Map MapInfo)
        {
            int RoomType = 0;
            List<int> merchantCharaList = new List<int>() {
                GlobalSettings.NPC_OLD_MAN,
                GlobalSettings.NPC_OLD_WOMAN
            };
            List<int> bossCharaList = new List<int>() {
                GlobalSettings.BOSS_ENEMY
            };

            foreach (int i in new int[] {0, 1, 2, 3 }) {
                if (MapInfo.LockedDoors[i] || MapInfo.MysteryDoors[i] || MapInfo.OpenDoors[i])
                    AddBridge(i);
            }

            for (int i = 0; i < MapInfo.Arrangement.GetLength(0); i++) {
                for (int j = 0; j < MapInfo.Arrangement.GetLength(1); j++) {
                    int nowIndex = MapInfo.Arrangement[i, j];
                    int bossInd = GlobalSettings.BOSS_ENEMY;
                    if (merchantCharaList.Contains(MapInfo.Arrangement[i, j])) {
                        RoomTypeIndex[Index[0], Index[1]] = (int)RoomTypes.Merchant;
                        break;
                    }
                    
                    else if (MapInfo.Arrangement[i, j] == GlobalSettings.BOSS_ENEMY) {
                        RoomTypeIndex[Index[0], Index[1]] = (int)RoomTypes.Boss;
                        break;
                    }
                }
            }

            RoomVisibility[Index[0], Index[1]] = true;
            UpdateMinimap();
        }

        // Set current minimap display focus 
        public void SetPivot(int[] Index)
        {
            currentFocusIndex = Index;

            // Dealing with outlier cases when there's no more area to clip or move
            if (currentFocusIndex[0] >= levelCycler.currentMapSet.GetLength(0) - 1) {
                clipY = (levelCycler.currentMapSet.GetLength(0) - 2) * WHOLE_HEIGHT;
            }    
            else if (currentFocusIndex[0] <= 0) {
                playerEdgeRelocate.Y = - UNIT_HEIGHT * SCALE_INDEX - PLAYER_NOTATION_SIZE * 2; 
                clipY = 0;
            }
            else {
                clipY = (currentFocusIndex[0] - 1) * WHOLE_HEIGHT;
                playerEdgeRelocate.Y = 0;
            }

            if (currentFocusIndex[1] >= levelCycler.currentMapSet.GetLength(1) - 1) {
                clipX = (levelCycler.currentMapSet.GetLength(1) - 2) * WHOLE_WIDTH;

            }
            else if (currentFocusIndex[1] <= 0) {
                clipX = 0;
                playerEdgeRelocate.X = -UNIT_WIDTH * SCALE_INDEX; 
            }
            else {
                clipX = (currentFocusIndex[1] - 1) * WHOLE_WIDTH;
                playerEdgeRelocate.X = 0;
            }

        }

        // Normal update to replicate player position 
        public void UpdatePlayer(Vector2 PlayerPos)
        {
            Vector2 GameAreaMid = new Vector2(GlobalSettings.WINDOW_WIDTH / 2,
                GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BORDER_OFFSET + GlobalSettings.GAME_AREA_HEIGHT / 2);

            Vector2 RealOffset = PlayerPos - GameAreaMid;

            playerOffset.X = (RealOffset.X / (GlobalSettings.GAME_AREA_WIDTH - GlobalSettings.BASE_SCALAR)
                * UNIT_WIDTH * SCALE_INDEX) - PLAYER_NOTATION_SIZE;
            playerOffset.Y = (RealOffset.Y / (GlobalSettings.GAME_AREA_HEIGHT - GlobalSettings.BASE_SCALAR)
                * UNIT_HEIGHT * SCALE_INDEX) + PLAYER_NOTATION_SIZE;

            transOffset = new Vector2(0, 0); 

        }

        // Update for transition animation 
        public void UpdateTransition(int Direction)
        {
            AddBridge(Direction);

            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    if (currentFocusIndex[0] > 1)
                        transOffset.Y -= (float)(TRANSITION_STEP_Y);
                    break;
                case (int)GlobalSettings.Direction.Down:
                    if (currentFocusIndex[0] > 0)
                        transOffset.Y += (float)(TRANSITION_STEP_Y);
                    break;
                case (int)GlobalSettings.Direction.Right:
                    if (currentFocusIndex[1] > 0)
                        transOffset.X += (float)(TRANSITION_STEP_X);
                    break;
                case (int)GlobalSettings.Direction.Left:
                    if (currentFocusIndex[1] > 1)
                        transOffset.X -= (float)(TRANSITION_STEP_X);
                    break;
                default:
                    break;
            }
        }

        // Top-left corner display 
        public void Draw()
        {
            Rectangle MiniMapSrcClip = new Rectangle((int)(clipX + transOffset.X - 1), (int)(clipY + transOffset.Y - 1),
                DISPLAY_REGION_X, DISPLAY_REGION_Y);
            Vector2 PlayerBoxLocation = -3 * transOffset + playerEdgeRelocate + playerOffset + new Vector2(
                DRAW_POSITION_X + (int)(WHOLE_WIDTH * SCALE_INDEX * 1.5),
                DRAW_POSITION_Y + (int)(WHOLE_HEIGHT * SCALE_INDEX * 1.5));


            // The minimap 
            spriteBatch.Draw(minimap, new Vector2(DRAW_POSITION_X, DRAW_POSITION_Y),
                MiniMapSrcClip, defaultTint, 0f, Vector2.Zero, SCALE_FACTOR, SpriteEffects.None, layer);

            // The tiny box denoting player's position 
            spriteBatch.Draw(playerNotation, PlayerBoxLocation,
                null, defaultTint, 0f, Vector2.Zero, SCALE_FACTOR, SpriteEffects.None, layer + .1f);

            // The border 
            spriteBatch.Draw(borderLines, new Vector2(DRAW_POSITION_X, DRAW_POSITION_Y), null,
                defaultTint, 0f, Vector2.Zero, SCALE_FACTOR, SpriteEffects.None, layer);
        }

        // Tab display 
        public void DrawMap()
        {
            Vector2 PlayerDot = new Vector2(
                currentFocusIndex[1] * WHOLE_WIDTH * 4 + MAP_DISPLAY_X + (WHOLE_WIDTH - PLAYER_NOTATION_SIZE) * 2,
                currentFocusIndex[0] * WHOLE_HEIGHT * 4 + MAP_DISPLAY_Y + (WHOLE_HEIGHT - PLAYER_NOTATION_SIZE) * 2
                ) ;

            spriteBatch.Draw(playerNotation, PlayerDot,
                null, defaultTint, 0f, Vector2.Zero, 4, SpriteEffects.None, layer + .1f);

            spriteBatch.Draw(minimap, new Vector2(MAP_DISPLAY_X, MAP_DISPLAY_Y),
                null, defaultTint, 0f, Vector2.Zero, SCALE_FACTOR, SpriteEffects.None, layer);
        }

        public void DrawMapPause()
        {
            Vector2 PauseDrawPos = new Vector2(GlobalSettings.WINDOW_WIDTH / 2 - minimap.Width * 2, 
                GlobalSettings.BASE_SCALAR * 2);
            Vector2 PlayerDot = new Vector2(
                currentFocusIndex[1] * WHOLE_WIDTH * 4 + (WHOLE_WIDTH - PLAYER_NOTATION_SIZE) * 2,
                currentFocusIndex[0] * WHOLE_HEIGHT * 4 + (WHOLE_HEIGHT - PLAYER_NOTATION_SIZE) * 2
                ) + PauseDrawPos;

            spriteBatch.Draw(playerNotation, PlayerDot,
                null, defaultTint, 0f, Vector2.Zero, 4, SpriteEffects.None, layer + .1f);

            spriteBatch.Draw(minimap, PauseDrawPos,
                null, defaultTint, 0f, Vector2.Zero, SCALE_FACTOR, SpriteEffects.None, layer);
        }

    }
}
