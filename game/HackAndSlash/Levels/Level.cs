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

        public Level(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;

            levelTexture = GenerateTexture(GlobalSettings.WINDOW_WIDTH, GlobalSettings.WINDOW_HEIGHT, pixel => defaultColor);
            mapMatrix = LevelDatabase.Instance.DemoM1;
            levelStyle = LevelDatabase.Instance.DemoLevelStyle; 

            AlterTexture();
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

                    int TileTypeNow = mapMatrix[r, c];
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


        public void Update()
        {
            // Reserved for changing levels and breaking doors 
        }


        public void Draw()
        {
            spriteBatch.Draw(levelTexture, new Vector2(0, 0), defaultTint);
        }
    }
}
