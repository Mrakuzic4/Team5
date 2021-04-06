using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace HackAndSlash
{
    class FOG
    {

        private const int OFFEST_UNIT = 1;

        private Vector2 position;
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice graphics; // the graphics device used by the spritebatch

        private Texture2D maskArea;

        private Color defaultTint = Color.White;  // Draw() method tint 
        private Color transp = Color.Transparent; // Texture generation placeholder colod 

        public FOG(Vector2 PlayerPos, GraphicsDevice G, SpriteBatch SB)
        {
            position = PlayerPos;
            graphics = G;
            spriteBatch = SB;

            maskArea = SpriteFactory.Instance.GetFOGMask();
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

        private void Generate()
        {

        }

        public void Draw()
        {
            spriteBatch.Draw(maskArea, position, null,
                defaultTint, 0f, Vector2.Zero, 4, SpriteEffects.None, 1f);
        }
    }
}
