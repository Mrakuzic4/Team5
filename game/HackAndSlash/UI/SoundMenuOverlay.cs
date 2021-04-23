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
            this.posY = GlobalSettings.VOLUME_TOP_LIMIT;
        }
        public void Update()
        {
            //Song Selection
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                SoundFactory.Instance.SongSelect(GlobalSettings.SONG_ONE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                SoundFactory.Instance.SongSelect(GlobalSettings.SONG_TWO);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                SoundFactory.Instance.SongSelect(GlobalSettings.SONG_THREE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                SoundFactory.Instance.SongSelect(GlobalSettings.SONG_FOUR);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                SoundFactory.Instance.SongSelect(GlobalSettings.SONG_FIVE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                SoundFactory.Instance.SongSelect(GlobalSettings.SONG_SIX);
            }

            //Volume Slider
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //Increase volume
                SoundFactory.Instance.IncreaseVol();
                //Slider animation logic
                if (posY >= GlobalSettings.VOLUME_TOP_LIMIT)
                {
                    posY--;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //Decrease volume
                SoundFactory.Instance.DecreaseVol();
                //Slider animation logic
                if (posY <= GlobalSettings.VOLUME_BOTTOM_LIMIT)
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
            spriteBatch.Draw(VolumeSlider, new Vector2(GlobalSettings.VOLUME_SLIDE_POS_X, posY), Color.White);
        }
    }
}
