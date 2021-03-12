using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HackAndSlash.GlobalSettings;
using Microsoft.Xna.Framework;

namespace HackAndSlash.Collision
{
    class EnemyCollisionDetector
    {
        private Game1 game;

        public EnemyCollisionDetector(Game1 game, IEnemy enemy)
        {
            this.game = game;
        }

        public CollisionType CheckBlockCollisions(Rectangle hitbox)
        {
            //System.Diagnostics.Debug.WriteLine("Hitbox: " + hitbox.X + "," + hitbox.Y);
            //check if enemy is colliding with each block in the current room
            foreach (IBlock block in game.blockList)
            {
                Rectangle collision = Rectangle.Intersect(hitbox, block.rectangle);
                //System.Diagnostics.Debug.WriteLine("Rectangle: " + block.rectangle.X + "," + block.rectangle.Y);
                //left or right intersection
                if (collision.Height > collision.Width)
                {
                    //right collision
                    if (collision.X >= hitbox.X)
                    {
                        System.Diagnostics.Debug.WriteLine("right collision detected");
                        return GlobalSettings.CollisionType.Right;
                    }
                    //left collision
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Left collision detected");
                        return GlobalSettings.CollisionType.Left;
                    }
                }
                //top or bottom collision
                else if (collision.Height < collision.Width)
                {
                    //top collision
                    if (collision.Y >= hitbox.Y)
                    {
                        System.Diagnostics.Debug.WriteLine("top collision detected");
                        return GlobalSettings.CollisionType.Top;
                    }
                    //bottom collision
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("bottom collision detected");
                        return GlobalSettings.CollisionType.Bottom;
                    }
                }
            }
            //no collision detected
            return GlobalSettings.CollisionType.None;
        }
    }
}
