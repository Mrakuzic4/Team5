
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{
    // Left for some future global (throught out the game kind) variables 
    

    public class GlobalSettings
    {
        public const int WINDOW_WIDTH = 1024;
        public const int WINDOW_HEIGHT = 704;

        public const int BASE_SCALAR = 64; // Pixel unit

        public const int BORDER_OFFSET = 128; // Size of the map border 
        public const int TILE_ROW = 7;
        public const int TILE_COLUMN = 12;
        public const int MAX_DISPLAY_DIV = 6;

        public const int STEP_SIZE_X = 5; // Distance moved in each update 
        public const int STEP_SIZE_Y = 5;
        public const int KNOCKBACK_DIST_X = 64; // Knockback Distance 
        public const int KNOCKBACK_DIST_Y = 64;
        public const int PLAYER_HITBOX_WIDTH = 54;
        public const int PLAYER_HITBOX_HEIGHT = 40;
        public const int PLAYER_HITBOX_Y_OFFSET = 24;
        public const int PLAYER_HITBOX_X_OFFSET = 5;

        public const long DELAY_TIME = 100; // In ms
        public const long DELAY_KEYBOARD = 200; // For keyboard pressing event 
        public enum Direction { Left, Right, Up, Down };
        public enum CollisionType { None, Left, Right, Top, Bottom, }
    }

    class ImageFile
    {
        public string pathName { get; set; }
        public int R { get; set; }
        public int C { get; set; }

        public ImageFile(string PN, int C, int R)
        {
            this.pathName = PN;
            this.C = C;
            this.R = R;
            
        }
    }


    

}
