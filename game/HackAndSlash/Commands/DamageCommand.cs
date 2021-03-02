using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class DamageCommand : ICommand
    {
        private Game1 game;

        public DamageCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.Player.Damaged();
        }
    }
}
