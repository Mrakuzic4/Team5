using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class SoundMenuCommand : ICommand
    {
        private Game1 game;

        public SoundMenuCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.GameState = GlobalSettings.GameStates.SoundMenu;
        }
    }
}
