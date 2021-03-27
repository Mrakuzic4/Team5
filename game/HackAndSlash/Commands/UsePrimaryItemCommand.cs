using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UsePrimaryItemCommand : ICommand
    {
        private Game1 game;
        public UsePrimaryItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.useableItemList[0];
            game.Player.UseItem();
        }
    }
}