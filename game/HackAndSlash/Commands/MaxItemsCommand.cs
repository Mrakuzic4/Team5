using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class MaxItemsCommand : ICommand
    {
        private Game1 Game;
        public MaxItemsCommand(Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            foreach (IItem item in Game.useableItemList)
            {
                 if (!(item is RupyItem)) item.SetMax();
            }
            Game.cheatText.activeText = CheatText.cheats.MAX_ITEMS;
        }
    }
}
