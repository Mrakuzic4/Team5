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
    class GameWonOverlay
    {
        private Game1 Game;
        private Texture2D Overlay;
        private Texture2D SwordSelector;
        private SpriteBatch spriteBatch;
        private Vector2 SelectorContinueLoc = new Vector2(GlobalSettings.PAUSE_CONTINUE_X, GlobalSettings.PAUSE_CONTINUE_Y);
        private Vector2 SelectorQuitLoc = new Vector2(GlobalSettings.PAUSE_QUIT_X, GlobalSettings.PAUSE_QUIT_Y);
        private Vector2 CurrentSelection;
        private int gameOverAnimationCounter;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangleLeft;
        private Rectangle destinationRectangleRight;

        public GameWonOverlay(Game1 game, Texture2D overlay, Texture2D swordSelector, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.SwordSelector = swordSelector;
            this.spriteBatch = spriteBatch;
            CurrentSelection = SelectorContinueLoc;
            gameOverAnimationCounter = 0;
        }
        public void Update(GameTime gameTime)
        {
            gameOverAnimationCounter += gameTime.ElapsedGameTime.Milliseconds;
            if (gameOverAnimationCounter > 4000)
            {
                this.Game.GameState = GlobalSettings.GameStates.GameWon;
                this.Game.inGameWonAnimation = false;
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)|| GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
                {
                    CurrentSelection = SelectorQuitLoc;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
                {
                    CurrentSelection = SelectorContinueLoc;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                {
                    //If chose to continue
                    if (CurrentSelection == SelectorContinueLoc)
                    {
                        Game.GameState = GlobalSettings.GameStates.TitleMenu;
                    }
                    else
                    {
                        Game.Exit();
                    }
                }
            }

            else
            {
                SpriteFactory.Instance.SetPlayerWon();
            }
        }

        public void Draw()
        {
            if (Game.inGameWonAnimation)
            {
                sourceRectangle = new Rectangle(0, 0, Overlay.Width, Overlay.Height);
                int current = (gameOverAnimationCounter / 4000) *Game.GraphicsDevice.Viewport.Width;
                System.Console.WriteLine("Current: " +current);
                System.Console.WriteLine("Right x coordinate: "+(Game.GraphicsDevice.Viewport.Width - (gameOverAnimationCounter / 4000) * Game.GraphicsDevice.Viewport.Width));
                destinationRectangleLeft = new Rectangle(0, 0, (gameOverAnimationCounter / 4000) * Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
                destinationRectangleRight = new Rectangle((Game.GraphicsDevice.Viewport.Width - (gameOverAnimationCounter / 4000) * Game.GraphicsDevice.Viewport.Width), 0, (gameOverAnimationCounter / 4000) * Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
                //spriteBatch.Draw(SpriteFactory.Instance.getGameWonScreen(), destinationRectangleLeft, sourceRectangle, Color.White);
                //spriteBatch.Draw(SpriteFactory.Instance.getGameWonScreen(), destinationRectangleRight, sourceRectangle, Color.White);
                
            }
            else
            {
                spriteBatch.Draw(SpriteFactory.Instance.GetGameOverOverlay(), new Vector2(0, 0), Color.White);
                spriteBatch.Draw(SwordSelector, CurrentSelection, Color.White);
            }
            
        }
    }
}
