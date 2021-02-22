

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{



    public class DrawPlayer : ISprite
    {
                
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int UpdateDelay { get; set; }

        // Frame and fame delays 
        private int frameCounter; 
        private int currentFrame;
        private int totalFrames;

        public int Frame
        {
            get
            {
                return currentFrame;
            }
            set 
            { 
                currentFrame = value; 
            }
        }

        //Singleton
        private static DrawPlayer instance = new DrawPlayer();

        public static DrawPlayer Instance
        {
            get
            {
                return instance;
            }
        }

        private DrawPlayer()
        {
            UpdateDelay = 8;
            currentFrame = 0;
            frameCounter = 0;
        }

        public void SetTexture(Texture2D texture)
        {
            this.Texture = texture;
            //This is safe because Rows and Columns
            //are already set before calling SetTexture.
            totalFrames = Rows * Columns; 
        }

        public int GetCurrentFrame()
        {
            return currentFrame;
        }

        public void Update()
        {
            frameCounter++;
            // Add delay 
            if (frameCounter == UpdateDelay) {

                // Loop reset 
                if (currentFrame == totalFrames)
                    currentFrame = 0;

               // currentFrame++; //maybe comment it out?
                frameCounter = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (Rows == 1) ? 0 : (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width*5, height*5);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
            spriteBatch.End();
        }
    }
    
}
