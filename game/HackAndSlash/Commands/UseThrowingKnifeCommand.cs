using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseThrowingKnifeCommand : ICommand
    {
        private Game1 game;
        public UseThrowingKnifeCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.throwingKnifeFirst;
            game.Player.UseItem();
        }
    }
}