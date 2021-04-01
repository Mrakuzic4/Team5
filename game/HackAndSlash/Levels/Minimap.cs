using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    class Minimap
    {
        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private LevelCycling levelCycler;

        private bool[,] AllRoom;        // If there's a room at that position 
        private bool[,] RoomVisibility; // If that room has been explored 

        private Texture2D minimap;
        private Texture2D singleRoom;
        private Texture2D horizontalBridge;
        private Texture2D verticalBridge;

        private int roomRowCount;
        private int roomColCount;

        public int[] currentFocusIndex { get; set; }

        private const int SCALE_INDEX = 4;
        private const int UNIT_WIDTH = 12;
        private const int UNIT_HEIGHT = 7;
        private const int WHOLE_WIDTH = 14;
        private const int WHOLE_HEIGHT = 9;
        private const int BRIDGE_LEN_0 = 2;
        private const int BRIDGE_LEN_1 = 1;

        private Color defaultTint = Color.White;
        private Color transp = Color.Transparent;
        private Color fillColor = Color.Brown; 

        public Minimap(GraphicsDevice Graphics, SpriteBatch SB, LevelCycling LC)
        {
            graphics = Graphics;
            spriteBatch = SB;
            levelCycler = LC;

            roomRowCount = levelCycler.currentMapSet.GetLength(0);
            roomColCount = levelCycler.currentMapSet.GetLength(1);

            InitlizeMinimap();
        }

        public Texture2D GenerateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(graphics, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
                data[pixel] = paint(pixel);

            texture.SetData(data);
            return texture;
        }

        private void InitlizeMinimap()
        {
            AllRoom = new bool[roomRowCount, roomColCount];
            RoomVisibility = new bool[roomRowCount, roomColCount];

            minimap = GenerateTexture(roomColCount * WHOLE_WIDTH, roomRowCount * WHOLE_HEIGHT, pixel => transp);
            singleRoom = GenerateTexture(UNIT_WIDTH, UNIT_HEIGHT, pixel => fillColor);
            horizontalBridge = GenerateTexture(BRIDGE_LEN_0, BRIDGE_LEN_1, pixel => fillColor);
            verticalBridge = GenerateTexture(BRIDGE_LEN_1, BRIDGE_LEN_0, pixel => fillColor);

            for(int i = 0; i < roomRowCount; i++){
                for (int j = 0; j < roomColCount; j++){
                    AllRoom[i, j] = (levelCycler.currentMapSet[i, j] != null);
                    RoomVisibility[i, j] = (i == levelCycler.currentLocationIndex[0] && j == levelCycler.currentLocationIndex[1]);
                }
            }

            UpdateMinimap();
        }

        private void UpdateMinimap()
        {
            Color[] RoomData = new Color[singleRoom.Width * singleRoom.Height];
            Color[] BridgeVerData = new Color[verticalBridge.Width * verticalBridge.Height];
            Color[] BridgeHorData = new Color[horizontalBridge.Width * horizontalBridge.Height];

            singleRoom.GetData<Color>(RoomData);
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

                        minimap.SetData(0, new Rectangle(PosX, PosY, 
                            singleRoom.Width, singleRoom.Height), RoomData, 
                            0, RoomData.Length);
                    }
                }
            }
        }

        public void FlagExplored(int[] Index )
        {
            RoomVisibility[Index[0], Index[1]] = true; 
            UpdateMinimap();
        }

        public void Draw()
        {
            spriteBatch.Draw(minimap, new Vector2(0, 0), defaultTint);
        }

    }
}
