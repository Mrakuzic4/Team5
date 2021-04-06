using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace HackAndSlash
{
    class DrawRectangle
    {

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;

        public int thickness { get; set; }
        private Texture2D RectangleLine;
        public Rectangle position;

        public Color fillColor { get; set; }
        private Color transp = Color.Transparent; // Texture generation placeholder colod 
        private Color defaultTint = Color.White;  // Draw() method tint 


        public DrawRectangle(GraphicsDevice Graphics, SpriteBatch SB, Rectangle P, Color fill)
        {
            graphics = Graphics;
            spriteBatch = SB;
            position = P;

            thickness = 4;
            fillColor = fill;

            Generate();
        }

        private Texture2D GenerateFill(int width, int height, Func<int, Color> paint)
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
            RectangleLine = GenerateFill(position.Width, position.Height, pixel => fillColor);

            Texture2D BorderInnerTransp = GenerateFill(RectangleLine.Width - 2 * thickness,
                RectangleLine.Height - 2 * thickness, pixel => transp);
            Color[] InnerFillData = new Color[BorderInnerTransp.Width * BorderInnerTransp.Height];

            BorderInnerTransp.GetData(InnerFillData);

            RectangleLine.SetData(0, new Rectangle(thickness, thickness, 
                BorderInnerTransp.Width, BorderInnerTransp.Height),
                InnerFillData, 0, InnerFillData.Length);
        }

        public void Draw()
        {
            spriteBatch.Draw(RectangleLine, position, null, defaultTint);
        }
    }
}
