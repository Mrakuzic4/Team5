using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class EnemyCycleCommandBug : ICommand
    {
        private Game1 game;
        private int waitTime = 10000;
        public EnemyCycleCommandBug(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change statemachine of snake to not and bug to idle
            game.snakefirst.changeToNot();

            game.bugfirst.changeToIdle();
            
            

        }
    }
}