using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HackAndSlash
{
    public class DisplayMapCommand : ICommand
    {
        private Game1 game;


        public DisplayMapCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.displayMap = true; 
        }
    }
}
