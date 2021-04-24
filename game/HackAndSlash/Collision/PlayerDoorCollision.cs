using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace HackAndSlash
{
    class PlayerDoorCollision
    {
        private const int TOLERANCE = 8;
        private const int ACTIVATION_RANGE = 32;

        // Added static to minimize computation 
        // ".*Range" is for picking doors on the facing direction of the door 
        private static int TopRange = GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BORDER_OFFSET + TOLERANCE;
        private static int BottomRange = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET
            - GlobalSettings.BASE_SCALAR - TOLERANCE;
        private static int LeftRange = GlobalSettings.BORDER_OFFSET + TOLERANCE;
        private static int RightRange = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET
            - GlobalSettings.BASE_SCALAR - TOLERANCE;

        // ".*Locator" if for picking distance perpendicular to the facing direction of the doors 
        private static int TopBotomLocator = GlobalSettings.WINDOW_WIDTH / 2 - GlobalSettings.BASE_SCALAR / 2;
        private static int LeftRightLocator = GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.GAME_AREA_HEIGHT / 2
            - GlobalSettings.BASE_SCALAR / 2; 

        public PlayerDoorCollision()
        {
            
        }

        public int ReachingDirection(Vector2 Pos)
        {
            int Result = -1;

            if (Pos.X < LeftRange && Math.Abs(Pos.Y - LeftRightLocator) < ACTIVATION_RANGE) {
                Result = (int)GlobalSettings.Direction.Left;
            }
            else if (Pos.X > RightRange && Math.Abs(Pos.Y - LeftRightLocator) < ACTIVATION_RANGE) {
                Result = (int)GlobalSettings.Direction.Right;
            }
            else if (Pos.Y < TopRange && Math.Abs(Pos.X - TopBotomLocator) < ACTIVATION_RANGE) {
                Result = (int)GlobalSettings.Direction.Up;
            }
            else if (Pos.Y > BottomRange && Math.Abs(Pos.X - TopBotomLocator) < ACTIVATION_RANGE) {
                Result = (int)GlobalSettings.Direction.Down;
            }

            return Result;
        }

    }
}
