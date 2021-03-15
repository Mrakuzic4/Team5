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
            //if delay time has elapsed
            /* if (stopwatch.ElapsedMilliseconds > delay)
            {
                if (game.ItemHolder == game.firewallFirst)
                {
                    game.ItemHolder = game.bombFirst;
                }
                else if (game.ItemHolder == game.bombFirst)
                {
                    game.ItemHolder = game.throwingKnifeFirst;
                }
                else
                {
                    game.ItemHolder = game.firewallFirst;
                }
                stopwatch.Restart();
            }
            */
            
        }
    }
}
