using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (game.levelCycleRecord.currentLocationIndex.SequenceEqual(new int[] { 2, 0 }))
            {   // Old man room 
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
                        game.currentLevel.OpenMysDoor((int)GlobalSettings.Direction.Left);
                }
            }

        }

        public void Draw()
        {

        }
    }
}

    

