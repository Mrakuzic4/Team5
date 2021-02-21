using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class ChipBlock : IBlock
    {
        private Texture2D block;
        private Vector2 location { get; set; }
        private SpriteBatch spriteBatch;

        public ChipBlock(Texture2D block, Vector2 location, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.location = location;
            this.block = block;
        }

        public void Update()
        {
            //do nothing
        }
        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(block, location, Color.White);
            spriteBatch.End();
        }

    }
}
