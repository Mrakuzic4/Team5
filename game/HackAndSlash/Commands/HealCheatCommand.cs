using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class HealCheatCommand : ICommand
    {
        private Game1 Game;
        public HealCheatCommand(Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            Game.Player.Healed();
            Game.cheatText.activeText = CheatText.cheats.HEAL;
        }
    }
}
