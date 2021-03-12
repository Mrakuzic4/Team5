using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class GamepadController : IController
    {
        private Game1 Game;
        GamePadCapabilities capabilities;
        private Dictionary<Buttons, ICommand> controllerMappings;

        public GamepadController(Game1 game)
        {
            this.Game = game;
            capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            controllerMappings = new Dictionary<Buttons, ICommand>()
            { 
                {Buttons.A, new AttackCommand(game) },
                {Buttons.DPadDown, new MoveDownCommand(game) },
                {Buttons.DPadLeft, new MoveLeftCommand(game) },
                {Buttons.DPadRight, new MoveRightCommand(game) },
                {Buttons.DPadUp, new MoveUpCommand(game) }
            };
        }
        public void Update()
        {
             if (capabilities.IsConnected && capabilities.GamePadType == GamePadType.GamePad)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                foreach (Buttons button in controllerMappings.Keys)
                {
                    if (state.IsButtonDown(button)) 
                    {
                        controllerMappings[button].execute();
                    }
                }
            }
        }
    }
}
