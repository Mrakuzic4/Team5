using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackAndSlash
{
    class KeyboardController: IController
    {
        private Game1 game { get; set; }
        private Dictionary<Keys, ICommand> controllerMappings;
        //Statemachine to keep track the player's state
        private PlayerStateMachine playerStateMachine;

        public KeyboardController(Game1 game)
        {
            this.game = game;
            controllerMappings = new Dictionary<Keys, ICommand>();
            playerStateMachine = new PlayerStateMachine(1); //default is facing right
        }

        /// <summary>
        /// Simple method to setup all the default controls in the controllerMappings dictionary.
        /// </summary>
        public void Initialize()
        {
            RegisterCommand(Keys.W, new MoveUpCommand(game,playerStateMachine));
            RegisterCommand(Keys.A, new MoveLeftCommand(game, playerStateMachine));
            RegisterCommand(Keys.S, new MoveDownCommand(game, playerStateMachine));
            RegisterCommand(Keys.D, new MoveRightCommand(game, playerStateMachine));
            RegisterCommand(Keys.Z, new AttackCommand(game));
            RegisterCommand(Keys.N, new AttackCommand(game));
            RegisterCommand(Keys.D1, new UsePlayerItemCommand(game));
            RegisterCommand(Keys.E, new DamageCommand(game));
            RegisterCommand(Keys.T, new BlockCycleCommand(game));
            RegisterCommand(Keys.Y, new BlockCycleCommand(game));
            RegisterCommand(Keys.U, new ItemCycleCommand(game));
            RegisterCommand(Keys.I, new ItemCycleCommand(game));
            RegisterCommand(Keys.O, new EnemyCycleCommand(game));
            RegisterCommand(Keys.P, new EnemyCycleCommand(game));
            RegisterCommand(Keys.R, new ResetCommand(game));
            RegisterCommand(Keys.Q, new QuitCommand(game));
        }

        /// <summary>
        /// Method to map each key to a command in the dictionary. Can be used later to allow 
        /// modification of controls by the user.
        /// </summary>
        public void RegisterCommand(Keys key, ICommand command)
        {
            controllerMappings.Add(key, command);
        }

        /// <summary>
        /// Records which keys are pressed and stores them in an array, then calls the
        /// execute() method on each key. Allows for multiple keys to be pressed and 
        /// executed in the same frame.
        /// </summary>
        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (controllerMappings.ContainsKey(key)){
                    controllerMappings[key].execute();
                }
            }
        }
    }
}
