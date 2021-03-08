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

        public PlayerCollisionDetector(Player player, Game1 game)
        {
            this.player = player;
            this.game = game;
        }

        public CollisionType CheckBlockCollisions(Rectangle hitbox)
        {
            //check if player is colliding with each block in the current room
            foreach (IBlock block in game.blockList)
            {
                Rectangle collision = Rectangle.Intersect(hitbox, block.rectangle);

                //left or right intersection
                if(collision.Height > collision.Width)
                {
                    //right collision
                    if (collision.X > hitbox.X)
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
                else if (collision.Height < collision.Width)
                {
                    //top collision
                    if (collision.Y > hitbox.Y)
                    {
                        return GlobalSettings.CollisionType.Top;
                    }
                    //bottom collision
                    else
                    {
                        return GlobalSettings.CollisionType.Bottom;
                    }
                }
            }
            //no collision detected
            return GlobalSettings.CollisionType.None;
        }
    }
}
