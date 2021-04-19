
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    // Left for some future global (throught out the game kind) variables 


    public class GlobalSettings
    {

        public const int GAME_AREA_WIDTH = 1024;
        public const int GAME_AREA_HEIGHT = 704;
        public const int HEADSUP_DISPLAY = 128; // 2 times 64

        public const int WINDOW_WIDTH = 1024;
        public const int WINDOW_HEIGHT = GAME_AREA_HEIGHT + HEADSUP_DISPLAY;

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
        public const long DELAY_KEYBOARD = 100; // For keyboard pressing event
        public const long CHEAT_INPUT_TIMEOUT = 4000;
        public const long TITLE_DELAY = 150;
        public enum roomTypes { Start, Boss, Merchant };
        public enum Direction { Left, Right, Up, Down };
        public const int RESET_DIRECTION = 4; // The 5th "direction" for resetting 
        public enum CollisionType { None, Left, Right, Top, Bottom, };

        public const int PAUSE_CONTINUE_X = 185;
        public const int PAUSE_CONTINUE_Y = 440;
        public const int PAUSE_QUIT_X = 310;
        public const int PAUSE_QUIT_Y = 512;

        // Used to initlize level eagle startup location
        public static int[] STRAT_UP_INDEX = new int[] { 5, 2 };

        /* Some fields created for level cycling in sprint 3.
         * Future updates might just get rid of these.
         */
        public const int CYCLE_BOUND = 17;

        // Index for blocks 
        public const int SOLID_BLOCK_BOUND = 32;
        public const int VERTICAL_MOVE_BLOCK = 40;
        public const int HORIZONTAL_MOVE_BLOCK = 41;

        // Index for item 
        public const int FIREWALL_ITEM = -257;
        public const int BOMB_ITEM = -258;
        public const int THROWING_KNIFE_ITEM = -259;
        public const int FOOD_ITEM = -260;
        public const int TRIFORCE_ITEM = -261;
        public const int BURNING_FIRE = -262;
        public const int RUPY_ITEM = -263;
        public const int SHIELD_ITEM = -264;
        public const int HEART_ITEM = -265;
        public const int REFILL_ITEM = -266;
        public const int PRICE = -267;


        // Index for enemy 
        public const int SNAKE_ENEMY = -1;
        public const int BUG_ENEMY = -2;
        public const int MOBLIN_ENEMY = -3;
        public const int BOSS_ENEMY = -4;
        public const int NPC_OLD_MAN = -5;
        public const int NPC_OLD_WOMAN = -6;

        //Cheats
        public static bool GODMODE { get; set; } = false;
        // Misc
        public static Random RND = new Random(); 
        public const int PRNG_WEIGHT = 10; // Increse this to make it more random

        public static SavedSettings saveSets = new JsonParser(SaveDatabase.saveFilePath, JsonParser.ParseMode.settingsMode).getCurrentSavedSettings();


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
