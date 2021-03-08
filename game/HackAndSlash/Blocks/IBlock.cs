using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public interface IBlock
    {
        Rectangle rectangle { get; }

        void Update();
        void Draw();
    }
}
