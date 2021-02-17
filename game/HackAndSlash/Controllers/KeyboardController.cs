﻿using System;
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
            playerStateMachine = new PlayerStateMachine(1); //default is facing right

            //add all default controls
            controllerMappings = new Dictionary<Keys, ICommand>()
            {
                {Keys.W, new MoveUpCommand(game,playerStateMachine)},
                {Keys.A, new MoveLeftCommand(game, playerStateMachine)},
                {Keys.S, new MoveDownCommand(game, playerStateMachine)},
                {Keys.D, new MoveRightCommand(game, playerStateMachine)},
                {Keys.Z, new AttackCommand(game)},
                {Keys.N, new AttackCommand(game)},
                {Keys.D1, new UsePlayerItemCommand(game)},
                {Keys.E, new DamageCommand(game)},
                {Keys.T, new BlockCycleCommand(game)},
                {Keys.Y, new BlockCycleCommand(game)},
                {Keys.U, new ItemCycleCommand(game)},
                {Keys.I, new ItemCycleCommand(game)},
                {Keys.O, new EnemyCycleCommand(game)},
                {Keys.P, new EnemyCycleCommand(game)},
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
