using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class DamageCommand : ICommand
    {
        private IPlayer Player;

        public DamageCommand(IPlayer Player)
        {
            this.Player = Player;
        }
        public void execute()
        {
            Player.Damaged();
        }
    }
}
