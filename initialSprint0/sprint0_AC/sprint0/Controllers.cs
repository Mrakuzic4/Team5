using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace sprint0
{
    class keyboardCtrl : IController
    {
        public bool Pressed(KeyboardState state, Keys tarKey)
        {
            return state.IsKeyDown(tarKey);
        }

        // Based on key pressed, return positive, negative, or halt
        public int DirMultiplier(KeyboardState state, Keys PosKey, Keys NegKey)
        {
            if (Pressed(state, PosKey))
                return 1;
            if (Pressed(state, NegKey))
                return -1;
            return 0;
        }

        public int CheckState(KeyboardState state, int stateBefore)
        {
            int ReturnState = stateBefore;
            var StateMap = new Dictionary<Keys, int>(){
                {Keys.D0, 0},
                {Keys.D1, 1},
                {Keys.D2, 2},
                {Keys.D3, 3},
                {Keys.D4, 4},
                {Keys.D5, 5},
                {Keys.NumPad0, 0},
                {Keys.NumPad1, 1},
                {Keys.NumPad2, 2},
                {Keys.NumPad3, 3},
                {Keys.NumPad4, 4},
                {Keys.NumPad5, 5}
            };

            foreach (var KeyInput in StateMap)
            {
                if (Pressed(state, KeyInput.Key))
                    ReturnState = KeyInput.Value;
            }

            return ReturnState;
        }
    }
    
    class MouseCtrl : IController
    {
        private Vector2 windowSize { get; set; }
        private Vector2 cursorLoc;

        public MouseCtrl(GraphicsDeviceManager graphics)
        {
            windowSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            cursorLoc = new Vector2(0, 0);
        }

        public bool LeftPressed(MouseState state)
        {
            return state.LeftButton == ButtonState.Pressed;
        }
        public bool RightPressed(MouseState state)
        {
            return state.RightButton == ButtonState.Pressed;
        }

        // Not used 
        public Vector2 CalculateOffset(MouseState state)
        {
            Vector2 offset = new Vector2(0, 0);
            cursorLoc.X = state.X;
            cursorLoc.Y = state.Y;
            /// TODO offset implemntation  
            return offset;
        }

        public int CheckState(MouseState state, int stateBefore, GraphicsDeviceManager graphics)
        {
            int determinedState = 1;
            int width = graphics.PreferredBackBufferWidth;
            int height = graphics.PreferredBackBufferHeight;

            if (RightPressed(state))
                return 0;
            if(LeftPressed(state))
            {
                // Math trick to do the screen spilt 
                if (state.X > (width / 2))
                    determinedState += 1;
                if (state.Y > (height / 2))
                    determinedState += 2;

                return determinedState;
            }
            return stateBefore;
        }
    }
}
