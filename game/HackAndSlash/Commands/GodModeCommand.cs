using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class GodModeCommand : ICommand
    {
        private Game1 Game;

        public GodModeCommand (Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            GlobalSettings.GODMODE = true;
            Game.cheatText.activeText = CheatText.cheats.GODMODE;
        }
    }
}
