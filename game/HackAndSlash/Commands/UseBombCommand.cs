using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseBombCommand : ICommand
    {
        private Game1 game;
        public UseBombCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.bombFirst;
            game.Player.UseItem();
        }
    }
}