using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HackAndSlash
{
    public class ItemUseableCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopwatch;
        private long delay = GlobalSettings.DELAY_TIME;

        public ItemUseableCommand(Game1 game)
        {
            this.game = game;
            stopwatch = new Stopwatch();
            stopwatch.Restart();
        }
        public void execute()
        {
            if (game._DevMode)
            {
                if (stopwatch.ElapsedMilliseconds > delay)
                {
                    foreach (IItem item in game.itemList) item.CollectItem();
                }
                stopwatch.Restart();
            }
        }
    }
}
