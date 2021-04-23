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
    class GameOverOverlay
    {
        private Game1 Game;
        private Texture2D Overlay;
        private Texture2D SwordSelector;
        private SpriteBatch spriteBatch;
        private Vector2 SelectorContinueLoc = new Vector2(GlobalSettings.PAUSE_CONTINUE_X, GlobalSettings.PAUSE_CONTINUE_Y);
        private Vector2 SelectorQuitLoc = new Vector2(GlobalSettings.PAUSE_QUIT_X, GlobalSettings.PAUSE_QUIT_Y);
        private Vector2 CurrentSelection;
        private int gameOverAnimationCounter;
        private int animationSlower;

        public GameOverOverlay(Game1 game, Texture2D overlay, Texture2D swordSelector, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.SwordSelector = swordSelector;
            this.spriteBatch = spriteBatch;
            CurrentSelection = SelectorContinueLoc;
            gameOverAnimationCounter = 0;
            animationSlower = 0;
        }
        public void Update(GameTime gameTime)
        {
            animationSlower += gameTime.ElapsedGameTime.Milliseconds;
            if(animationSlower>80)
            {
                animationSlower = 0;
                DrawPlayer.Instance.Frame++;
            }
            DrawPlayer.Instance.Update();

            gameOverAnimationCounter += gameTime.ElapsedGameTime.Milliseconds;
            if (gameOverAnimationCounter > 3000)
            {
                this.Game.GameState = GlobalSettings.GameStates.GameOver;
                this.Game.inGameOverAnimation = false;
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || 
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
                {
                    CurrentSelection = SelectorQuitLoc;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || 
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
                {
                    CurrentSelection = SelectorContinueLoc;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                {
                    //If chose to continue
                    if (CurrentSelection == SelectorContinueLoc)
                    {
                        Game.GameState = GlobalSettings.GameStates.Running;
                        Game.reset(5); //Reset the room upon player's death
                        Game.resetCurrentLevelGameOver();
                        Game.Player.Healed(); //Restart the game with Player having 1 HP.
                        gameOverAnimationCounter = 0;
                        SoundFactory.Instance.PlayLast();
                    }
                    else
                    {
                        Game.GameState = GlobalSettings.GameStates.Upgrading;
                    }
                }
            }

            else
            {
                SpriteFactory.Instance.SetPlayerDying();
            }
        }

        public void Draw()
        {
            
                spriteBatch.Draw(Overlay, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(SwordSelector, CurrentSelection, Color.White);
            
        }
    }
}
