using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public interface ILevel
    {
        void Update();

        void Draw();
        void DrawOverlay();
    }
}
