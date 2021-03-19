using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{
    class MapTransition
    {
        private bool test = true; // Dev option 

        private GraphicsDevice graphicDevice;
        private SpriteBatch spriteBatch;
        private GameTime gameTime;

        private Level prevLevel { get; set; }
        private Level newLevel { get; set; }

        public MapTransition(GraphicsDevice GD, SpriteBatch SB, GameTime GT)
        {
            graphicDevice = GD;
            spriteBatch = SB;
        }



    }
}
