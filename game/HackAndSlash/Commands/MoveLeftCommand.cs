using HackAndSlash;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveLeftCommand : ICommand
    {
        private Game1 game;
        public MoveLeftCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for left facing player sprite)
        }
    }
}
