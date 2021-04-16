using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HackAndSlash
{
    public class AttackCommand : ICommand
    {
        private Game1 game;


        public AttackCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.Player.Attack();
            //game.Player.HealthUp();
            game.Player.ShieldUp();
        }
    }
}
