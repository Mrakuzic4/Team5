using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class UsePlayerItemCommand : ICommand
    {
        private Game1 game;
        IPlayer Player;
        public UsePlayerItemCommand(Game1 game, IPlayer Player)
        {
            this.game = game;
            this.Player = Player;
        }
        public void execute()
        {
            Player.UseItem();
            game.Player = new UseItemPlayer(Player, game); //Decorator of the PlayerSprite
            game.ItemHolder.UseItem(game.Player.GetDir(), Player.GetPos()); // default position for now
        }
    }
}
