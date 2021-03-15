using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackAndSlash
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Map currentMap; 
        private MapGenerator generator;
        private int mapCycleIndex; 

        //Player
        private IPlayer PlayerMain;
        public IPlayer Player
        {
            get
            {
                return PlayerMain;
            }
            set
            {
                PlayerMain = value;
            }
        }

        //Player's SwordHitBox
        private Rectangle swordHitbox;

        public Rectangle SwordHitBox
        {
            set
            {
                swordHitbox = value;
            }
        }


        // Sprites  
        public IItem ItemHolder { get; set; }
        

        public SnakeEnemy snakefirst;
        public BugEnemy bugfirst;
        public MoblinEnemy moblinfirst;

        public FirewallItem firewallFirst;
        public BombItem bombFirst;
        public ThrowingKnifeItem throwingKnifeFirst;

        // Object lists
        List<Object> controllerList;
        public List<IBlock> blockList { get; set; }
        public List<ILevel> levelList { get; set; }
        public List<IEnemy> enemyList { get; set; }
        public List<IItem> itemList { get; set; }
        /* ============================================================
         * ======================== Methods ===========================
         * ============================================================ */
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        public void reset() {

            /* the following 2 lines shall be modified,
             * currently they're just for sprint 3 cycleing */
            mapCycleIndex++;
            if (mapCycleIndex >= GlobalSettings.CYCLE_BOUND) mapCycleIndex = 0;

            currentMap = new LevelCycling().S3EagleCycle[mapCycleIndex];
            generator = new MapGenerator(currentMap);

            levelList = generator.getLevelList(GraphicsDevice, spriteBatch, currentMap);
            blockList = generator.GetBlockList(spriteBatch,SpriteFactory.Instance);
            enemyList = generator.GetEnemyList(spriteBatch, GraphicsDevice,this);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            mapCycleIndex = 0;

            controllerList = new List<Object>();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));
            controllerList.Add(new GamepadController(this));

            // Setup window size 
            graphics.PreferredBackBufferWidth = GlobalSettings.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GlobalSettings.WINDOW_HEIGHT;

            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            currentMap = new JsonParser(MapDatabase.demoLevelM1).getCurrentMapInfo();
            generator = new MapGenerator(currentMap);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Get sprite from spriteFactory
            SpriteFactory.Instance.LoadAllTextures(Content);

            // SpriteHolder = SpriteFactory.Instance.CreateRightPlayer();

            //Enemy
            snakefirst = new SnakeEnemy(new Vector2(128,200), GraphicsDevice, spriteBatch, this);
            bugfirst = new BugEnemy(new Vector2(700,128), GraphicsDevice, spriteBatch, this);
            moblinfirst = new MoblinEnemy(new Vector2(500, 200), GraphicsDevice, spriteBatch, this);

            enemyList = new List<IEnemy>()
            {
                snakefirst,bugfirst,moblinfirst
            };
         
            //Player
            PlayerMain = new Player(this);//Player object

            // Items
            firewallFirst = new FirewallItem(new Vector2(128, 128), spriteBatch, this);
            bombFirst = new BombItem(new Vector2(192, 192), spriteBatch, this);
            throwingKnifeFirst = new ThrowingKnifeItem(new Vector2(256, 256), spriteBatch, this);

            itemList = new List<IItem>()
            {
                firewallFirst,bombFirst,throwingKnifeFirst
            };

            // A list of level maps for further transition cutscene 
            levelList = new List<ILevel>()
            {
                new Level(GraphicsDevice, spriteBatch, currentMap.Arrangement, currentMap.DefaultBlock,
                currentMap.OpenDoors, currentMap.HiddenDoors, currentMap.LockedDoors) 
            };

            //Create list of blocks
            blockList = generator.GetBlockList(spriteBatch, SpriteFactory.Instance);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
            PlayerMain.Update();

            foreach (IEnemy enemy in enemyList) enemy.Update(gameTime);
            //snakefirst.Update(gameTime);
            //bugfirst.Update(gameTime);
            //moblinfirst.Update(gameTime);

            foreach (IItem item in itemList) item.Update();

            if (blockList.OfType<BlockMovable>().Any())
            {
                List<BlockMovable> movableBlocks = blockList.OfType<BlockMovable>().ToList();
                foreach (BlockMovable block in movableBlocks)
                {
                    block.Update();
                }
            }

            //Collision detector and handler of player

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp);

            foreach (ILevel levelMap in levelList) levelMap.Draw();
            foreach (IBlock block in blockList) block.Draw();
            foreach (IEnemy enemy in enemyList) enemy.Draw();
            PlayerMain.Draw(spriteBatch, Player.GetPos(), Color.White);
            foreach (IItem item in itemList) item.Draw();
            //snakefirst.Draw();
            //moblinfirst.Draw();
            //bugfirst.Draw();

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
