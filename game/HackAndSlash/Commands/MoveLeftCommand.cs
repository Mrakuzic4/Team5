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
        private PlayerStateMachine playerStateMachine;

        public MoveLeftCommand(Game1 game, PlayerStateMachine playerStateMachine)
        {
            this.game = game;
            this.playerStateMachine = playerStateMachine;
        }
        public void execute()
        {
            //only change the character facing direction if the state changes
            if (playerStateMachine.Direction != 0)
            {
                playerStateMachine.ChangeDirection(0);//direction is down
                game.PlayerSprite = playerStateMachine.idle(); //set the sprite
            }
            //move the sprite
            game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X-2, game.Pos.Y);
        }
    }
}
