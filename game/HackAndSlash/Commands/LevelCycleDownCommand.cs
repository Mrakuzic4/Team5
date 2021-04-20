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
            if (Game._EnableMouseTeleport)
            {
                if (Game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Left))
                {
                    Game.currentLevel.TransDir = (int)GlobalSettings.Direction.Left;
                    Game.reset((int)GlobalSettings.Direction.Left);
                }
                else if (Game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Right))
                {
                    Game.currentLevel.TransDir = (int)GlobalSettings.Direction.Right;
                    Game.reset((int)GlobalSettings.Direction.Right);
                }
            }
        }
    }
}
