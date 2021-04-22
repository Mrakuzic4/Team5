
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class DrawItemCD : ISprite
    {
        //CD BAR
        Texture2D bar;

        private Rectangle sourceRectangle; //position of the BAR that is to be drawn
        private Rectangle destinationRectangle; //position of the BAR that is to be drawn

        private const int BAR_X = 260;
        private const int BAR_Y = 98;
        private const int DIVISOR = 126;
        private const int CD = 380;
        private const float LAYER = 0.9f;

        private int timer;
        private int COUNT; //CoolDown

        //Singleton
        private static DrawItemCD instance = new DrawItemCD();
        public static DrawItemCD Instance
        {
            get
            {
                return instance;
            }
        }
        /// <summary>
        /// CD only triggers after using item.
        /// </summary>
        /// <param name="game"></param>
        private DrawItemCD()
        {
            this.bar = SpriteFactory.Instance.GetBar();
            sourceRectangle = new Rectangle(0, 0, bar.Width, bar.Height); //reset source rectangle.
            timer = 0;
        }
        public void setCD()
        {
            this.timer = CD;
        }

        /// <summary>
        /// Update Item CD
        /// </summary>
        public void Update()
        {
            if (timer != 0) timer--;
            COUNT = timer / DIVISOR;
        }

        /// <summary>
        /// Draw Item CD on HUD.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="location"></param>
        /// <param name="color"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            destinationRectangle = new Rectangle(BAR_X, BAR_Y, bar.Width, bar.Height); //reset source rectangle.
            for (int i = 1; i < COUNT + 1; i++)
            {
                spriteBatch.Draw(bar, destinationRectangle, sourceRectangle, color,
       0f, Vector2.Zero, SpriteEffects.None, LAYER);
                destinationRectangle = new Rectangle(BAR_X + bar.Width * i, BAR_Y, bar.Width, bar.Height); //reset source rectangle.
            }
        }
    }
    
}
