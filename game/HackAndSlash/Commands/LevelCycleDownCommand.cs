using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class LevelCycleDownCommand : ICommand
    {
        private Game1 Game;

        public LevelCycleDownCommand(Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            //cycle down the currently displayed level
        }
    }
}
