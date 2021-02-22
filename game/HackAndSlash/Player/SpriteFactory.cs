using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Texture2D Zelda;
        private Texture2D ZeldaDown;
        private Texture2D ZeldaUp;
        private Texture2D ZeldaLeft;
        private Texture2D ZeldaRight;

        private Texture2D ZeldaAttackDown;
        private Texture2D ZeldaAttackUp;
        private Texture2D ZeldaAttackLeft;
        private Texture2D ZeldaAttackRight;


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
            //PlayerSpriteRight = content.Load<Texture2D>(IMDB.playerMoveRight.pathName);
            //PlayerSpriteLeft = content.Load<Texture2D>(IMDB.playerMoveLeft.pathName);
            //PlayerSpriteUp = content.Load<Texture2D>(IMDB.playerMoveUp.pathName);
            //PlayerSpriteDown = content.Load<Texture2D>(IMDB.playerMoveDown.pathName);

            Zelda = content.Load<Texture2D>(IMDB.zelda.pathName);

            ZeldaDown = content.Load<Texture2D>(IMDB.zeldaDown.pathName);
            ZeldaUp = content.Load<Texture2D>(IMDB.zeldaUp.pathName);
            ZeldaLeft = content.Load<Texture2D>(IMDB.zeldaLeft.pathName);
            ZeldaRight = content.Load<Texture2D>(IMDB.zeldaRight.pathName);

            ZeldaAttackDown = content.Load<Texture2D>(IMDB.zeldaAttackDown.pathName);
            ZeldaAttackUp = content.Load<Texture2D>(IMDB.zeldaAttackUp.pathName);
            ZeldaAttackLeft = content.Load<Texture2D>(IMDB.zeldaAttackLeft.pathName);
            ZeldaAttackRight = content.Load<Texture2D>(IMDB.zeldaAttackRight.pathName);

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


            // More Content.Load calls follow
            BGSprite = content.Load<Texture2D>(IMDB.BG.pathName);

        }

        public Texture2D CreateBG()
        {
            return BGSprite;
        }

        public Texture2D CreatePlayer()
        {
            return ZeldaRight; //initial face right
        }
            
        //***********Below are Player movement***************
    
        public void CreateUpPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaUp);
        }

        public void CreateRightPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaRight);
        }
        public void CreateLeftPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaLeft);
        }

        public void CreateDownPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaDown);
        }

        //*************Below are Player attack*********************
        public void CreateUpAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackUp);
        }

        public void CreateRightAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackRight);
        }
        public void CreateLeftAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackLeft);
        }

        public void CreateDownAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackDown);
        }

        //*****************Below are enemies sprites******************

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

    }
}
