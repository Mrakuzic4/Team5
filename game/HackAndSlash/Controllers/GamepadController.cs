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

        /// <summary>
        /// Constructor. Defines the default controls in controllerMappings dictionary and
        /// stores the capabilites of user's gamepad (if connected)
        /// </summary>
        /// <param name="game"></param>
        public GamepadController(Game1 game)
        {
            this.Game = game;
            capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            controllerMappings = new Dictionary<Buttons, ICommand>()
            { 
                {Buttons.A, new AttackCommand(game) },
                {Buttons.B, new UsePrimaryItemCommand(game) },
                {Buttons.X, new UseSecondaryItemCommand(game) },
                {Buttons.DPadDown, new MoveDownCommand(game) },
                {Buttons.DPadLeft, new MoveLeftCommand(game) },
                {Buttons.DPadRight, new MoveRightCommand(game) },
                {Buttons.DPadUp, new MoveUpCommand(game) },
                {Buttons.Start, new PauseCommand(game) }
            };
        }

        /// <summary>
        /// Method to map each key to a command in the dictionary. Can be used later to allow 
        /// modification of controls by the user.
        /// </summary>
        public void RegisterCommand(Buttons button, ICommand command)
        {
            controllerMappings.Add(button, command);
        }

        /// <summary>
        /// Checks if a gamepad is connected, and executes each command according to controllerMappings
        /// dictionary if that button is pressed.
        /// </summary>
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
