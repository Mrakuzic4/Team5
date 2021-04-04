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
        private GlobalSettings.CollisionType collisionType { get; set; }
        public Rectangle rectangle { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BlockMovable(Texture2D block, Vector2 location, SpriteBatch spriteBatch, bool vertical)
        {
            this.spriteBatch = spriteBatch;
            this.location = location;
            this.block = block;
            this.vertical = vertical;
            isMoving = false;
            hasMoved = false;
            counter = 0;
            collisionType = GlobalSettings.CollisionType.None;
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        /// <summary>
        /// Handles updating the blocks location, locks the block if it has already been moved
        /// </summary>
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

        /// <summary>
        /// Changes block to its moving state, checks to confirm collision type corresponds with block movement(horizontal/vertical)
        /// </summary>
        public void ChangeToMoving(GlobalSettings.CollisionType type)
        {
            if ((vertical == true && (type == GlobalSettings.CollisionType.Top || type == GlobalSettings.CollisionType.Bottom)) ||
                (vertical == false && (type == GlobalSettings.CollisionType.Left || type == GlobalSettings.CollisionType.Right)))
            {
                isMoving = true;
                collisionType = type;
            }
            
        }

        /// <summary>
        /// Updates block location 1 pixel vertically per frame
        /// </summary>
        private void HandleVerticalCollision()
        {
            if (collisionType == GlobalSettings.CollisionType.Top)
            {
                location = new Vector2(location.X, location.Y+1);
            }
            else
            {
                location = new Vector2(location.X, location.Y-1);
            }
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        /// <summary>
        /// Updates block location 1 pixel horizontally per frame
        /// </summary>
        private void HandleHorizontalCollision() 
        {
            if (collisionType == GlobalSettings.CollisionType.Left)
            {
                location = new Vector2(location.X - 1, location.Y);
            }
            else
            {
                location = new Vector2(location.X + 1, location.Y);
            }
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        public bool Moved()
        {
            return hasMoved; 
        }

    }
}
