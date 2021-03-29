using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    // This class is used to record all the meta-info of the pictures 
    class ImageDatabase
    {
        public ImageFile playerMoveUp;
        public ImageFile playerMoveDown;
        public ImageFile playerMoveLeft;
        public ImageFile playerMoveRight;

        public ImageFile zeldaDown;
        public ImageFile zeldaUp;
        public ImageFile zeldaLeft;
        public ImageFile zeldaRight;

        public ImageFile zeldaAttackDown;
        public ImageFile zeldaAttackUp;
        public ImageFile zeldaAttackLeft;
        public ImageFile zeldaAttackRight;

        public ImageFile zeldaUseItemDown;
        public ImageFile zeldaUseItemUp;
        public ImageFile zeldaUseItemLeft;
        public ImageFile zeldaUseItemRight;

        public ImageFile fullHeart;
        public ImageFile halfHeart;
        public ImageFile emptyHeart;

        public ImageFile snakeMoveLeft;
        public ImageFile snakeMoveRight;
        public ImageFile snakeDie;
        public ImageFile snakeIdle;

        public ImageFile bugMoveUp;
        public ImageFile bugMoveDown;
        public ImageFile bugMoveRight;
        public ImageFile bugMoveLeft;
        public ImageFile bugIdle;
        public ImageFile bugDie;

        public ImageFile moblinMoveUp;
        public ImageFile moblinMoveDown;
        public ImageFile moblinMoveRight;
        public ImageFile moblinMoveLeft;
        public ImageFile moblinBomb;

        public ImageFile fireWall;
        public ImageFile bomb;
        public ImageFile explosion;
        public ImageFile throwingKnifeUp;
        public ImageFile throwingKnifeDown;
        public ImageFile throwingKnifeLeft;
        public ImageFile throwingKnifeRight;
        public ImageFile food;

        public ImageFile BG;

        public ImageFile BlockMovable;
        public ImageFile BlockAllMight;
        public ImageFile BlockDemo;
        public ImageFile BlockBlank1;
        public ImageFile LevelEagleBorder;
        public ImageFile LevelEagleDoors; 
        public ImageFile[] LevelEagleDoorNormOpen;
        public ImageFile[] LevelEagleHole;

        public ImageFile PauseOverlay;
        public ImageFile SwordSelector;

        public ImageFile TextCharacters;

        public ImageDatabase()
        {
            // Initilize images with path/name, column and row 

            playerMoveUp = new ImageFile("images/sucUp", 1, 7);
            playerMoveDown = new ImageFile("images/sucDown", 1, 7);
            playerMoveLeft = new ImageFile("images/sucLeft", 1, 7);
            playerMoveRight = new ImageFile("images/sucOva", 1, 7);

            zeldaDown = new ImageFile("images/ZeldaDown", 2, 1);
            zeldaUp = new ImageFile("images/ZeldaUp", 2, 1);
            zeldaLeft = new ImageFile("images/ZeldaLeft", 2, 1);
            zeldaRight = new ImageFile("images/ZeldaRight", 2, 1);

            zeldaAttackDown = new ImageFile("images/ZeldaAttackDown", 3, 1);
            zeldaAttackUp = new ImageFile("images/ZeldaAttackUp", 3, 1);
            zeldaAttackRight = new ImageFile("images/ZeldaAttackRight", 3, 1);
            zeldaAttackLeft = new ImageFile("images/ZeldaAttackLeft", 3, 1);

            zeldaUseItemDown = new ImageFile("images/ZeldaUseItemDown", 1, 1);
            zeldaUseItemUp = new ImageFile("images/ZeldaUseItemUp", 1, 1);
            zeldaUseItemLeft = new ImageFile("images/ZeldaUseItemLeft", 1, 1);
            zeldaUseItemRight = new ImageFile("images/ZeldaUseItemRight", 1, 1);

            fullHeart = new ImageFile("images/fullHeart", 1, 1);
            halfHeart = new ImageFile("images/halfHeart", 1, 1);
            emptyHeart = new ImageFile("images/emptyHeart", 1, 1);

            snakeMoveLeft = new ImageFile("images/SnakeLeftMoving", 1, 10);
            snakeMoveRight = new ImageFile("images/SnakeEnemyRight", 1, 10);
            snakeDie = new ImageFile("images/SnakeDie", 1, 10);
            snakeIdle = new ImageFile("images/SnakeIdle", 1, 10);

            bugMoveUp = new ImageFile("images/BugEnemyMoveUp", 1, 6);
            bugMoveDown = new ImageFile("images/BugEnemyMoveDown", 1, 6);
            bugMoveLeft = new ImageFile("images/BugEnemyLeft", 1, 6);
            bugMoveRight = new ImageFile("images/BugEnemyRight", 1, 6);
            bugIdle = new ImageFile("images/BugEnemyIdle", 1, 6);
            bugDie = new ImageFile("images/BugIdle", 1, 6);

            moblinMoveUp = new ImageFile("images/MoblinUp", 1, 2);
            moblinMoveDown = new ImageFile("images/MoblinDown", 1, 2);
            moblinMoveLeft = new ImageFile("images/MoblinLeft", 1, 2);
            moblinMoveRight = new ImageFile("images/MoblinRight", 1, 2);
            moblinBomb = new ImageFile("images/MoblinBomb", 1, 1);

            fireWall = new ImageFile("images/firewall3", 1, 2);
            bomb = new ImageFile("images/Bomb", 1, 1);
            explosion = new ImageFile("images/explosion", 1, 3);
            throwingKnifeUp = new ImageFile("images/throwingKnifeUp", 1, 1);
            throwingKnifeDown = new ImageFile("images/throwingKnifeDown", 1, 1);
            throwingKnifeLeft = new ImageFile("images/throwingKnifeLeft", 1, 1);
            throwingKnifeRight = new ImageFile("images/throwingKnifeRight", 1, 1);
            food = new ImageFile("images/Food", 1, 1);

            BG = new ImageFile("images/BG", 1, 1);

            BlockMovable = new ImageFile("images/blockMovable", 1, 1);

            // For level blocks, they must be the same size as BASE_SCALAR
            BlockDemo = new ImageFile("images/BlockDemo", 1, 1);
            BlockBlank1 = new ImageFile("images/BlockBlank1", 1, 1);
            BlockAllMight = new ImageFile("images/levels/blockAllMight", 1, 1);
            LevelEagleBorder = new ImageFile("images/LevelEagleBorder", 1, 1);
            LevelEagleDoors = new ImageFile("images/levels/eagleDoors", 5, 4);
            LevelEagleDoorNormOpen = new ImageFile[] {
                new ImageFile("images/levels/eagleDoorVerticalUp", 1, 1),
                new ImageFile("images/levels/eagleDoorVerticalDown", 1, 1),
                new ImageFile("images/levels/eagleDoorHorizontalLeft", 1, 1),
                new ImageFile("images/levels/eagleDoorHorizontalRight", 1, 1)};
            LevelEagleHole = new ImageFile[]{
                new ImageFile("images/levels/eagleHoleVerticalUp", 1, 1),
                new ImageFile("images/levels/eagleHoleVerticalDown", 1, 1),
                new ImageFile("images/levels/eagleHoleHorizontalLeft", 1, 1),
                new ImageFile("images/levels/eagleHoleHorizontalRight", 1, 1)};

            PauseOverlay = new ImageFile("images/UI/GamePauseOverlay", 1, 1);
            SwordSelector = new ImageFile("images/UI/SwordSelector", 1, 1);

            TextCharacters = new ImageFile("images/TextCharacters", 4, 13);
        }
    }

    public class MapDatabase
    {
        // Means "Level Demo Map 1"
        public const string demoLevelM1 = @"Content/info/levelDemoM1.json";

        public const string eagleM1 = @"Content/info/eagleM1.json";
        public const string eagleM2 = @"Content/info/eagleM2.json";
        public const string eagleM3 = @"Content/info/eagleM3.json";
        public const string eagleM4 = @"Content/info/eagleM4.json";
        public const string eagleM5 = @"Content/info/eagleM5.json";
        public const string eagleM6 = @"Content/info/eagleM6.json";

        public static string[,] eagle = new string[6,6] { 
                {@"Content/info/eagleM1.json", @"Content/info/eagleM6.json", @"Content/info/eagleM5.json", null, null, null},
                {null, null, @"Content/info/eagleM6.json", null, @"Content/info/eagleM4.json", @"Content/info/eagleM6.json"},
                {@"Content/info/eagleM3.json", @"Content/info/eagleM2.json", @"Content/info/eagleM1.json", @"Content/info/eagleM6.json", @"Content/info/eagleM5.json", null},
                {null, @"Content/info/eagleM3.json", @"Content/info/eagleM2.json", @"Content/info/eagleM1.json", null, null},
                {null, null, @"Content/info/eagleM3.json", null, null, null},
                {null, @"Content/info/eagleM5.json", @"Content/info/eagleM4.json", @"Content/info/eagleM3.json", null, null},
            };

  
    }
}
