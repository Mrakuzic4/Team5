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
        private Game1 game;
        public MoveUpCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for up facing player sprite)
        }
    }
}
