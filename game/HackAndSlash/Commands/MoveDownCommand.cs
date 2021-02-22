using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveDownCommand : ICommand
    {
        private IPlayer Player;

        public MoveDownCommand( IPlayer Player)
        {
            this.Player = Player;
        }
        public void execute()
        {
                //move driection out
                Player.ChangeDirection(GlobalSettings.Direction.Down);//face down
                Player.Move();
           
        }
    }
}
