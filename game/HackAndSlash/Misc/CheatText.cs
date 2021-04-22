using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class CheatText
    {
        private Game1 game;
        private TextSprite textsprite;
        private SpriteBatch spritebatch;
        private string[] texts = { "GOD MODE ACTIVATED", "HEAL ACTIVATED" , "MAX ITEMS ACTIVATED"};
        private int counter;
        private const int displayTime = 180;
        public enum cheats { GODMODE , HEAL , MAX_ITEMS }
        public cheats? activeText { get; set; }
        public CheatText (Game1 game, SpriteBatch spritebatch)
        {
            this.game = game;
            this.spritebatch = spritebatch;
            textsprite = (TextSprite)SpriteFactory.Instance.CreateTextCharacters(3);
            activeText = null;
            counter = 0;
        }
        public void Draw()
        {
            textsprite.Draw(spritebatch, texts[(int)activeText], new Vector2(GlobalSettings.WINDOW_WIDTH / 4, 
                GlobalSettings.WINDOW_HEIGHT / 2), Color.White);
            counter++;
            if (counter == displayTime)
            {
                activeText = null;
                counter = 0;
            }
        }
    }
}
