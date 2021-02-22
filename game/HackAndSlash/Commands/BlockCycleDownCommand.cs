using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class BlockCycleDownCommand : ICommand
    {
        private Game1 Game;
        private List<IBlock> BlockList;
        private Stopwatch stopwatch;
        private long delay = GlobalSettings.DELAY_KEYBOARD;

        //constructor which starts stopwatch for delay
        public BlockCycleDownCommand(Game1 game, List<IBlock> blockList)
        {
            this.Game = game;
            this.BlockList = blockList;
            stopwatch = new Stopwatch();
            stopwatch.Restart();
        }
        public void execute()
        {
            //if delay time has elapsed
            if (stopwatch.ElapsedMilliseconds > delay)
            {
                //set blockholder to last block in list if at the beginning of list
                if (BlockList.IndexOf(Game.BlockHolder) == 0)
                {
                    Game.BlockHolder = BlockList[BlockList.Count - 1];
                }
                //move blockholder down one block in list
                else
                {
                    Game.BlockHolder = BlockList[BlockList.IndexOf(Game.BlockHolder) - 1];
                }
                stopwatch.Restart();
            }
        }
    }
}
