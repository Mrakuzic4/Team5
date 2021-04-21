using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace HackAndSlash
{
    // This thing is essentially unused and useless
    public interface ILevel
    {
        void Update(GameTime gameTime);

        void Draw();
        void DrawOverlay();
    }
}
