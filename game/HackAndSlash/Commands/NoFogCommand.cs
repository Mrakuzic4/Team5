using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class NoFogCommand : ICommand
    {
        Game1 Game;

        public NoFogCommand(Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            Game._FOG = false;
            Game.cheatText.activeText = CheatText.cheats.NO_FOG;
        }
    }
}
