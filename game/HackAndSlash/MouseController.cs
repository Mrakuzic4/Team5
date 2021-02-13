
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        public MouseController(Game1 game)
        {
            Game = game;
        }

        public void Initialize()
        {
            //no initialization required for sprint 2
        }

        public void Update(Texture2D texture)
        {
            MouseState currentState = Mouse.GetState();
            mouseX = currentState.X;
            mouseY = currentState.Y;
            if (currentState.RightButton == ButtonState.Pressed)
            {
                //Game.Exit();
            }

            //Still
            //if ((mouseX < 300) && (mouseY < 200) && (currentState.LeftButton == ButtonState.Pressed))
            //{
            //    Game.mario = new StillSprite(texture);
            //}

            ////Animate Still
            //if ((mouseX < 300) && (mouseY > 200) && (currentState.LeftButton == ButtonState.Pressed))
            //{
            //    Game.mario = new MovingSprite(texture);
            //}

            ////Still + Up Down
            //if ((mouseX > 300) && (mouseY < 200) && (currentState.LeftButton == ButtonState.Pressed))
            //{
            //    Game.mario = new AnimatedSprite(texture);
            //}

            ////Animate + Left Right
            //if ((mouseX > 300) && (mouseY > 200) && (currentState.LeftButton == ButtonState.Pressed))
            //{
            //    Game.mario = new MovingAnimatedSprite(texture);
            //}
        }
    }
}
