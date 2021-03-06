
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace HackAndSlash
{
    class MouseController:IController
    {
        public Game1 Game { get; set; }
        public bool isMouseVisible { get; set; }
        public ButtonState RightButton { get; }
        public ButtonState LeftButton { get; }
        private int mouseX;
        private int mouseY;
        private ICommand LevelUp;
        private ICommand LevelDown;
        public MouseController(Game1 game)
        {
            Game = game;
            isMouseVisible = true;
            Game.IsMouseVisible = isMouseVisible;
            LevelUp = new LevelCycleUpCommand(game);
            LevelDown = new LevelCycleDownCommand(game);
        }


        public void Update()
        {
            MouseState currentState = Mouse.GetState();
            mouseX = currentState.X;
            mouseY = currentState.Y;
            if (currentState.RightButton == ButtonState.Pressed)
            {
                LevelUp.execute();
            }
            if (currentState.LeftButton == ButtonState.Pressed)
            {
                LevelDown.execute();
            }

           
        }
    }
}
