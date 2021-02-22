using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class SmoothBlock : IBlock
    {
        private Texture2D block;
        private Vector2 location { get; set; }
        private SpriteBatch spriteBatch;

        public SmoothBlock(Texture2D block, Vector2 location, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.location = location;
            this.block = block;
        }
        public void Draw()
        {
            spriteBatch.Draw(block, location, null, Color.White, 0f, Vector2.Zero, .5f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            //do nothing
        }
    }
}
