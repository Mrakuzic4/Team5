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

            // Original image from https://www.spriters-resource.com/fullview/146744/
            // Edited in Photoshop to align the textures 
            // Create the maincharacter sprite with delay, might look odd depending on your machine 
            PlayerSpriteRight = content.Load<Texture2D>("images/sucOva");
            PlayerSpriteLeft = content.Load<Texture2D>("images/sucLeft");
            PlayerSpriteUp = content.Load<Texture2D>("images/sucUp");
            PlayerSpriteDown = content.Load<Texture2D>("images/sucDown");
            
            // Original image from https://opengameart.org/content/animated-snake
            // Edited in Adobe Fresco to align specific states

            SnakeIdleSprite = content.Load<Texture2D>("images/SnakeIdle");
            SnakeMoveSprite = content.Load<Texture2D>("images/SnakeMoving");
            SnakeAttackSprite = content.Load<Texture2D>("images/SnakeAttack");
            SnakeDieSprite = content.Load<Texture2D>("images/SnakeDie");

            //Original image sourced from 
            //Edited in Adobe fresco to align specific states

            BugMoveUpSprite = content.Load<Texture2D>("images/BugMoveUp");
            BugMoveDownSprite = content.Load<Texture2D>("images/BugMoveDown");
            BugDieSprite = content.Load<Texture2D>("images/BugDie");
            BugIdleSprite = content.Load<Texture2D>("images/BugIdle");


            // More Content.Load calls follow
            BGSprite = content.Load<Texture2D>("images/BG");

        }

        public Texture2D CreateBG()
        {
            return BGSprite;
        }

        public IPlayer CreateUpPlayer()
        {
            return new Player(PlayerSpriteUp, 1, 7, 4);
        }

        public IPlayer CreateRightPlayer()
        {
            return new Player(PlayerSpriteRight, 1, 7, 4);
        }
        public IPlayer CreateLeftPlayer()
        {
            return new Player(PlayerSpriteLeft, 1, 7, 4);
        }

        public IPlayer CreateDownPlayer()
        {
            return new Player(PlayerSpriteDown, 1, 7, 4);
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
            return new EnemySprite(SnakeIdleSprite, 1, 10);
            //return new AnimatedSpriteMC(SnakeIdleSprite, 1, 10, 0);
        }

        public ISprite CreateSnakeMoving()
        {
            return new EnemySprite(SnakeMoveSprite, 1, 10);
            //return new AnimatedSpriteMC(SnakeMoveSprite, 1, 10, 0);
        }

        public ISprite CreateSnakeAttack()
        {
            return new EnemySprite(SnakeAttackSprite, 1, 10);
            //return new AnimatedSpriteMC(SnakeAttackSprite, 1, 10, 0);
        }

        public ISprite CreateSnakeDie()
        {
            return new EnemySprite(SnakeDieSprite, 1, 10);
            //return new AnimatedSpriteMC(SnakeDieSprite, 1, 10, 0);
        }

        public ISprite CreateBugIdle()
        {
            return new EnemySprite(BugIdleSprite, 1, 6);
            //return new AnimatedSpriteMC(BugIdleSprite, 1, 6, 0);
        }

        public ISprite CreateBugMoveUp()
        {
            return new EnemySprite(BugMoveUpSprite, 1, 6);
            //return new AnimatedSpriteMC(BugMoveUpSprite, 1, 6, 0);
        }

        public ISprite CreateBugMoveDown()
        {
            return new EnemySprite(BugMoveDownSprite, 1, 6);
            //return new AnimatedSpriteMC(BugMoveDownSprite, 1, 6, 0);
        }

        public ISprite CreateBugDie()
        {
            return new EnemySprite(BugDieSprite, 1, 6);
            //return new AnimatedSpriteMC(BugDieSprite, 1, 6, 0);
        }

    }
}
