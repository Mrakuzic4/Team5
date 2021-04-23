using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class NightmareModeCommand : ICommand
    {
        private Game1 Game;

        public NightmareModeCommand (Game1 game)
        {
            this.Game = game;
        }
        public void execute()
        {
            SoundFactory.Instance.NightmareMode();
            GlobalSettings.NIGHTMAREMODE = true;
        }
    }
}
