using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class ItemCollisionHandler
    {
        private const int LEFT_WALL_POSITION = 128;
        private const int TOP_WALL_POSITION = 128;
        private const int RIGHT_WALL_POSITION = GlobalSettings.WINDOW_WIDTH - 192;
        private const int BOTTOM_WALL_POSITION = GlobalSettings.WINDOW_HEIGHT - 192;
        private Rectangle[] WALL_RECTANGLES = { 
            new Rectangle(0, 0, GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_HEIGHT), 
            new Rectangle(0, 0, GlobalSettings.WINDOW_WIDTH, GlobalSettings.BORDER_OFFSET), 
            new Rectangle(GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET, 0, GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_HEIGHT), 
            new Rectangle(0, GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET, GlobalSettings.WINDOW_WIDTH, GlobalSettings.BORDER_OFFSET) };
        private CollisionID[] collisionIndexAndType;
        private enum CollisionType { Enemy, Player, Block, Wall };

        private IPlayer player;
        private Game game;

        // constructor
        public ItemCollisionHandler(IPlayer player)
        {
            this.player = player;
        }
        // Methods
        // check for wall (stop unless bomb explosion)
        public bool CheckForWall(Vector2[] collidableItemTiles)
        {
            collisionIndexAndType = new CollisionID[collidableItemTiles.Length];
            int i = 0;
            bool collidesWithWall = false;
            foreach (Vector2 itemTile in collidableItemTiles)
            {
                if (itemTile.X <= LEFT_WALL_POSITION)
                {
                    // left wall collision
                    // add to collided indicies list
                    collidesWithWall = true;
                } else if (itemTile.X >= RIGHT_WALL_POSITION)
                {
                    collisionIndexAndType[i] = new CollisionID(itemTile, CollisionType.Wall);
                    i++;

                    collidesWithWall = true;
                }
                if (itemTile.Y <= TOP_WALL_POSITION)
                {
                    collisionIndexAndType[i] = new CollisionID(itemTile, CollisionType.Wall);
                    i++;

                    collidesWithWall = true;
                } else if (itemTile.Y >= BOTTOM_WALL_POSITION)
                {
                    collisionIndexAndType[i] = new CollisionID(itemTile, CollisionType.Wall);
                    i++;

                    collidesWithWall = true;
                }
            }
            return collidesWithWall;
        }

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
        public bool CheckForBlock(Vector2[] collidableItemTiles)
        {
            bool collidingWithBlock = false;
            Vector2[] blockPositions = { new Vector2(0, 0) }; // TODO: get blocks location somehow
            foreach (Vector2 itemTile in collidableItemTiles)
            {
                foreach (Vector2 blockTile in blockPositions)
                {
                    if (itemTile.Equals(blockTile))
                    {
                        collidingWithBlock = true;
                    }
                }
            }
            return collidingWithBlock;
        }

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
                if (itemTile.Equals(player.GetPos())) //Player position reference!
                {
                    collidingWithPlayer = true;
                }
            }
            return collidingWithPlayer;
        }

        // Check for enemy collision
        public bool CheckForEnemy(Rectangle[] collidableItemTiles)
        {
            bool CollidesWithEnemy = false;
            foreach (Rectangle itemTile in collidableItemTiles)
            {
                
            }
            return CollidesWithEnemy;
        }
        // nested class to store colliding tiles and type
        private class CollisionID
        {
            public Vector2 collidingTile;
            public CollisionType collidedObject;
            public CollisionID(Vector2 collisionTile, CollisionType collisionType)
            {

            }
        }
    }
}