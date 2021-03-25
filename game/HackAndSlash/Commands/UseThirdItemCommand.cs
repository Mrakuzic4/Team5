using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseThirdItemCommand : ICommand
    {
        private Game1 game;
        public UseThirdItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.itemList[2];
            game.Player.UseItem();
        }
    }
}