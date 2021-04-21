using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class BlockTexture
    {
        private SpriteBatch spritebatch;
        private GraphicsDevice graphics; 

        private Texture2D blockAllMight; // Containing all blocks 
        private int tarIndex;

        private const int ALL_MIGHT_DIV = 16;
        private const int ALL_MIGH_COUNT = 256;
        private Color defaultColor = Color.Black;

        public BlockTexture(SpriteBatch Spritebatch, GraphicsDevice Graphics, int Index)
        {
            blockAllMight = SpriteFactory.Instance.getBlockAllMight();
            spritebatch = Spritebatch;
            graphics = Graphics;
            tarIndex = Index; 
        }

        public Texture2D GetTexture()
        {
            return getBlockByIndex(tarIndex); 
        }

        private Texture2D GenerateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(graphics, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
                data[pixel] = paint(pixel);

            texture.SetData(data);
            return texture;
        }


        private Texture2D getBlockByIndex(int index)
        {
            Texture2D block = GenerateTexture(GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR, 
                pixel => defaultColor);
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
    }
}
