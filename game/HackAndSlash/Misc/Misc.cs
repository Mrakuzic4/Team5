using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


/// <summary>
/// Put whatever function that is too small to become a class here. 
/// Try not to add redundancy to GlobalSettings 
/// </summary>

namespace HackAndSlash
{
    public class Misc
    {

        public Misc()
        {

        }

        public Vector2 PlayAreaPosition(int x, int y)
        {
            return new Vector2(GlobalSettings.BORDER_OFFSET + x * GlobalSettings.BASE_SCALAR,
                GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BORDER_OFFSET + y * GlobalSettings.BASE_SCALAR);
        }
    }
}
