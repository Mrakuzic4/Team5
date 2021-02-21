using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class BlockCycleDownCommand : ICommand
    {
        private Game1 Game;
        private List<IBlock> BlockList;
        public BlockCycleDownCommand(Game1 game, List<IBlock> blockList)
        {
            this.Game = game;
            this.BlockList = blockList;
        }
        public void execute()
        {
            if (BlockList.IndexOf(Game.BlockHolder) == 0)
            {
                Game.BlockHolder = BlockList[BlockList.Count - 1];
            }
            else
            {
                Game.BlockHolder = BlockList[BlockList.IndexOf(Game.BlockHolder) - 1];
            }
        }
    }
}
