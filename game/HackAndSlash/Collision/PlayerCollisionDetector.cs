using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HackAndSlash.GlobalSettings;

namespace HackAndSlash
{
    class PlayerCollisionDetector
    {
        private Player player;
        private Game1 game;
        private IEnemy enemy; //the enemy that takes damage.

        public PlayerCollisionDetector(Player player, Game1 game)
        {
            this.game = game;
            this.player = player;
        }
        /// <summary>
        /// Check the collision between player and blocks.
        /// </summary>
        /// <param name="playerHitbox"></param>
        /// <returns>Collision side</returns>
        public CollisionType CheckBlockCollisions(Rectangle playerHitbox)
        {
            //initialize the type with no collision detected
            CollisionType type = CollisionType.None;

            //check if player is colliding with each block in the current room
            foreach (IBlock block in game.blockList)
            {
                Rectangle collisionBox = Rectangle.Intersect(playerHitbox, block.rectangle);
                
                //Jump out of the loop when the collision is dectected 
                type = checkHorizontalCollision(collisionBox,playerHitbox);
                if (type != CollisionType.None) break; 
                type = checkVerticalCollision(collisionBox, playerHitbox);
                if (type != CollisionType.None) break;
            }
            return type;
        }
        /// <summary>
        /// Check the collision between the player and the enemies.
        /// </summary>
        /// <param name="playerHitbox"></param>
        /// <returns>Collision side</returns>
        public CollisionType CheckEnemyCollisions(Rectangle playerHitbox)
        {
            //initialize the type with no collision detected
            CollisionType type = CollisionType.None;
            //check if player is colliding with each block in the current room
            foreach (IEnemy enemy in game.enemyList)
            {
                Rectangle collisionBox = Rectangle.Intersect(playerHitbox, enemy.getRectangle());

                //Jump out of the loop when the collision is dectected 
                type = checkHorizontalCollision(collisionBox, playerHitbox);
                if (type != CollisionType.None)
                {
                    this.enemy = enemy; //Set enemy that collides with sword.
                    break;
                }
                type = checkVerticalCollision(collisionBox, playerHitbox);
                if (type != CollisionType.None)
                {
                    this.enemy = enemy; //Set enemy that collides with sword.
                    break;
                }
            }
            return type;
        }
        /// <summary>
        /// Check the collision between player's sword and enemies
        /// </summary>
        /// <returns>Collision side</returns>
        public IEnemy CheckSwordEnemyCollisions()
        {
            //parameters for the sword hitbox
            Rectangle playerSwordHitBox;
            int middle = GlobalSettings.BASE_SCALAR / 2;
            int thinnerSideOfSword = GlobalSettings.BASE_SCALAR / 3; //the hitbox of the sword would be a rectangle instead of a square, this is the parameter for the narrower part of the rectangle.

            //Player sword collides with enemies
            switch (player.GetDir())
            {
                //width and height set of the sword hitbox set to Base_SCALAR, Base_SCALAR/2
                case GlobalSettings.Direction.Right:
                    //Player's sword hitbox is located at the hand of the player
                    playerSwordHitBox = new Rectangle((int) game.Player.GetPos().X + GlobalSettings.BASE_SCALAR, (int)game.Player.GetPos().Y + middle, GlobalSettings.BASE_SCALAR, thinnerSideOfSword);
                    break;
                case GlobalSettings.Direction.Left:
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X - GlobalSettings.BASE_SCALAR, (int)game.Player.GetPos().Y + middle, GlobalSettings.BASE_SCALAR, thinnerSideOfSword);
                    break;
                case GlobalSettings.Direction.Up:
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X + middle, (int)game.Player.GetPos().Y - GlobalSettings.BASE_SCALAR, thinnerSideOfSword, GlobalSettings.BASE_SCALAR);
                    break;
                default:
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X + middle, (int)game.Player.GetPos().Y + GlobalSettings.BASE_SCALAR, thinnerSideOfSword, GlobalSettings.BASE_SCALAR);
                    break;
            }
            CheckEnemyCollisions(playerSwordHitBox);
            return this.enemy; //It is safe to return null value becuase it is handled in the collision handler.
        }


        /// <summary>
        /// left or right intersection
        /// </summary>
        /// <returns>Collision Side</returns>
        private CollisionType checkHorizontalCollision(Rectangle collisionBox, Rectangle playerHitbox)
        {
            if (collisionBox.Height > collisionBox.Width)
            {
                if (collisionBox.X > playerHitbox.X)
                {
                    return GlobalSettings.CollisionType.Right;
                }
                else
                {
                    return GlobalSettings.CollisionType.Left;
                }
            }
            return GlobalSettings.CollisionType.None;
        }

        /// <summary>
        /// top or bottom intersection
        /// </summary>
        /// <returns>Collision Side</returns>
        private CollisionType checkVerticalCollision(Rectangle collisionBox, Rectangle playerHitbox)
        {
            if (collisionBox.Height < collisionBox.Width)
            {
                if (collisionBox.Y > playerHitbox.Y)
                {
                    return GlobalSettings.CollisionType.Top;
                }
                else
                {
                    return GlobalSettings.CollisionType.Bottom;
                }
            }
            return GlobalSettings.CollisionType.None;
        }

        
    }
}
