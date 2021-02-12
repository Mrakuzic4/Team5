using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackAndSlash
{
    class KeyboardController: IController
    {
        public Game1 Game { get; set; }
        public KeyboardController(Game1 game)
        {
            Game = game;
        }

        public void Update(Texture2D texture)
        {
            KeyboardState keyState = Keyboard.GetState();

            //Exit
            if (Keyboard.GetState().IsKeyDown(Keys.D0))
            {
                Game.Exit();
            }

            //Player moving up, change the state to face up.
            //If state is already face up, no action needed. Just implement player movement.
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //Game.mario = new StillSprite(texture);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //Game.mario = new StillSprite(texture);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //Game.mario = new StillSprite(texture);
            }
        }
    }
}
