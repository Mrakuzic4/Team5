using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace HackAndSlash
{
    class BombWallCollision
    {
        public int direction { get; set; }
        public const int RANGE = 3 * GlobalSettings.BASE_SCALAR; 

        public BombWallCollision(Vector2 Pos, Map MapInfo)
        {
            if (!CanAddHole(CanReachWall(Pos), MapInfo))
                direction = -1; 
        }

        private int CanReachWall(Vector2 Pos)
        {
            int Direction = -1;
            int HorizontalDoor = GlobalSettings.WINDOW_WIDTH / 2;
            int VerticalDoor = GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.GAME_AREA_HEIGHT / 2;

            if (Pos.X > HorizontalDoor - RANGE && Pos.X < HorizontalDoor)
            {
                if (Pos.Y <= GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BORDER_OFFSET)
                    Direction = (int)GlobalSettings.Direction.Up;
                else if (Pos.Y >= GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET)
                    Direction = (int)GlobalSettings.Direction.Down;
            } 
            else if (Pos.Y > VerticalDoor - RANGE && Pos.Y < VerticalDoor)
            {
                if (Pos.X <= GlobalSettings.BORDER_OFFSET)
                    Direction = (int)GlobalSettings.Direction.Left;
                else if (Pos.X >= GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET)
                    Direction = (int)GlobalSettings.Direction.Right; 
            }

            direction = Direction; 

            return Direction; 
        }

        // This methods went throught some modifications and is now basically useless 
        private bool CanAddHole(int Direction, Map MapInfo)
        {
            return (Direction > 0); 
        }


    }
}
