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
        public UsePlayerItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for player sprite with item)
        }
    }
}
