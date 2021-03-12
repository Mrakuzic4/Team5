using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class Level : ILevel
    {
        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private Texture2D levelTexture;
        private Color defaultColor = Color.Black;
        private Color defaultTint = Color.White;

        private int[,] mapMatrix; 
        private Dictionary<int, Texture2D>  levelStyle;

        // Up, Bottom, Left Right 
        private bool[] doorOpen = { false, false, false, false };
        private bool[] doorHole= { false, false, false, false };
        private bool[] doorLocked = { false, false, false, false };

        private JsonParser parser; 

        public Level(GraphicsDevice graphics, SpriteBatch spriteBatch, int[,] Arrangement, int Defaultblock)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;

            levelTexture = GenerateTexture(GlobalSettings.WINDOW_WIDTH, GlobalSettings.WINDOW_HEIGHT, pixel => defaultColor);
            mapMatrix = Arrangement;
            levelStyle = LevelDatabase.Instance.DemoLevelStyle;


            AlterTexture();
            addOpenDoor(0);
            addOpenDoor(2);
            addOpenHole(1);
        }   

        
        // Generate a texture filled with default color 
        private Texture2D GenerateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(graphics, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
                data[pixel] = paint(pixel);

            texture.SetData(data);
            return texture;
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

                    int TileTypeNow = mapMatrix[r, c] > 31 ? 1 : 0;
                    Texture2D TextureNow = levelStyle[TileTypeNow];
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
        }

        public void addOpenHole(int Direction)
        {
            doorHole[Direction] = true;
        }

        public void addOpenDoor(int Direction)
        {
            doorOpen[Direction] = true;
            //Texture2D[] Doors = SpriteFactory.Instance.GetLevelEagleDoorNormOpen();

            //foreach (int Dir in new int[] {0, 1, 2, 3 })
            //{
            //    if (Dir == Direction)
            //    {
            //        doorOpen[Dir] = true;

            //        Texture2D Door = Doors[Dir];
            //        int Count = Door.Width * Door.Height;
            //        Color[] RawData = new Color[Count];
            //        Door.GetData<Color>(RawData);

            //        levelTexture.SetData(0, new Rectangle(0, 0, Door.Width, Door.Height),
            //            RawData, 0, Count);
            //    }
            //}
        }

        // Up, Bottom, Left Right 
        public bool canGoThrough(int Dir)
        {
            return doorOpen[Dir];
        }

        public void Update()
        {
            // Reserved for changing levels and breaking doors 
        }


        public void Draw()
        {
            // Iterator 
            int[] iter = { 0, 1, 2, 3 };
            Texture2D[] Doors = SpriteFactory.Instance.GetLevelEagleDoorNormOpen();
            Texture2D[] Holes = SpriteFactory.Instance.GetLevelEagleHoles();

            spriteBatch.Draw(levelTexture, new Vector2(0, 0), defaultTint);

            foreach (int Dir in iter)
            {
                if (doorOpen[Dir]) spriteBatch.Draw(Doors[Dir], new Vector2(0, 0), defaultTint);
                if (doorHole[Dir]) spriteBatch.Draw(Holes[Dir], new Vector2(0, 0), defaultTint);
            }
        }
    }
}
