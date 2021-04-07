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
        private const int BASE_RANGE = 1;

        private int threshold = BASE_RANGE * GlobalSettings.BASE_SCALAR;

        public Misc()
        {

        }

        public void SetFogRange(int Range)
        {
            
            threshold = (BASE_RANGE + Range) * GlobalSettings.BASE_SCALAR;
        }
        public bool InFogRange(Vector2 PlayerPos, Vector2 TargetPos)
        {
            Vector2 D = PlayerPos - TargetPos;
            if (Math.Sqrt(D.X * D.X + D.Y * D.Y) < threshold)
                return true;
            return false;
        }

        public Vector2 PlayAreaPosition(int x, int y)
        {
            return new Vector2(GlobalSettings.BORDER_OFFSET + x * GlobalSettings.BASE_SCALAR,
                GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BORDER_OFFSET + y * GlobalSettings.BASE_SCALAR);
        }
    }
}
