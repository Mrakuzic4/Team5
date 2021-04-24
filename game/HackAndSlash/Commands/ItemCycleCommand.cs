using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class ItemCycleCommand : ICommand
    {
        private Game1 game;
        private Stopwatch stopwatch;
        private long delay = GlobalSettings.DELAY_TIME;
        public ItemCycleCommand(Game1 game)
        {
            this.game = game;
            stopwatch = new Stopwatch();
            stopwatch.Restart();
        }
        public void execute()
        {
            if (game._DevMode)
            {
                //if delay time has elapsed
                if (game.useableItemList.Count() > 0 && stopwatch.ElapsedMilliseconds > delay)
                {
                    IItem temp = game.useableItemList[0];
                    int count = game.useableItemList.Count();
                    for (int i = 1; i < count; i++)
                    {
                        game.useableItemList[i - 1] = game.useableItemList[i];
                    }
                    game.useableItemList[count - 1] = temp;

                    stopwatch.Restart();
                }
            }
            
        }
    }
}
