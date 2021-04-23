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
    class SoundMenuOverlay
    {
        private Game1 Game;
        private Texture2D Overlay;
        private Texture2D VolumeSlider;
        private SpriteBatch spriteBatch;
        private int posY;

        public SoundMenuOverlay(Game1 game, Texture2D overlay, Texture2D volumeSlider, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.VolumeSlider = volumeSlider;
            this.spriteBatch = spriteBatch;
            this.posY = 600;
        }
        public void Update()
        {
            //Song Selection
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                SoundFactory.Instance.SongSelect(1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                SoundFactory.Instance.SongSelect(2);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                SoundFactory.Instance.SongSelect(3);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                SoundFactory.Instance.SongSelect(4);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                SoundFactory.Instance.SongSelect(5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                SoundFactory.Instance.SongSelect(6);
            }

            //Volume Slider
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //Increase volume
                SoundFactory.Instance.IncreaseVol();
                //Slider animation logic
                if (posY >= 600)
                {
                    posY--;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //Decrease volume
                SoundFactory.Instance.DecreaseVol();
                //Slider animation logic
                if (posY <= 735)
                {
                    posY++;
                }
            }

            //Escape back to pause screen
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.GameState = GlobalSettings.GameStates.Paused;
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(Overlay, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(VolumeSlider, new Vector2(469, posY), Color.White);
        }
    }
}
