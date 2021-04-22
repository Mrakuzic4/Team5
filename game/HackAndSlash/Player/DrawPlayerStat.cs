
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class DrawPlayerStat : ISprite
    {
        private Game1 game;

        //three heart sprites
        Texture2D emptyHeart;
        Texture2D halfHeart;
        Texture2D fullHeart;
        Texture2D shield;
        Texture2D fontLife;
        Texture2D fontShield;

        //number of different hearts
        private int full; 
        private int half;
        private int empty;

        private Rectangle destinationRectangle; //position of the heart that is to be drawn
        private Rectangle shieldDestinationRectangle; //position of the shield that is to be drawn
        private Rectangle lifeFontRectangle; //position of the font that is to be drawn
        private Rectangle shieldFontRectangle; //position of the font that is to be drawn

        //Position of Hearts on HUD
        private const int Y = 40;
        private const int X = 768;
        private const int DEST_SIZE = 32;
        private const int SRC_SIZE = 8;

        //Position of Life Fonts on HUD
        private const int LIFE_Y_Font = 4;
        private const int SCALE = 3;

        //Position of Shield Fonts on HUD
        private const int SHIELD_Y_Font = 90;

        //Position of Shield on HUD
        private const int S_Y = 76;
        private const int S_X = 980;

        private const float LAYER = 0.9f;

        public DrawPlayerStat(Game1 game)
        {
            this.game = game;
            this.full = this.game.Player.GetMaxHealth();
            this.half = 0;
            this.empty = 0;
            this.fullHeart = SpriteFactory.Instance.GetFullHeart();
            this.halfHeart = SpriteFactory.Instance.GetHalfHeart();
            this.emptyHeart = SpriteFactory.Instance.GetEmptyHeart();
            this.shield = SpriteFactory.Instance.GetShieldItem();
            this.fontLife = SpriteFactory.Instance.GetFontLife();
            this.fontShield = SpriteFactory.Instance.GetFontShield();
            destinationRectangle = new Rectangle(X, Y, DEST_SIZE, DEST_SIZE);
            lifeFontRectangle = new Rectangle(X, LIFE_Y_Font, fontLife.Width*SCALE, fontLife.Height*SCALE);
            shieldFontRectangle = new Rectangle(X, SHIELD_Y_Font, fontShield.Width * SCALE, fontShield.Height * SCALE);
            shieldDestinationRectangle = new Rectangle(S_X, S_Y, DEST_SIZE, DEST_SIZE*2);
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
        /// Draw health point & shield point  on HUD.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        /// <param name="color"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            //Draw font "-Life-"
            Rectangle sourceRectangle = new Rectangle(0, 0, fontLife.Width, fontLife.Height);
            spriteBatch.Draw(fontLife, lifeFontRectangle, sourceRectangle, color, 
                0f, Vector2.Zero, SpriteEffects.None, LAYER);

            //Draw font "-Shield-"
            sourceRectangle = new Rectangle(0, 0, fontShield.Width, fontShield.Height);
            spriteBatch.Draw(fontShield, shieldFontRectangle, sourceRectangle, color,
                0f, Vector2.Zero, SpriteEffects.None, LAYER);

            //Draw Shield, depend on Player's stat
            sourceRectangle = new Rectangle(0, 0, shield.Width, shield.Height); //reset source rectangle.
            if (!game.Player.isShield())
            {
                spriteBatch.Draw(shield, shieldDestinationRectangle, sourceRectangle, Color.Gray,
                    0f, Vector2.Zero, SpriteEffects.None, LAYER);
            }
            else
            {
                spriteBatch.Draw(shield, shieldDestinationRectangle, sourceRectangle, color,
                    0f, Vector2.Zero, SpriteEffects.None, LAYER);
            }
            //Draw Hearts
            drawHeart(spriteBatch,color);
        }

        private void drawHeart(SpriteBatch spriteBatch, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, SRC_SIZE, SRC_SIZE); //reset source rectangle.

            //draw full hearts
            for (int i = 1; i < full + 1; i++)
            {
                spriteBatch.Draw(fullHeart, destinationRectangle, sourceRectangle, color,
                    0f, Vector2.Zero, SpriteEffects.None, LAYER);
                destinationRectangle = new Rectangle(X + DEST_SIZE * i, Y, DEST_SIZE, DEST_SIZE);
            }
            int destinationRectangleForHalf = destinationRectangle.X; //this is not true if there is no full hearts.

            //draw half hearts
            for (int i = 1; i < half + 1; i++)
            {
                spriteBatch.Draw(halfHeart, destinationRectangle, sourceRectangle, color,
                    0f, Vector2.Zero, SpriteEffects.None, LAYER);
                destinationRectangle = new Rectangle(destinationRectangleForHalf + DEST_SIZE * i, Y, DEST_SIZE, DEST_SIZE);
            }
            int destinationRectangleForEmpty = destinationRectangle.X;

            //draw empty hearts
            for (int i = 1; i < empty + 1; i++)
            {
                spriteBatch.Draw(emptyHeart, destinationRectangle, sourceRectangle, color,
                    0f, Vector2.Zero, SpriteEffects.None, LAYER);
                destinationRectangle = new Rectangle(destinationRectangleForEmpty + DEST_SIZE * i, Y, DEST_SIZE, DEST_SIZE);
            }
            destinationRectangle = new Rectangle(X, Y, DEST_SIZE, DEST_SIZE); //reset the dest rectangle
        }
    }
    
}
