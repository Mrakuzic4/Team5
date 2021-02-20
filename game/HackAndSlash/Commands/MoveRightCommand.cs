using HackAndSlash;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class MoveRightCommand : ICommand
    {
        private Game1 game;
        private IPlayer Player;

        public MoveRightCommand(Game1 game, IPlayer Player)
        {
            this.game = game;
            this.Player = Player;
        }
        public void execute()
        {
            //only change the character facing direction if the state changes
            if (Player.GetDir() != 1)
            {
                Player.ChangeDirection(1);//face down
                Player.Move();
            }
            //move the sprite
            game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X+2, game.Pos.Y);
        }
    }
}
