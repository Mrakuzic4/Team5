using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class PauseOverlay
    {
        private Game1 Game;
        private Texture2D Overlay;
        private Texture2D SwordSelector;
        private SpriteBatch spriteBatch;
        private Vector2 SelectorContinueLoc = new Vector2(GlobalSettings.PAUSE_CONTINUE_X, GlobalSettings.PAUSE_CONTINUE_Y);
        private Vector2 SelectorQuitLoc = new Vector2(GlobalSettings.PAUSE_QUIT_X, GlobalSettings.PAUSE_QUIT_Y);
        private Vector2 CurrentSelection;
        public PauseOverlay(Game1 game, Texture2D overlay, Texture2D swordSelector, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.SwordSelector = swordSelector;
            this.spriteBatch = spriteBatch;
            CurrentSelection = SelectorContinueLoc;
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                CurrentSelection = SelectorQuitLoc;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                CurrentSelection = SelectorContinueLoc;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (CurrentSelection == SelectorContinueLoc)
                {
                    Game.elapsing = true;
                    Game.gamePaused = false;
                }
                else
                {
                    Game.Exit();
                }
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(Overlay, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(SwordSelector, CurrentSelection, Color.White);
        }
    }
}
