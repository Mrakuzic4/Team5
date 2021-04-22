using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class MaxRupeesCommand : ICommand
    {
        Game1 Game;
        public MaxRupeesCommand(Game1 game)
        {
            this.Game = game;
        }

        public void execute()
        {
            foreach (IItem item in Game.itemList)
            {
                if (item is RupyItem) item.SetMax();
                Game.cheatText.activeText = CheatText.cheats.MAX_RUPEES;
            }
        }
    }
}
