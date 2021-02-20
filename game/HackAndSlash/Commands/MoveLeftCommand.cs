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
        private IPlayer Player;

        public MoveLeftCommand(Game1 game, IPlayer Player)
        {
            this.game = game;
            this.Player = Player;
        }
        public void execute()
        {
            //only change the character facing direction if the state changes
            if (Player.GetDir() != 0)
            {
                Player.ChangeDirection(0);//face down
                Player.Move();
            }
            //move the sprite
            game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X-2, game.Pos.Y);
        }
    }
}
