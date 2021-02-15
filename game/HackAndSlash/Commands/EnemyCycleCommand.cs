using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash.Commands
{
    public class EnemyCycleCommand : ICommand
    {
        private Game1 game;
        public EnemyCycleCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change "EnemyHolder"(?) field in primary game class
        }
    }
}
