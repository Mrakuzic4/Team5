using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class ItemCycleCommand : ICommand
    {
        private Game1 game;
        public ItemCycleCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change "ItemHolder"(?) field in primary game class
        }
    }
}
