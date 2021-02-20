

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class AttackPlayer : ISprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int UpdateDelay { get; set; }

        // Frame and fame delays 
        private int frameCounter; 
        private int currentFrame;
        private int totalFrames;        

        public AttackPlayer(Texture2D texture, int rows, int columns, int updateDelay)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            UpdateDelay = updateDelay;
            currentFrame = 0;
            frameCounter = 0;
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

                currentFrame++;
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
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
            spriteBatch.End();
        }
    }
    
}
