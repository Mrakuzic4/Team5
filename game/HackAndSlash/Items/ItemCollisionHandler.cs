using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class ItemCollisionHandler
    {
        private const int LEFT_WALL_POSITION = 64;
        private const int TOP_WALL_POSITION = 64;
        private const int RIGHT_WALL_POSITION = GlobalSettings.WINDOW_WIDTH - 128;
        private const int BOTTOM_WALL_POSITION = GlobalSettings.WINDOW_HEIGHT - 128;
        private CollisionID[] collisionIndexAndType;
        private enum CollisionType { Enemy, Player, Block, Wall };

        private IPlayer player;

        // constructor
        public ItemCollisionHandler(IPlayer player)
        {
            this.player = player;
        }
        // Methods
        // Check for collision calls all other checks, passed collidable tiles list
        public void CheckForCollision(Vector2[] collidableItemTiles)
        {
            CheckForWall(collidableItemTiles);
            return;
        }
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
            // chcek for enemy (damage enemy once [cooldown in enemy?], no stops)
            // check for player (pickup if collectable)
        public bool CheckForPlayerCollision(Vector2[] collidableItemTiles)
        {
            bool collidingWithPlayer = false;
            foreach (Vector2 itemTile in collidableItemTiles)
            {
                if (itemTile.Equals(player.GetPos())) //Player position reference!
                {
                    collidingWithPlayer = true;
                }
            }
            return collidingWithPlayer;
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