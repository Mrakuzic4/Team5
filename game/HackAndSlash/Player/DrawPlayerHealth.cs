
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class DrawPlayerHealth : ISprite
    {
        private Game1 game;

        //three heart sprites
        Texture2D emptyHeart;
        Texture2D halfHeart;
        Texture2D fullHeart;
        Texture2D fontLife;

        //number of different hearts
        private int full; 
        private int half;
        private int empty;

        private Rectangle destinationRectangle; //position of the heart that is to be drawn
        private Rectangle fontRectangle; //position of the font that is to be drawn

        //Position of Hearts on HUD
        private const int Y = 84;
        private const int X = 768;
        private const int DEST_SIZE = 32;
        private const int SRC_SIZE = 8;

        //Position of Fonts on HUD
        private const int Y_Font = 4;

        private const int SCALE = 4;

        private float layer = 0.9f; 

        public DrawPlayerHealth(Game1 game, Texture2D emptyHeart, Texture2D halfHeart, Texture2D fullheart, Texture2D fontLife)
        {
            this.game = game;
            this.fullHeart = fullheart;
            this.halfHeart = halfHeart;
            this.emptyHeart = emptyHeart;
            this.full = this.game.Player.GetMaxHealth();
            this.half = 0;
            this.empty = 0;
            this.fontLife = fontLife;
            destinationRectangle = new Rectangle(X, Y, DEST_SIZE, DEST_SIZE);
            fontRectangle = new Rectangle(X, Y_Font, fontLife.Width*SCALE, fontLife.Height*SCALE);
        }

        /// <summary>
        /// Calculate the number of different kinds of hearts needed on HUD.
        /// full=6 indicates HP = full / 2 = 3
        /// </summary>
        public void Update()
        {
            int currentHealth = this.game.Player.GetCurrentHealth();
            int maxHealth = this.game.Player.GetMaxHealth();
            int notEmptyHearts; //for the purpose of calculating empty hearts, number of not empty Hearts

            //number of half heart, could be either 0 or 1
            half = currentHealth % 2;
            //two times the number of full hearts
            if (half != 0)
            {
                full = currentHealth - 1;
                notEmptyHearts = currentHealth + 1;
            }
            else
            {
                full = currentHealth;
                notEmptyHearts = full;
            }
            //two times the number of empty hearts
            empty = maxHealth - notEmptyHearts;

            //Correct number of full and empty hearts
            empty /= 2;
            full /= 2;
        }

        /// <summary>
        /// Draw different hearts on HUD.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        /// <param name="color"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            //Draw font "-Life-"
            Rectangle sourceRectangle = new Rectangle(0, 0, 48, 8);
            spriteBatch.Draw(fontLife, fontRectangle, sourceRectangle, color, 
                0f, Vector2.Zero, SpriteEffects.None, layer);

            //Draw Hearts
            sourceRectangle = new Rectangle(0, 0, SRC_SIZE, SRC_SIZE); //reset source rectangle.

            //draw full hearts
            for (int i = 1; i < full+1; i++)
            {
                spriteBatch.Draw(fullHeart, destinationRectangle, sourceRectangle, color, 
                    0f, Vector2.Zero, SpriteEffects.None, layer);
                destinationRectangle = new Rectangle(X + DEST_SIZE * i, Y, DEST_SIZE, DEST_SIZE);
            }
            int destinationRectangleForHalf = destinationRectangle.X; //this is not true if there is no full hearts.

            //draw half hearts
            for (int i = 1; i < half+1; i++)
            {
                spriteBatch.Draw(halfHeart, destinationRectangle, sourceRectangle, color, 
                    0f, Vector2.Zero, SpriteEffects.None, layer);
                destinationRectangle = new Rectangle(destinationRectangleForHalf + DEST_SIZE * i, Y, DEST_SIZE, DEST_SIZE);
            }
            int destinationRectangleForEmpty = destinationRectangle.X;

            //draw empty hearts
            for (int i = 1; i < empty+1; i++)
            {
                spriteBatch.Draw(emptyHeart, destinationRectangle, sourceRectangle, color, 
                    0f, Vector2.Zero, SpriteEffects.None, layer);
                destinationRectangle = new Rectangle(destinationRectangleForEmpty + DEST_SIZE * i, Y, DEST_SIZE, DEST_SIZE);
            }
            destinationRectangle = new Rectangle(X, Y, DEST_SIZE, DEST_SIZE); //reset the dest rectangle
        }
    }
    
}
