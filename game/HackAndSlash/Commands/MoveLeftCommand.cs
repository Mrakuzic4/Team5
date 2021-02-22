using HackAndSlash;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveLeftCommand : ICommand
    {

        private IPlayer Player;

        public MoveLeftCommand(IPlayer Player)
        {
            this.Player = Player;
        }
        public void execute()
        {        
            Player.ChangeDirection(0);//face left
            Player.Move();
         
        }
    }
}
