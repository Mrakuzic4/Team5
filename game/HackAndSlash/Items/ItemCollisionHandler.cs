using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class ItemCollisionHandler
    {
        private Rectangle[] WALL_RECTANGLES = { 
            new Rectangle(0, 0, GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_HEIGHT), 
            new Rectangle(0, 0, GlobalSettings.WINDOW_WIDTH, GlobalSettings.BORDER_OFFSET), 
            new Rectangle(GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET, 0, GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_HEIGHT), 
            new Rectangle(0, GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_WIDTH, GlobalSettings.BORDER_OFFSET) };

        private IPlayer player;

        // constructor
        public ItemCollisionHandler(IPlayer player)
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
        public bool CheckForBlock(Rectangle checkTile)
        {
            bool collidingWithBlock = false;
            Rectangle[] blockPositions = { new Rectangle(128, 128, 64, 64) }; // TODO: get blocks location somehow
            foreach (Rectangle blockTile in blockPositions)
            {
                if (checkTile.Intersects(blockTile))
                {
                    collidingWithBlock = true;
                }
            }
            return collidingWithBlock;
        }
        // chcek for enemy (damage enemy once [cooldown in enemy?], no stops)
        // check for player (pickup if collectable)
        public bool CheckForPlayerCollision(Rectangle[] collidableItemTiles)
        {
            bool collidingWithPlayer = false;
            foreach (Rectangle itemTile in collidableItemTiles)
            {
                if (itemTile.Intersects(new Rectangle((int)player.GetPos().X, (int)player.GetPos().Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR))) //Player position reference!
                {
                    collidingWithPlayer = true;
                }
            }
            return collidingWithPlayer;
        }

        // Check for enemy collision is handled in enemy ItemEnemyCollisionHandler
    }
}