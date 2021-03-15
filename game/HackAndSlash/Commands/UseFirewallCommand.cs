using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UseFirewallCommand : ICommand
    {
        private Game1 game;
        public UseFirewallCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.ItemHolder = game.firewallFirst;
            game.Player.UseItem();
        }
    }
}