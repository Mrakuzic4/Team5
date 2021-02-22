using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class BlockCycleUpCommand : ICommand
    {
        private Game1 Game;
        private List<IBlock> BlockList;
        public BlockCycleUpCommand(Game1 game, List<IBlock> blockList)
        {
            this.Game = game;
            this.BlockList = blockList;
        }
        public void execute()
        {
            if (BlockList.IndexOf(Game.BlockHolder) == (BlockList.Count - 1))
            {
                Game.BlockHolder = BlockList[0];
            } 
            else
            {
                Game.BlockHolder = BlockList[BlockList.IndexOf(Game.BlockHolder) + 1];
            }
        }
    }
}
