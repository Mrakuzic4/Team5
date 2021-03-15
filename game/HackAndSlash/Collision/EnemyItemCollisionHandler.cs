using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class EnemyItemCollisionHandler
    {
        private Rectangle[] WALL_RECTANGLES = {
            new Rectangle(0, 0, GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_HEIGHT),
            new Rectangle(0, 0, GlobalSettings.WINDOW_WIDTH, GlobalSettings.BORDER_OFFSET),
            new Rectangle(GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET, 0, GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_HEIGHT),
            new Rectangle(0, GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_WIDTH, GlobalSettings.BORDER_OFFSET) };

        private IPlayer player;

        // constructor
        public EnemyItemCollisionHandler(IPlayer player)
        {
            this.player = player;
        }
        // Methods

        // check for wall (stop unless bomb explosion)
        public bool CheckForWall(Rectangle checkTile)
        {
            bool collidesWithWall = false;
            foreach (Rectangle wall in WALL_RECTANGLES)
            {
                if (checkTile.Intersects(wall))
                {
                    collidesWithWall = true;
                }
            }
            return collidesWithWall;
        }

        // check for block (stop from placing or moving, unless bomb explosion)

        public bool CheckForBlock(Rectangle checkTile, List<IBlock> blockList)
        {
            bool collidingWithBlock = false;
            Rectangle blockTile;
            foreach (IBlock block in blockList)
            {
                blockTile = block.rectangle;
                if (checkTile.Intersects(blockTile))
                {
                    collidingWithBlock = true;
                }
            }
            return collidingWithBlock;
        }

        public bool CheckForPlayerCollision(Rectangle collidableItemTile)
        {
            bool collidingWithPlayer = false;
            
                if (collidableItemTile.Intersects(new Rectangle((int)this.player.GetPos().X, (int)this.player.GetPos().Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR))) //Player position reference!
                {
                    collidingWithPlayer = true;
                }
            
            return collidingWithPlayer;
        }

    }
}
