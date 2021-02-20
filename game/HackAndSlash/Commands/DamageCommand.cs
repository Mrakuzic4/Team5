using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class DamageCommand : ICommand
    {
        private Game1 game;
        private IPlayer Player;

        public DamageCommand(Game1 game, IPlayer Player)
        {
            this.game = game;
            this.Player = Player;
        }
        public void execute()
        {
            game.Player = new DamagedPlayer(Player, game); //Decorator of the PlayerSprite
        }
    }
}
