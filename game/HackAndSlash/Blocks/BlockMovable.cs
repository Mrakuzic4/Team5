using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class BlockMovable : IBlock
    {
        private Texture2D block;
        private Vector2 location { get; set; }
        private SpriteBatch spriteBatch;
        private bool vertical; //if true block moves vertically, if false block moves horizontally
        private bool isMoving; //holds whether the block is currently moving
        private bool hasMoved; //holds whether the block has already been moved or not
        private int counter;
        private GlobalSettings.CollisionType type { get; set; }
        public Rectangle rectangle { get; set; }

        public BlockMovable(Texture2D block, Vector2 location, SpriteBatch spriteBatch, bool vertical)
        {
            this.spriteBatch = spriteBatch;
            this.location = location;
            this.block = block;
            this.vertical = vertical;
            isMoving = false;
            hasMoved = false;
            counter = 0;
            type = GlobalSettings.CollisionType.None;
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        //handles movement of the block, locks the blocks movement after it has already moved 1 tile
        public void Update()
        {
            if (hasMoved == false && isMoving == true)
            {
                if (vertical == true) HandleVerticalCollision();
                else HandleHorizontalCollision();
                counter++;
                if (counter == 64) hasMoved = true;
            }
        }
        public void Draw()
        {
            spriteBatch.Draw(block, location, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        //checks to confirm the collision type corresponds with the blocks movement type(vertical/horizontal)
        public void ChangeToMoving(GlobalSettings.CollisionType type)
        {
            if ((vertical == true && (type == GlobalSettings.CollisionType.Top || type == GlobalSettings.CollisionType.Bottom)) ||
                (vertical == false && (type == GlobalSettings.CollisionType.Left || type == GlobalSettings.CollisionType.Right)))
            {
                isMoving = true;
                this.type = type;
            }
            
        }

        //updates the block location 1 pixel vertically each frame
        private void HandleVerticalCollision()
        {
            if (type == GlobalSettings.CollisionType.Top)
            {
                location = new Vector2(location.X, location.Y+1);
            }
            else
            {
                location = new Vector2(location.X, location.Y-1);
            }
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        //updates the block location 1 pixel horizontally each frame
        private void HandleHorizontalCollision() 
        {
            if (type == GlobalSettings.CollisionType.Left)
            {
                location = new Vector2(location.X - 1, location.Y);
            }
            else
            {
                location = new Vector2(location.X + 1, location.Y);
            }
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

    }
}
