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
        private int Rows = 2;
        private int Columns = 2;
        private int TotalFrames = 4;
        private int currentFrame = 0;
        private int width = GlobalSettings.WINDOW_WIDTH;
        private int height = GlobalSettings.WINDOW_HEIGHT;
        private Stopwatch stopwatch = new Stopwatch();
        private SpriteBatch spriteBatch;

        public TitleScreenOverlay(Game1 game, Texture2D overlay, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.spriteBatch = spriteBatch;
            stopwatch.Restart();
        }

        public void Update()
        {
            if (stopwatch.ElapsedMilliseconds > GlobalSettings.TITLE_DELAY)
            {
                currentFrame++;
                if (currentFrame >= TotalFrames) currentFrame = 0;
                stopwatch.Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))
            {
                Game.titleMenu = false;
                Game.elapsing = true;
            }
        }
        public void Draw()
        {
            int row = (int)(currentFrame / Rows);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(column * width, row * height, width, height);
            spriteBatch.Draw(Overlay, new Vector2(0, 0), sourceRectangle, Color.White);
        }
    }
}
