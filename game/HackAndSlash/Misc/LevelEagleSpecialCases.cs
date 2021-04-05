using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HackAndSlash
{
    /// <summary>
    /// Dealing with the special cases in level eagle 
    /// </summary>
    class LevelEagleSpecialCases : ISpecialCases
    {

        public LevelEagleSpecialCases()
        {

        }

        public void Update(Game1 game)
        {

            if (game.levelCycleRecord.currentLocationIndex.SequenceEqual(new int[] { 2, 1 }))
            {
                Room2_1(game);
            }

            

        }

        // Open mys door with block move 
        private void Room2_1(Game1 game)
        {

            foreach (IBlock block in game.blockList)
            {
                if (block is BlockMovable)
                {
                    BlockMovable BlockExaminer = (BlockMovable)block;
                    if (BlockExaminer.Moved())
                    {
                        Rectangle Position = BlockExaminer.rectangle;
                        int X = (Position.X - GlobalSettings.BORDER_OFFSET) / GlobalSettings.BASE_SCALAR;
                        int Y = (Position.Y - GlobalSettings.HEADSUP_DISPLAY - GlobalSettings.BORDER_OFFSET) / 
                            GlobalSettings.BASE_SCALAR;

                        game.currentLevel.OpenMysDoor((int)GlobalSettings.Direction.Left);
                        game.levelCycleRecord.RemoveOneIndex(GlobalSettings.HORIZONTAL_MOVE_BLOCK);

                        game.levelCycleRecord.currentMapSet[2, 1].Arrangement[Y, X] = 32; 
                    }
                }
            }

        }

        public void Draw()
        {

        }
    }
}

    

