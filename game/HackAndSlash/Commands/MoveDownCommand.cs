﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash.Commands
{
    public class MoveDownCommand : ICommand
    {
        private Game1 game;
        public MoveDownCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for down facing player sprite)
        }
    }
}
