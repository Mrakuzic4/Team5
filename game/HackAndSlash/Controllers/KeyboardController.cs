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

        public KeyboardController(Game1 game)
        {
            this.game = game;
         
            //add all default controls
            controllerMappings = new Dictionary<Keys, ICommand>()
            {
                {Keys.W, new MoveUpCommand(game.Player)},
                {Keys.A, new MoveLeftCommand(game.Player)},
                {Keys.S, new MoveDownCommand( game.Player)},
                {Keys.D, new MoveRightCommand(game.Player)},
                {Keys.Z, new AttackCommand(game, game.Player)},
                {Keys.N, new AttackCommand(game, game.Player)},
                {Keys.Up, new MoveUpCommand(game.Player)},
                {Keys.Left, new MoveLeftCommand(game.Player)},
                {Keys.Down, new MoveDownCommand(game.Player)},
                {Keys.Right, new MoveRightCommand(game.Player)},

                {Keys.D1, new UsePlayerItemCommand(game, game.Player)},
                {Keys.E, new DamageCommand(game, game.Player)},
                {Keys.T, new BlockCycleDownCommand(game, game.blockList)},
                {Keys.Y, new BlockCycleUpCommand(game, game.blockList)},
                {Keys.U, new ItemUseableCommand(game)},
                {Keys.I, new ItemCycleCommand(game)},
                {Keys.O, new EnemyCycleCommandSnake(game)},
                {Keys.P, new EnemyCycleCommandBug(game)},
                {Keys.R, new ResetCommand(game)},
                {Keys.Q, new QuitCommand(game)}
            };
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
