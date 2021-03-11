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
        //private Player player;
        private Game1 game;

        public PlayerCollisionDetector(Player player, Game1 game)
        {
            //Is the Player reference necessary???????????????????
           // this.player = player;
            this.game = game;
        }

        public CollisionType CheckBlockCollisions(Rectangle playerHitbox)
        {

            //check if player is colliding with each block in the current room
            //foreach (IBlock block in game.blockList)
            //{
                IBlock block = game.BlockHolder;
                Rectangle collisionBox = Rectangle.Intersect(playerHitbox, block.rectangle);

                //left or right intersection
                if(collisionBox.Height > collisionBox.Width)
                {
                    //right collision
                    if (collisionBox.X > playerHitbox.X)
                    {
                        return GlobalSettings.CollisionType.Right;
                    }
                    //left collision
                    else
                    {
                        return GlobalSettings.CollisionType.Left;
                    }
                }
                //top or bottom collision
                else if (collisionBox.Height < collisionBox.Width)
                {
                    //top collision
                    if (collisionBox.Y > playerHitbox.Y)
                    {
                        return GlobalSettings.CollisionType.Top;
                    }
                    //bottom collision
                    else
                    {
                        return GlobalSettings.CollisionType.Bottom;
                    }
                }
            //}
            //no collision detected
            return GlobalSettings.CollisionType.None;
        }

        public CollisionType CheckEnemyCollisions(Rectangle playerHitbox)
        {

            //check if player is colliding with each block in the current room
            //foreach (IBlock block in game.blockList)
            //{
            IBlock block = game.BlockHolder;
            Rectangle collisionBox = Rectangle.Intersect(playerHitbox, block.rectangle);

            //left or right intersection
            if (collisionBox.Height > collisionBox.Width)
            {
                //right collision
                if (collisionBox.X > playerHitbox.X)
                {
                    return GlobalSettings.CollisionType.Right;
                }
                //left collision
                else
                {
                    return GlobalSettings.CollisionType.Left;
                }
            }
            //top or bottom collision
            else if (collisionBox.Height < collisionBox.Width)
            {
                //top collision
                if (collisionBox.Y > playerHitbox.Y)
                {
                    return GlobalSettings.CollisionType.Top;
                }
                //bottom collision
                else
                {
                    return GlobalSettings.CollisionType.Bottom;
                }
            }
            //}
            //no collision detected
            return GlobalSettings.CollisionType.None;
        }
    }
}
