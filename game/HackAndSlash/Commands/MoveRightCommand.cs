using HackAndSlash;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveRightCommand : ICommand
    {
        private IPlayer Player;

        public MoveRightCommand(IPlayer Player)
        {
            this.Player = Player;
        }
        public void execute()
        {
                Player.ChangeDirection(GlobalSettings.Direction.Right);//face right
                Player.Move();
        }
    }
}
