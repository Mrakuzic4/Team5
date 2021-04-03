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

        private Texture2D ZeldaDown;
        private Texture2D ZeldaUp;
        private Texture2D ZeldaLeft;
        private Texture2D ZeldaRight;

        private Texture2D ZeldaAttackDown;
        private Texture2D ZeldaAttackUp;
        private Texture2D ZeldaAttackLeft;
        private Texture2D ZeldaAttackRight;

        private Texture2D ZeldaUseItemDown;
        private Texture2D ZeldaUseItemUp;
        private Texture2D ZeldaUseItemLeft;
        private Texture2D ZeldaUseItemRight;

        private Texture2D ZeldaDying;

        private Texture2D fullHeart;
        private Texture2D halfHeart;
        private Texture2D emptyHeart;

        private Texture2D BGSprite;

        private Texture2D SnakeIdleSprite;
        private Texture2D SnakeMoveLeft;
        private Texture2D SnakeMoveRight;
        private Texture2D SnakeDieSprite;

        private Texture2D BugIdleSprite;
        private Texture2D BugMoveUpSprite;
        private Texture2D BugMoveDownSprite;
        private Texture2D BugDieSprite;
        private Texture2D BugMoveLeftSprite;
        private Texture2D BugMoveRightSprite;

        private Texture2D MoblinMoveUpSprite;
        private Texture2D MoblinMoveDownSprite;
        private Texture2D MoblinMoveLeftSprite;
        private Texture2D MoblinMoveRightSprite;
        private Texture2D MoblinBombSprite;

        private Texture2D FirewallSprite;
        private Texture2D BombSprite;
        private Texture2D ExplosionSprite;
        private Texture2D ThrowingKnifeLeftSprite;
        private Texture2D ThrowingKnifeRightSprite;
        private Texture2D ThrowingKnifeUpSprite;
        private Texture2D ThrowingKnifeDownSprite;
        private Texture2D FoodSprite;
        private Texture2D TriforceSprite;

        private Texture2D BlockMovable;
        private Texture2D BlockAllMight; 
        private Texture2D BlockDemo;
        private Texture2D BlockBlank1;
        private Texture2D LevelEagleBorder;
        private Texture2D LevelEagleDoors;
        private Texture2D[] LevelEagleDoorNormOpen;
        private Texture2D[] LevelEagleHoles;

        private Texture2D PauseOverlay;
        private Texture2D SwordSelector;
        private Texture2D GameOverOverlay;
        private Texture2D InventoryText;
        private Texture2D ItemSelector;


        private Texture2D TextCharacters;

        private Texture2D Font_Life;

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

            //Font
            Font_Life = content.Load<Texture2D>(IMDB.Font_Life.pathName);

            //Zelda
            ZeldaDown = content.Load<Texture2D>(IMDB.zeldaDown.pathName);
            ZeldaUp = content.Load<Texture2D>(IMDB.zeldaUp.pathName);
            ZeldaLeft = content.Load<Texture2D>(IMDB.zeldaLeft.pathName);
            ZeldaRight = content.Load<Texture2D>(IMDB.zeldaRight.pathName);

            ZeldaAttackDown = content.Load<Texture2D>(IMDB.zeldaAttackDown.pathName);
            ZeldaAttackUp = content.Load<Texture2D>(IMDB.zeldaAttackUp.pathName);
            ZeldaAttackLeft = content.Load<Texture2D>(IMDB.zeldaAttackLeft.pathName);
            ZeldaAttackRight = content.Load<Texture2D>(IMDB.zeldaAttackRight.pathName);

            ZeldaUseItemDown = content.Load<Texture2D>(IMDB.zeldaUseItemDown.pathName);
            ZeldaUseItemUp = content.Load<Texture2D>(IMDB.zeldaUseItemUp.pathName);
            ZeldaUseItemLeft = content.Load<Texture2D>(IMDB.zeldaUseItemLeft.pathName);
            ZeldaUseItemRight = content.Load<Texture2D>(IMDB.zeldaUseItemRight.pathName);

            ZeldaDying = content.Load<Texture2D>(IMDB.zeldaDying.pathName);

            //Zelda's Health--full heart, half heart, empty heart
            fullHeart = content.Load<Texture2D>(IMDB.fullHeart.pathName);
            halfHeart = content.Load<Texture2D>(IMDB.halfHeart.pathName);
            emptyHeart = content.Load<Texture2D>(IMDB.emptyHeart.pathName);

            // Original image from https://opengameart.org/content/animated-snake
            // Edited in Adobe Fresco to align specific states

            SnakeIdleSprite = content.Load<Texture2D>(IMDB.snakeIdle.pathName);
            SnakeMoveLeft = content.Load<Texture2D>(IMDB.snakeMoveLeft.pathName);
            SnakeMoveRight = content.Load<Texture2D>(IMDB.snakeMoveRight.pathName);
            SnakeDieSprite = content.Load<Texture2D>(IMDB.snakeDie.pathName);

            //Original image sourced from 
            //Edited in Adobe fresco to align specific states

            BugMoveUpSprite = content.Load<Texture2D>(IMDB.bugMoveUp.pathName);
            BugMoveDownSprite = content.Load<Texture2D>(IMDB.bugMoveDown.pathName);
            BugMoveLeftSprite = content.Load<Texture2D>(IMDB.bugMoveLeft.pathName);
            BugMoveRightSprite = content.Load<Texture2D>(IMDB.bugMoveRight.pathName);
            BugDieSprite = content.Load<Texture2D>(IMDB.bugDie.pathName);
            BugIdleSprite = content.Load<Texture2D>(IMDB.bugIdle.pathName);

            MoblinMoveUpSprite = content.Load<Texture2D>(IMDB.moblinMoveUp.pathName);
            MoblinMoveDownSprite = content.Load<Texture2D>(IMDB.moblinMoveDown.pathName);
            MoblinMoveLeftSprite = content.Load<Texture2D>(IMDB.moblinMoveLeft.pathName);
            MoblinMoveRightSprite = content.Load<Texture2D>(IMDB.moblinMoveRight.pathName);
            MoblinBombSprite = content.Load<Texture2D>(IMDB.moblinBomb.pathName);

            //Item Sprites 
            FirewallSprite = content.Load<Texture2D>(IMDB.fireWall.pathName);
            BombSprite = content.Load<Texture2D>(IMDB.bomb.pathName);
            ExplosionSprite = content.Load<Texture2D>(IMDB.explosion.pathName);
            ThrowingKnifeUpSprite = content.Load<Texture2D>(IMDB.throwingKnifeUp.pathName);
            ThrowingKnifeDownSprite = content.Load<Texture2D>(IMDB.throwingKnifeDown.pathName);
            ThrowingKnifeLeftSprite = content.Load<Texture2D>(IMDB.throwingKnifeLeft.pathName);
            ThrowingKnifeRightSprite = content.Load<Texture2D>(IMDB.throwingKnifeRight.pathName);
            FoodSprite = content.Load<Texture2D>(IMDB.food.pathName);
            TriforceSprite = content.Load<Texture2D>(IMDB.triforce.pathName);

            // More Content.Load calls follow
            BGSprite = content.Load<Texture2D>(IMDB.BG.pathName);

            //Blocks
            BlockMovable = content.Load<Texture2D>(IMDB.BlockMovable.pathName);
            
            // Level map related 
            BlockDemo = content.Load<Texture2D>(IMDB.BlockDemo.pathName);
            BlockBlank1 = content.Load<Texture2D>(IMDB.BlockBlank1.pathName);
            BlockAllMight = content.Load<Texture2D>(IMDB.BlockAllMight.pathName);
            LevelEagleBorder = content.Load<Texture2D>(IMDB.LevelEagleBorder.pathName);
            LevelEagleDoors = content.Load<Texture2D>(IMDB.LevelEagleDoors.pathName);
            LevelEagleDoorNormOpen = new Texture2D[] {
                content.Load<Texture2D>(IMDB.LevelEagleDoorNormOpen[0].pathName),
                content.Load<Texture2D>(IMDB.LevelEagleDoorNormOpen[1].pathName),
                content.Load<Texture2D>(IMDB.LevelEagleDoorNormOpen[2].pathName),
                content.Load<Texture2D>(IMDB.LevelEagleDoorNormOpen[3].pathName) };
            LevelEagleHoles = new Texture2D[] {
                content.Load<Texture2D>(IMDB.LevelEagleHole[0].pathName),
                content.Load<Texture2D>(IMDB.LevelEagleHole[1].pathName),
                content.Load<Texture2D>(IMDB.LevelEagleHole[2].pathName),
                content.Load<Texture2D>(IMDB.LevelEagleHole[3].pathName)};

            //UI Related
            PauseOverlay = content.Load<Texture2D>(IMDB.PauseOverlay.pathName);
            GameOverOverlay = content.Load<Texture2D>(IMDB.GameOverOverlay.pathName);

            SwordSelector = content.Load<Texture2D>(IMDB.SwordSelector.pathName);
            TextCharacters = content.Load<Texture2D>(IMDB.TextCharacters.pathName);
            InventoryText = content.Load<Texture2D>(IMDB.InventoryText.pathName);
            ItemSelector = content.Load<Texture2D>(IMDB.ItemSelector.pathName);
        }

        public Texture2D GetGameOverOverlay()
        {
            return GameOverOverlay;
        }

        public Texture2D GetFontLife()
        {
            return Font_Life;
        }

        public Texture2D GetFullHeart()
        {
            return fullHeart;
        }

        public Texture2D GetHalfHeart()
        {
            return halfHeart;
        }

        public Texture2D GetEmptyHeart()
        {
            return emptyHeart;
        }

        public Texture2D CreateBG()
        {
            return BGSprite;
        }
        public Texture2D GetBlockDemo()
        {
            return BlockDemo;
        }
        public Texture2D GetBlockBlank1()
        {
            return BlockBlank1;
        }
        public Texture2D getBlockAllMight()
        {
            return BlockAllMight;
        }
        public Texture2D GetLevelEagleBorder()
        {
            return LevelEagleBorder;
        }
        public Texture2D[] GetLevelEagleDoorNormOpen()
        {
            return LevelEagleDoorNormOpen;
        }
        public Texture2D[] GetLevelEagleHoles()
        {
            return LevelEagleHoles;
        }
        public Texture2D GetLevelEagleDoors()
        {
            return LevelEagleDoors;
        }
        public Texture2D GetPauseOverlay()
        {
            return PauseOverlay;
        }
        public Texture2D GetSwordSelector()
        {
            return SwordSelector;
        }

        public Texture2D GetInventoryText()
        {
            return InventoryText;
        }

        public Texture2D GetItemSelector()
        {
            return ItemSelector;
        }

        public Texture2D GetTextCharacters()
        {
            return TextCharacters;
        }

        public Texture2D CreatePlayer()
        {
            return ZeldaRight; //initial face right
        }
            
        //***********Below are Player movement***************
    
        public void SetUpPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaUp);
        }

        public void SetRightPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaRight);
        }
        public void SetLeftPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaLeft);
        }

        public void SetDownPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaDown);
        }

        //*************Below are Player attack*********************
        public void SetUpAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackUp);
        }

        public void SetRightAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackRight);
        }
        public void SetLeftAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackLeft);
        }

        public void SetDownAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackDown);
        }

        //***************Below is player dying***************//

        public void SetPlayerDying()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaDying.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaDying.C;
            DrawPlayer.Instance.SetTexture(ZeldaDying);
        }

        //*************Below are Player use item*********************
        public void SetUpUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemUp);
        }

        public void SetRightUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemRight);
        }

        public void SetLeftUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemLeft);
        }

        public void SetDownUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemDown);
        }
        //*****************Below are enemies sprites******************

        public ISprite CreateSnakeIdle()
        {
            return new EnemySprite(SnakeIdleSprite, IMDB.snakeIdle.C, IMDB.snakeIdle.R);
        }

        public ISprite CreateSnakeLeftMoving()
        {
            return new EnemySprite(SnakeMoveLeft, IMDB.snakeMoveLeft.C, IMDB.snakeMoveLeft.R);
        }

        public ISprite CreateSnakeRightMoving()
        {
            return new EnemySprite(SnakeMoveRight, IMDB.snakeMoveRight.C, IMDB.snakeMoveRight.R);
        }

        public ISprite CreateSnakeDie()
        {
            return new EnemySprite(SnakeDieSprite, IMDB.snakeDie.C, IMDB.snakeDie.R);
        }



        public ISprite CreateBugIdle()
        {
            return new EnemySprite(BugIdleSprite, IMDB.bugIdle.C, IMDB.bugIdle.R);
        }

        public ISprite CreateBugMoveUp()
        {
            return new EnemySprite(BugMoveUpSprite, IMDB.bugMoveUp.C, IMDB.bugMoveUp.R);
        }

        public ISprite CreateBugMoveDown()
        {
            return new EnemySprite(BugMoveDownSprite, IMDB.bugMoveDown.C, IMDB.bugMoveDown.R);
        }

        public ISprite CreateBugMoveLeft()
        {
            return new EnemySprite(BugMoveLeftSprite, IMDB.bugMoveLeft.C, IMDB.bugMoveLeft.R);
        }


        public ISprite CreateBugMoveRight()
        {
            return new EnemySprite(BugMoveRightSprite, IMDB.bugMoveRight.C, IMDB.bugMoveRight.R);
        }

        public ISprite CreateBugDie()
        {
            return new EnemySprite(BugDieSprite, IMDB.bugDie.C, IMDB.bugDie.R);
        }

        public ISprite CreateMoblinMoveUp()
        {
            return new EnemySprite(MoblinMoveUpSprite, IMDB.moblinMoveUp.C, IMDB.moblinMoveUp.R);
        }

        public ISprite CreateMoblinMoveDown()
        {
            return new EnemySprite(MoblinMoveDownSprite, IMDB.moblinMoveDown.C, IMDB.moblinMoveDown.R);
        }

        public ISprite CreateMoblinMoveLeft()
        {
            return new EnemySprite(MoblinMoveLeftSprite, IMDB.moblinMoveLeft.C, IMDB.moblinMoveLeft.R);
        }


        public ISprite CreateMoblinMoveRight()
        {
            return new EnemySprite(MoblinMoveRightSprite, IMDB.moblinMoveRight.C, IMDB.moblinMoveRight.R);
        }

        public ISprite CreateMoblinDie()
        {
            return new EnemySprite(MoblinMoveDownSprite, IMDB.moblinMoveDown.C, IMDB.moblinMoveDown.R);
        }

        public ISprite CreateMoblinIdle()
        {
            return new EnemySprite(MoblinMoveDownSprite, IMDB.moblinMoveDown.C, IMDB.moblinMoveDown.R);
        }

        public ISprite CreateEnemyBomb()
        {
            return new ItemSprite(MoblinBombSprite, IMDB.moblinBomb.C, IMDB.moblinBomb.R);
        }

        //*************************Below are item  ***************************//

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

        public ISprite CreateTextCharacters()
        {
            return new TextSprite(TextCharacters, IMDB.TextCharacters.C, IMDB.TextCharacters.R);
        }

        public ISprite CreateThrowingKnife(GlobalSettings.Direction direction)
        {
            ISprite knifeSprite = new ItemSprite(ThrowingKnifeUpSprite, IMDB.throwingKnifeUp.C, IMDB.throwingKnifeUp.R); ;
            switch (direction)
            {
                case (GlobalSettings.Direction.Left):
                    knifeSprite = new ItemSprite(ThrowingKnifeLeftSprite, IMDB.throwingKnifeLeft.C, IMDB.throwingKnifeLeft.R);
                    break;
                case (GlobalSettings.Direction.Right):
                    knifeSprite = new ItemSprite(ThrowingKnifeRightSprite, IMDB.throwingKnifeRight.C, IMDB.throwingKnifeRight.R);
                    break;
                case (GlobalSettings.Direction.Down):
                    knifeSprite = new ItemSprite(ThrowingKnifeDownSprite, IMDB.throwingKnifeDown.C, IMDB.throwingKnifeDown.R);
                    break;

            }
            return knifeSprite;
        }

        public ISprite CreateTriforce()
        {
            return new ItemSprite(TriforceSprite, IMDB.triforce.C, IMDB.triforce.R);
        }


        public ISprite CreateFood()
        {
            return new ItemSprite(FoodSprite, IMDB.food.C, IMDB.food.R);
        }

        //*****************************Below are block objects******************************//

        public IBlock CreateBlockMovableVertical(SpriteBatch spriteBatch, int r, int c)
        {
            return new BlockMovable(BlockMovable, new Vector2((c * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET), 
                (r * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET + GlobalSettings.HEADSUP_DISPLAY)), spriteBatch, true);
        }

        public IBlock CreateBlockMovableHorizontal(SpriteBatch spriteBatch, int r, int c)
        {
            return new BlockMovable(BlockMovable, new Vector2((c * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET),
                (r * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET + GlobalSettings.HEADSUP_DISPLAY)), spriteBatch, false);
        }

    }
}
