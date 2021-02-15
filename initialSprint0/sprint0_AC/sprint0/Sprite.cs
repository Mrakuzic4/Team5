using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint0
{

    // Theoretically not needed but still: 
    public class StaticSpriteMC : ISprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;


        public StaticSpriteMC(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
        }

        public void SetCurrentFrame(int TarFrame)
        {
            currentFrame = TarFrame;
        }

        public void Update()
        {
            // Nothing to do here, since it does not update 
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (Rows == 1)? 0 : (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

    }

    public class AnimatedSpriteMC : ISprite
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

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (Rows == 1) ? 0 : (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }

    public class TextSprite : ISprite
    {
        
        private int width { get; set; }
        private int height { get; set; }
        private Vector2 creditTextPos;
        private int currentState;
        private SpriteFont font;

        private const string stateHeader = "Use 1-5 to control state, 0 to quite\n"
            + "Current state: ";
        private const string credit = "Credits: \n"
            + "Program made by Amarth Chen (.8759) \n"
            + "Sprite: https://www.spriters-resource.com/fullview/146744/ \n"
            + "Background: https://medium.com/odd-nugget/the-art-of-h-r-giger-4020ad7e38b8 ";

        public TextSprite(GraphicsDeviceManager Graphics, SpriteFont Font)
        {
            width = Graphics.PreferredBackBufferWidth;
            height = Graphics.PreferredBackBufferHeight;
            font = Font;

            creditTextPos.X = width / 100;
            creditTextPos.Y = height - height / 4;
        }

        private string StateMessage(int tarState)
        {
            var StateMap = new Dictionary<int, String>(){
                {1, "1, On hold."},
                {2, "2, Static mode."},
                {3, "3, Patrol up and down."},
                {4, "4, Patrol left and right."},
                {5, "5, Free arrow key movement."}
            };
            return StateMap[tarState]; 
        }

        public void SetState(int state)
        {
            currentState = state;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            position.X += width / 100;
            position.Y += height / 100;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, credit, creditTextPos, Color.White);
            spriteBatch.DrawString(font, stateHeader + StateMessage(currentState), position, Color.White);
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
