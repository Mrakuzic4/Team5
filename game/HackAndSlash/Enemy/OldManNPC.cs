using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HackAndSlash.Collision;

namespace HackAndSlash
{
    class OldManNPC : IEnemy
    {
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch
        public Rectangle rectangle { get; set; }
        private Vector2 position; // the position of the enemy on screen

        private Texture2D oldManSprite; 

        private Color tintColor = Color.White;

        public OldManNPC(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch spriteBatch, Game1 game) 
        {
            position = new Vector2 (startPosition.X, startPosition.Y);
            Graphics = graphics;
            this.spriteBatch = spriteBatch;

            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            oldManSprite = SpriteFactory.Instance.GetOldMan(); 
        }
             

        public void Update(GameTime gametime)
        {

        }
        //gametime passed in for frame rate

        //Draw method for each of the enemies
        public void Draw()
        {
            spriteBatch.Draw(oldManSprite, position, null,
                tintColor, 0f, Vector2.Zero, 4, SpriteEffects.None, 1f); 
        }

        //changing the state of the enemy to be idle
        public void changeToIdle()
        {

        }

        public void damage()
        {       

        }

        //changing the state of the enemy to Not - meaning the enemy is not currently updated or drawn
        public void changeToNot()
        {

        }

        public void changeToMoveLeft()
        {

        }
        public void changeToMoveRight()
        {

        }
        public void changeToMoveUp()
        {

        }
        public void changeToMoveDown()
        {

        }
        public void changeToDie()
        {

        }

        public Vector2 GetPos()
        {
            return position;
        }

        public GlobalSettings.Direction GetDirection()
        {
            return GlobalSettings.Direction.Right;
        }

        public void SetPos(Vector2 pos)
        {
            this.position = pos;
        }

        //return current rectangle of enemy
        public Rectangle getRectangle()
        {
            return new Rectangle();
        }
    }
}
