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
            if (Game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Up))
            {
                Game.currentLevel.TransDir = (int)GlobalSettings.Direction.Up;
                Game.reset((int)GlobalSettings.Direction.Up);
            }
            else if (Game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Down))
            {
                Game.currentLevel.TransDir = (int)GlobalSettings.Direction.Down;
                Game.reset((int)GlobalSettings.Direction.Down);
            }
        }
    }
}
