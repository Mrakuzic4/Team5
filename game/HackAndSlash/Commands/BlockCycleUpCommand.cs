using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class BlockCycleUpCommand : ICommand
    {
        private Game1 Game;
        private List<IBlock> BlockList;
        private Stopwatch stopwatch;
        private long delay = GlobalSettings.DELAY_KEYBOARD;

        //constructor which starts stopwatch for delay
        public BlockCycleUpCommand(Game1 game, List<IBlock> blockList)
        {
            this.Game = game;
            this.BlockList = blockList;
            stopwatch = new Stopwatch();
            stopwatch.Restart();
        }
        public void execute()
        {
            //if delay time has elasped
            if (stopwatch.ElapsedMilliseconds > delay)
            {
                //set blockholder to first block in list if at the end of the list
                if (BlockList.IndexOf(Game.BlockHolder) == (BlockList.Count - 1))
                {
                    Game.BlockHolder = BlockList[0];
                }
                //move blockholder up one block in list
                else
                {
                    Game.BlockHolder = BlockList[BlockList.IndexOf(Game.BlockHolder) + 1];
                }
                stopwatch.Restart();
            }
        }
    }
}
