using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class ItemUseableCommand : ICommand
    {
        private Game1 game;
        public ItemUseableCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            foreach (IItem item in game.itemList) item.ChangeToUseable();
        }
    }
}
