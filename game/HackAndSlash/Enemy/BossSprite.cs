using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HackAndSlash
{
    public class BossSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        private int totalFrames;
        private int currentFrame;
        private long animeDelay = GlobalSettings.DELAY_TIME;
        private Stopwatch stopwatch = new Stopwatch();
        private long timer; 

        public BossSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            totalFrames = rows * columns;
            currentFrame = 0;
            stopwatch.Restart();
        }

        public void Update()
        {
            // Record the time elapsed 
            timer = stopwatch.ElapsedMilliseconds;
            // Every time the time elpased exceeds the designated delay amount,
            // update the frame and restart the timer 
            if (timer > animeDelay)
            {
                currentFrame++;
                stopwatch.Restart();
                timer = 0;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 3 * GlobalSettings.BASE_SCALAR, 4 * GlobalSettings.BASE_SCALAR);

            //spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
            //spriteBatch.End();

        }


    }
}
