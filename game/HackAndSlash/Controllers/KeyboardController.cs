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
         
            //Add all default controls
            controllerMappings = new Dictionary<Keys, ICommand>()
            {
                //PlayerMovement
                {Keys.W, new MoveUpCommand(game)},
                {Keys.A, new MoveLeftCommand(game)},
                {Keys.S, new MoveDownCommand(game)},
                {Keys.D, new MoveRightCommand(game)},
                {Keys.Up, new MoveUpCommand(game)},
                {Keys.Left, new MoveLeftCommand(game)},
                {Keys.Down, new MoveDownCommand(game)},
                {Keys.Right, new MoveRightCommand(game)},
                //PlayerFunction
                {Keys.Z, new AttackCommand(game)},
                {Keys.N, new AttackCommand(game)},
                {Keys.D1, new UsePrimaryItemCommand(game)},
                {Keys.D2, new UseSecondaryItemCommand(game)},
                {Keys.U, new ItemUseableCommand(game)},
                {Keys.I, new ItemCycleCommand(game)},
                //GameFunction
                {Keys.R, new ResetCommand(game)},
                {Keys.Q, new QuitCommand(game)},
                {Keys.Escape, new PauseCommand(game)},
                {Keys.Tab, new DisplayMapCommand(game)}
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
                if (controllerMappings.ContainsKey(key))
                {
                    controllerMappings[key].execute();
                }
            }
            //Player only allows to move in one direction.
            if (Keyboard.GetState().IsKeyUp(Keys.W)) this.game.Player.unlockMovement();
            if (Keyboard.GetState().IsKeyUp(Keys.D)) this.game.Player.unlockMovement();
            if (Keyboard.GetState().IsKeyUp(Keys.S)) this.game.Player.unlockMovement();
            if (Keyboard.GetState().IsKeyUp(Keys.A)) this.game.Player.unlockMovement();
        }
    }
}
