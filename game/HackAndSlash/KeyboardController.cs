using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackAndSlash
{
    class KeyboardController: IController
    {
        private Game1 Game { get; set; }
        private Dictionary<Keys, ICommand> controllerMappings;

        /// <summary>
        /// Constructor which initializes the controllerMappings dictionary with default controls.
        /// </summary>
        /// <param name="game"></param>
        public KeyboardController(Game1 game)
        {
            Game = game;
            controllerMappings = new Dictionary<Keys, ICommand>()
            {
                {Keys.W, new MoveUpCommand(Game)},
                {Keys.A, new MoveLeftCommand(Game)},
                {Keys.S, new MoveDownCommand(Game)},
                {Keys.D, new MoveRightCommand(Game)},
                {Keys.Z, new AttackCommand(Game)},
                {Keys.N, new AttackCommand(Game)},
                {Keys.D1, new UsePlayerItemCommand(Game)},
                {Keys.E, new DamageCommand(Game)},
                {Keys.T, new BlockCycleCommand(Game)},
                {Keys.Y, new BlockCycleCommand(Game)},
                {Keys.U, new ItemCycleCommand(Game)},
                {Keys.I, new ItemCycleCommand(Game)},
                {Keys.O, new EnemyCycleCommand(Game)},
                {Keys.P, new EnemyCycleCommand(Game)},
                {Keys.R, new ResetCommand(Game)},
                {Keys.Q, new QuitCommand(Game)}
            };
        }

        /// <summary>
        /// Simple method to setup all the default controls in the controllerMappings dictionary.
        /// </summary>
/*        public void Initialize()
        {
            RegisterCommand(Keys.W, new MoveUpCommand(Game));
            RegisterCommand(Keys.A, new MoveLeftCommand(Game));
            RegisterCommand(Keys.S, new MoveDownCommand(Game));
            RegisterCommand(Keys.D, new MoveRightCommand(Game));
            RegisterCommand(Keys.Z, new AttackCommand(Game));
            RegisterCommand(Keys.N, new AttackCommand(Game));
            RegisterCommand(Keys.D1, new UsePlayerItemCommand(Game));
            RegisterCommand(Keys.E, new DamageCommand(Game));
            RegisterCommand(Keys.T, new BlockCycleCommand(Game));
            RegisterCommand(Keys.Y, new BlockCycleCommand(Game));
            RegisterCommand(Keys.U, new ItemCycleCommand(Game));
            RegisterCommand(Keys.I, new ItemCycleCommand(Game));
            RegisterCommand(Keys.O, new EnemyCycleCommand(Game));
            RegisterCommand(Keys.P, new EnemyCycleCommand(Game));
            RegisterCommand(Keys.R, new ResetCommand(Game));
            RegisterCommand(Keys.Q, new QuitCommand(Game));
        }*/

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
        public void Update(Texture2D texture)
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
