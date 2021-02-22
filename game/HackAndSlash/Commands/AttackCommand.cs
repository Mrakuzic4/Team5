using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class AttackCommand : ICommand
    {
        IPlayer Player;
        private Game1 game;

        public AttackCommand(Game1 game, IPlayer Player)
        {
            this.Player = Player;
            this.game = game;
        }
        public void execute()
        {
            Player.Attack(); //Deal Damage, need further implementation.
            game.Player = new AttackPlayer(Player, game); //Decorator of the PlayerSprite
        }
    }
}
