using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class TitleScreenOverlay
    {
        private Game1 Game;
        private Texture2D Overlay;
        private Texture2D BlackOverlay;
        private int Rows = 2;
        private int Columns = 2;
        private int TotalFrames = 4;
        private int currentFrame = 0;
        private int width = GlobalSettings.WINDOW_WIDTH;
        private int height = GlobalSettings.WINDOW_HEIGHT;
        private int fadeOpacity = 0;
        private int fadeRate = 10;
        private bool fadingOut = false;
        private Stopwatch stopwatch = new Stopwatch();
        private SpriteBatch spriteBatch;

        public TitleScreenOverlay(Game1 game, Texture2D overlay, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.spriteBatch = spriteBatch;
            stopwatch.Restart();
            BlackOverlay = new Texture2D(game.GraphicsDevice, 1, 1);
            BlackOverlay.SetData<Color>(new Color[] { Color.White });
        }

        public void Update()
        {
            //If not in fading state, update frames and check for input
            if (!fadingOut)
            {
                if (stopwatch.ElapsedMilliseconds > GlobalSettings.TITLE_DELAY)
                {
                    currentFrame++;
                    if (currentFrame >= TotalFrames) currentFrame = 0;
                    stopwatch.Restart();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))
                {
                    //If player starts game, set to fading state
                    fadingOut = true;
                }
            }
            //if fading state, update the opacity of the black screen until 255
            else if (fadingOut)
            {
                fadeOpacity += fadeRate;
                if (fadeOpacity >= 255)
                {
                    fadingOut = false;
                    Game.elapsing = true;
                    Game.titleMenu = false;
                    SoundFactory.Instance.DungeonSong();
                }
            }
        }
        public void Draw()
        {
            int row = (int)(currentFrame / Rows);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(column * width, row * height, width, height);
            spriteBatch.Draw(Overlay, new Vector2(0, 0), sourceRectangle, Color.White);

            if (fadingOut)
            {
               spriteBatch.Draw(BlackOverlay, new Vector2(0, 0), null, new Color(0, 0, 0, fadeOpacity), 0f, Vector2.Zero, 
                   new Vector2(GlobalSettings.WINDOW_WIDTH, GlobalSettings.WINDOW_HEIGHT), SpriteEffects.None, 0);
            }
        }
    }
}
