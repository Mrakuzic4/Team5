

namespace HackAndSlash
{
    // Left for some future global (throught out the game kind) variables 
    

    class GlobalSettings
    {
        public const int WINDOW_WIDTH = 600;
        public const int WINDOW_HEIGHT = 400;

        public const int MAX_DISPLAY_DIV = 6;

        public const int STEP_SIZE_X = 5;
        public const int STEP_SIZE_Y = 5;
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

    class ImageDatabase
    {
        public ImageFile playerMoveUp;
        public ImageFile playerMoveDown;
        public ImageFile playerMoveLeft;
        public ImageFile playerMoveRight;

        public ImageFile snakeMoveLeft;
        public ImageFile snakeAttackLeft;
        public ImageFile snakeDie;
        public ImageFile snakeIdle;

        public ImageFile bugMoveUp;
        public ImageFile bugMoveDown;
        public ImageFile bugIdle;
        public ImageFile bugDie;

        public ImageFile fireWall;
        public ImageFile BG;

        public ImageDatabase()
        {
            // Initilize images with path/name, column and row 

            playerMoveUp = new ImageFile("images/sucUp", 1, 7);
            playerMoveDown = new ImageFile("images/sucDown", 1, 7);
            playerMoveLeft = new ImageFile("images/sucLeft", 1, 7);
            playerMoveRight = new ImageFile("images/sucOva", 1, 7);

            snakeMoveLeft = new ImageFile("images/SnakeMoving", 1, 10); 
            snakeAttackLeft = new ImageFile("images/SnakeAttack", 1, 10); 
            snakeDie = new ImageFile("images/SnakeDie", 1, 10); 
            snakeIdle = new ImageFile("images/SnakeIdle", 1, 10);

            bugMoveUp = new ImageFile("images/BugMoveUp", 1, 6);
            bugMoveDown = new ImageFile("images/BugMoveDown", 1, 6);
            bugIdle = new ImageFile("images/BugIdle", 1, 6);
            bugDie = new ImageFile("images/BugDie", 1, 6);

            fireWall = new ImageFile("images/firewall", 1, 2);
            BG = new ImageFile("images/BG", 1, 1);
        }
    }
}
