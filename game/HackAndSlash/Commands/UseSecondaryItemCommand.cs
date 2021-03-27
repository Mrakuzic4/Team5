using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseSecondaryItemCommand : ICommand
    {
        private Game1 game;
        public UseSecondaryItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.useableItemList[1];
            game.Player.UseItem();
        }
    }
}