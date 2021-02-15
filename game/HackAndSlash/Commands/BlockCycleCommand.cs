using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash.Commands
{
    public class BlockCycleCommand : ICommand
    {
        private Game1 game;
        public BlockCycleCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change "BlockHolder"(?) field in primary game class
        }
    }
}
