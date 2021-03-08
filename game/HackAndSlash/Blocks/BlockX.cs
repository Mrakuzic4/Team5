using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class BlockX : IBlock
    {
        private Texture2D block;
        private Vector2 location { get; set; }
        private SpriteBatch spriteBatch;
        public Rectangle rectangle { get; }

        public BlockX(Texture2D block, Vector2 location, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.location = location;
            this.block = block;
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        public void Update()
        {
            //do nothing
        }
        public void Draw()
        {
            spriteBatch.Draw(block, location, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
