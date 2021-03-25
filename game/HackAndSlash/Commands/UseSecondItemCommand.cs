using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseSecondItemCommand : ICommand
    {
        private Game1 game;
        public UseSecondItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.itemList[1];
            game.Player.UseItem();
        }
    }
}