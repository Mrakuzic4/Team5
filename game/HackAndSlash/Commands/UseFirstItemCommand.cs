using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseFirstItemCommand : ICommand
    {
        private Game1 game;
        public UseFirstItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.itemList[0];
            game.Player.UseItem();
        }
    }
}