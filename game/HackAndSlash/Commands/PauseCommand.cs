using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class PauseCommand : ICommand
    {
        private Game1 game;

        public PauseCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.GameState = GlobalSettings.GameStates.Paused;
        }
    }
}
