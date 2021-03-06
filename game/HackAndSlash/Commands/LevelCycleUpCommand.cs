using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class LevelCycleUpCommand : ICommand
    {
        private Game1 Game;

        public LevelCycleUpCommand(Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            //cycle up the currently displayed level
        }
    }
}
