using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace HackAndSlash
{
    class MaxKnockback
    {
        private int direction;           // The direction towards which being knocked back 
        private Vector2 playerPosition; 
        public int distance { get; } 

        public MaxKnockback(Game1 game, Vector2 PP, int Dir)
        {
            direction = Dir;  
            playerPosition = PP;

            int HoriDivision, VertDivision, temp = GlobalSettings.WINDOW_HEIGHT + GlobalSettings.WINDOW_WIDTH;
            int MinLength = GlobalSettings.WINDOW_HEIGHT + GlobalSettings.WINDOW_WIDTH; 
            bool HorizontalKnockback = (Dir == (int)GlobalSettings.Direction.Left || Dir == (int)GlobalSettings.Direction.Right);
            bool VerticalKnockback = (Dir == (int)GlobalSettings.Direction.Up || Dir == (int)GlobalSettings.Direction.Down);


            foreach (IBlock block in game.blockList)
            {
                if(block is BlockInvis)
                {
                    BlockInvis BlockExaminer = (BlockInvis)block;
                    Vector2 BlockPosition = new Vector2(BlockExaminer.rectangle.X, BlockExaminer.rectangle.Y);
                    HoriDivision = (int)(playerPosition.Y / GlobalSettings.BASE_SCALAR);
                    VertDivision = (int)(playerPosition.X / GlobalSettings.BASE_SCALAR);

                    if (HorizontalKnockback && (BlockPosition.Y > HoriDivision * GlobalSettings.BASE_SCALAR) &&
                            (BlockPosition.Y < (HoriDivision + 2) * GlobalSettings.BASE_SCALAR)) {

                        if (direction == (int)GlobalSettings.Direction.Right &&
                            BlockPosition.X + GlobalSettings.BASE_SCALAR <= playerPosition.X) {
                            temp = (int)(playerPosition.X - BlockPosition.X - GlobalSettings.BASE_SCALAR); 
                        }
                        else if  (direction == (int)GlobalSettings.Direction.Left &&
                            BlockPosition.X >= playerPosition.X + GlobalSettings.BASE_SCALAR) {
                            temp = (int)(BlockPosition.X - GlobalSettings.BASE_SCALAR - playerPosition.X);
                        }
                    } 
                    else if(VerticalKnockback && (playerPosition.X > VertDivision * GlobalSettings.BASE_SCALAR) &&
                        (playerPosition.X < (VertDivision + 2) * GlobalSettings.BASE_SCALAR)) {

                        if (direction == (int)GlobalSettings.Direction.Down &&
                            BlockPosition.Y + GlobalSettings.BASE_SCALAR <= playerPosition.Y) {
                            temp = (int)(playerPosition.Y - BlockPosition.Y - GlobalSettings.BASE_SCALAR);
                        }
                        else if (direction == (int)GlobalSettings.Direction.Up &&
                            BlockPosition.Y >= playerPosition.Y + GlobalSettings.BASE_SCALAR) {
                            temp = (int)(BlockPosition.Y - playerPosition.Y - GlobalSettings.BASE_SCALAR); 
                        }
                    }
                    if (temp < MinLength) MinLength = temp; 

                }
            }
            distance = MinLength;
        }

    }
}
