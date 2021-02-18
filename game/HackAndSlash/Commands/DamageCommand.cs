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
        private PlayerStateMachine playerStateMachine;

        public DamageCommand(Game1 game, PlayerStateMachine playerStateMachine)
        {
            this.game = game;
            this.playerStateMachine = playerStateMachine;
        }
        public void execute()
        {
            game.PlayerSprite = playerStateMachine.Damaged();//Pass the damaged state
        }
    }
}
