using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class EnemyCycleCommandSnake : ICommand
    {
        private Game1 game;
        private int delayTime= 10000;
        public EnemyCycleCommandSnake(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.bugfirst.changeToNot();
            //change statemachine of snake to idle and bug to not
            game.snakefirst.changeToIdle();
            
            
        }
    }
}
