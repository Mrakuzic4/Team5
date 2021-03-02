using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveDownCommand : ICommand
    {
        private Game1 game;

        public MoveDownCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
                //move driection out
                game.Player.ChangeDirection(GlobalSettings.Direction.Down);//face down
                game.Player.Move();
           
        }
    }
}
