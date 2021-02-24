

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
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

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            
            spriteBatch.Draw(Texture, textureLocation, Color.White);
            
        }
    }
}
