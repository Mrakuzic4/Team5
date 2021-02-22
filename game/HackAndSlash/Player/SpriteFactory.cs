using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace HackAndSlash
{   /// <summary>
    /// This is a class for all sprites including player sprite, enemy sprite, and obstacle sprite.
    /// Every sprite is required to implement ISprite Interface.
    /// </summary>
    class SpriteFactory
    {
        ImageDatabase IMDB;

        private Texture2D PlayerSpriteRight;
        private Texture2D PlayerSpriteLeft;
        private Texture2D PlayerSpriteUp;
        private Texture2D PlayerSpriteDown;

        private Texture2D BGSprite;

        private Texture2D SnakeIdleSprite;
        private Texture2D SnakeMoveSprite;
        private Texture2D SnakeAttackSprite;
        private Texture2D SnakeDieSprite;

        private Texture2D BugIdleSprite;
        private Texture2D BugMoveUpSprite;
        private Texture2D BugMoveDownSprite;
        private Texture2D BugDieSprite;

        private Texture2D FirewallSprite;
        private Texture2D BombSprite;
        private Texture2D ExplosionSprite;

        private Texture2D ChipBlock;
        private Texture2D SmoothBlock;


        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            IMDB = new ImageDatabase();
            // Original image from https://www.spriters-resource.com/fullview/146744/
            // Edited in Photoshop to align the textures 
            // Create the maincharacter sprite with delay, might look odd depending on your machine 
            PlayerSpriteRight = content.Load<Texture2D>(IMDB.playerMoveRight.pathName);
            PlayerSpriteLeft = content.Load<Texture2D>(IMDB.playerMoveLeft.pathName);
            PlayerSpriteUp = content.Load<Texture2D>(IMDB.playerMoveUp.pathName);
            PlayerSpriteDown = content.Load<Texture2D>(IMDB.playerMoveDown.pathName);

            // Original image from https://opengameart.org/content/animated-snake
            // Edited in Adobe Fresco to align specific states

            SnakeIdleSprite = content.Load<Texture2D>(IMDB.snakeIdle.pathName);
            SnakeMoveSprite = content.Load<Texture2D>(IMDB.snakeMoveLeft.pathName);
            SnakeAttackSprite = content.Load<Texture2D>(IMDB.snakeAttackLeft.pathName);
            SnakeDieSprite = content.Load<Texture2D>(IMDB.snakeDie.pathName);

            //Original image sourced from 
            //Edited in Adobe fresco to align specific states

            BugMoveUpSprite = content.Load<Texture2D>(IMDB.bugMoveUp.pathName);
            BugMoveDownSprite = content.Load<Texture2D>(IMDB.bugMoveDown.pathName);
            BugDieSprite = content.Load<Texture2D>(IMDB.bugDie.pathName);
            BugIdleSprite = content.Load<Texture2D>(IMDB.bugIdle.pathName);

            //Item Sprites 
            FirewallSprite = content.Load<Texture2D>(IMDB.fireWall.pathName);
            BombSprite = content.Load<Texture2D>(IMDB.bomb.pathName);
            ExplosionSprite = content.Load<Texture2D>(IMDB.explosion.pathName);

            // More Content.Load calls follow
            BGSprite = content.Load<Texture2D>(IMDB.BG.pathName);

            //Blocks
            ChipBlock = content.Load<Texture2D>(IMDB.ChipBlock.pathName);
            SmoothBlock = content.Load<Texture2D>(IMDB.SmoothBlock.pathName);

        }

        public Texture2D CreateBG()
        {
            return BGSprite;
        }

        public Texture2D CreatePlayer()
        {
            return PlayerSpriteRight; //initial face right
        }

        public void CreateUpPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteUp);
        }

        public void CreateRightPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteRight);
        }
        public void CreateLeftPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteLeft);
        }

        public void CreateDownPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteDown);
        }

        //*******************************
        //Below are player attack state, need new sprite to 
        public void CreateUpAttackPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteUp);
        }

        public void CreateRightAttackPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteRight);
        }
        public void CreateLeftAttackPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteLeft);
        }

        public void CreateDownAttackPlayer()
        {
            DrawPlayer.Instance.SetTexture(PlayerSpriteDown);
        }

        //public ISprite CreateUpPlayer()
        //{
        //    return new AnimatedSpriteMC(PlayerSpriteUp, 1, 7, 4);
        //}

        //public ISprite CreateRightPlayer()
        //{
        //    return new AnimatedSpriteMC(PlayerSpriteRight, 1, 7, 4);
        //}
        //public ISprite CreateLeftPlayer()
        //{
        //    return new AnimatedSpriteMC(PlayerSpriteLeft, 1, 7, 4);
        //}

        //public ISprite CreateDownPlayer()
        //{
        //    return new AnimatedSpriteMC(PlayerSpriteDown, 1, 7, 4);
        //}



        public ISprite CreateSnakeIdle()
        {
            return new EnemySprite(SnakeIdleSprite, IMDB.snakeIdle.C, IMDB.snakeIdle.R);
            //return new AnimatedSpriteMC(SnakeIdleSprite, 1, 10, 0);
        }

        public ISprite CreateSnakeMoving()
        {
            return new EnemySprite(SnakeMoveSprite, IMDB.snakeMoveLeft.C, IMDB.snakeMoveLeft.R);
            //return new AnimatedSpriteMC(SnakeMoveSprite, 1, 10, 0);
        }

        public ISprite CreateSnakeAttack()
        {
            return new EnemySprite(SnakeAttackSprite, IMDB.snakeAttackLeft.C, IMDB.snakeAttackLeft.R);
            //return new AnimatedSpriteMC(SnakeAttackSprite, 1, 10, 0);
        }

        public ISprite CreateSnakeDie()
        {
            return new EnemySprite(SnakeDieSprite, IMDB.snakeDie.C, IMDB.snakeIdle.R);
            //return new AnimatedSpriteMC(SnakeDieSprite, 1, 10, 0);
        }

        public ISprite CreateBugIdle()
        {
            return new EnemySprite(BugIdleSprite, IMDB.bugIdle.C, IMDB.bugIdle.R);
            //return new AnimatedSpriteMC(BugIdleSprite, 1, 6, 0);
        }

        public ISprite CreateBugMoveUp()
        {
            return new EnemySprite(BugMoveUpSprite, IMDB.bugMoveUp.C, IMDB.bugMoveUp.R);
            //return new AnimatedSpriteMC(BugMoveUpSprite, 1, 6, 0);
        }

        public ISprite CreateBugMoveDown()
        {
            return new EnemySprite(BugMoveDownSprite, IMDB.bugMoveDown.C, IMDB.bugMoveDown.R);
            //return new AnimatedSpriteMC(BugMoveDownSprite, 1, 6, 0);
        }

        public ISprite CreateBugDie()
        {
            return new EnemySprite(BugDieSprite, IMDB.bugDie.C, IMDB.bugDie.R);
            //return new AnimatedSpriteMC(BugDieSprite, 1, 6, 0);
        }

        public  ISprite CreateFirewall()
        {
            return new ItemSprite(FirewallSprite, IMDB.fireWall.C, IMDB.fireWall.R);
        }
        public ISprite CreateBomb()
        {
            return new ItemSprite(BombSprite, IMDB.bomb.C, IMDB.bomb.R);
        }
        public ISprite CreateExplosion()
        {
            return new ItemSprite(ExplosionSprite, IMDB.explosion.C, IMDB.explosion.R);
        }

        public IBlock CreateChipBlock(SpriteBatch spriteBatch)
        {
            return new ChipBlock(ChipBlock, new Vector2(100, 300), spriteBatch);
        }

        public IBlock CreateSmoothBlock(SpriteBatch spriteBatch)
        {
            return new SmoothBlock(SmoothBlock, new Vector2(175, 300), spriteBatch);
        }
    }
}
