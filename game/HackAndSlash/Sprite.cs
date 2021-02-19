

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class AnimatedSpriteMC : IPlayer
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int UpdateDelay { get; set; }

        // Frame and fame delays 
        private int frameCounter; 
        private int currentFrame;
        private int totalFrames;
        

        public AnimatedSpriteMC(Texture2D texture, int rows, int columns, int updateDelay)
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


    public class SpriteBG : ISprite
    {
        public Texture2D Texture { get; set; }
        private Rectangle textureLocation;
        private Vector2 mapSize;
        private Vector2 windowSize;

        public SpriteBG(Texture2D texture, GraphicsDeviceManager Graphics)
        {
            Texture = texture;
            mapSize.X = Texture.Width;
            mapSize.Y = Texture.Height;
            windowSize.X = Graphics.PreferredBackBufferWidth;
            windowSize.Y = Graphics.PreferredBackBufferHeight;

            textureLocation.X = -(int)(mapSize.X - windowSize.X / 2);
            textureLocation.Y = -(int)(mapSize.Y - windowSize.Y / 2);
            textureLocation.Width = (int)(mapSize.X * 2);
            textureLocation.Height = (int)(mapSize.Y * 2);
        }

        // Check if the map display is within premitted sisplay aera 
        private bool withinBorder()
        {
            if (textureLocation.X < 0 && textureLocation.Y < 0
                && textureLocation.X > -(mapSize.X * 2 - windowSize.X)
                && textureLocation.Y > -(mapSize.Y * 2 - windowSize.Y))
                return true;
            return false; 
        }

        public void UpdateCoordX(int coordX)
        {
            textureLocation.X -= coordX;
            if (!withinBorder())
                textureLocation.X += coordX;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, textureLocation, Color.White);
            spriteBatch.End();
        }
    }
}
