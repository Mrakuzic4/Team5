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

            //Still
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                //Game.mario = new StillSprite(texture);
            }
        }
    }
}
