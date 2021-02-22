using HackAndSlash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveUpCommand : ICommand
    {
        private IPlayer Player;

        public MoveUpCommand(IPlayer Player)
        {
            this.Player = Player;
        }
        public void execute()
        {
            Player.ChangeDirection(GlobalSettings.Direction.Up);//face Up
            Player.Move();
        }
    }
}
