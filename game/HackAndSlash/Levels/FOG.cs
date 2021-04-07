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

        private int scaler;
        private int width;
        private int height; 

        private Vector2 position;
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice graphics; // the graphics device used by the spritebatch

        private Texture2D maskArea;
        private Texture2D blackBlock;

        private Color fillColor = Color.Black;  // Draw() method tint 
        private Color defaultTint = Color.White;  // Draw() method tint 
        private double opacity = 0.9;

        private float layer = 0.6f;

        public FOG(GraphicsDevice G, SpriteBatch SB)
        {
            graphics = G;
            spriteBatch = SB;

            maskArea = SpriteFactory.Instance.GetFOGMask();

            width = maskArea.Width;
            height = maskArea.Height;

            SetRange(1);

            Generate();
        }

        public void SetRange(int Range)
        {
            scaler = Range + 2;
            position = new Vector2(-((scaler * width / 2) - GlobalSettings.BASE_SCALAR / 2),
                -((scaler * height / 2) - GlobalSettings.BASE_SCALAR / 2));
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
            blackBlock = GenerateTexture(GlobalSettings.WINDOW_WIDTH, GlobalSettings.WINDOW_HEIGHT, pixel => fillColor);
        }

        public void Draw(Vector2 PlayerPos, bool InTransition)
        {
            if (InTransition) {
                spriteBatch.Draw(blackBlock, new Vector2(0, 0), null,
                    defaultTint * (float)opacity, 0f, Vector2.Zero, 1, SpriteEffects.None, layer);
            }
            else {
                spriteBatch.Draw(maskArea, position + PlayerPos, null,
                    defaultTint * (float)opacity, 0f, Vector2.Zero, scaler, SpriteEffects.None, layer);
            }
        }
    }
}
