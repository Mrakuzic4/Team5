using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HackAndSlash
{
    class SoundByte
    {
        public SoundByte(SoundEffect effect)
        {
            effect.Play();
        }
    }
}
