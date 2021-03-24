using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GlobalSettings gameSettings;

        public bool elapsing = true; // set to false when invoking pause, bag, transition, etc.
        public bool gamePaused = false; //set to true if pause button has been pressed

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

        // Level and map related 
        public Map currentMap;
        public Level currentLevel; 
        private MapGenerator generator;
        private int mapCycleIndex;
        // Partically due to planning, "level" and "map" are used interchangeable 

        private Color defaultFill = Color.Black; 

        // Sprites  
        public IItem ItemHolder { get; set; }
        

        public SnakeEnemy snakefirst;
        public BugEnemy bugfirst;
        public MoblinEnemy moblinfirst;

        public FirewallItem firewallFirst;
        public BombItem bombFirst;
        public ThrowingKnifeItem throwingKnifeFirst;

        //UI Elements
        private PauseOverlay pauseOverlay;

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
            gameSettings = new GlobalSettings();
            Content.RootDirectory = "Content";
        }

        public void reset(bool cycleUp) {

            Level NextLevel; 

            /* the following 2 lines shall be modified,
             * currently they're just for sprint 3 cycleing */
            if (cycleUp == true)
            {
                mapCycleIndex++;
                if (mapCycleIndex >= GlobalSettings.CYCLE_BOUND) mapCycleIndex = 0;
            }
            else
            {
                mapCycleIndex--;
                if (mapCycleIndex < 0) mapCycleIndex = GlobalSettings.CYCLE_BOUND - 1;
            }


            currentMap = new LevelCycling().S3EagleCycle[mapCycleIndex];
            generator = new MapGenerator(currentMap);

            NextLevel = generator.getLevel(GraphicsDevice, spriteBatch, currentMap);
            currentLevel.nextLevel = NextLevel.levelTexture;
            currentLevel.transitioning = true;
            currentLevel.transFinsihed = false;

            // Empty the list to avoid things being drawn during transition 
            blockList = new List<IBlock>();
            enemyList = new List<IEnemy>();
            itemList = new List<IItem>();
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

            //Player
            PlayerMain = new Player(this);//Player object
            
            //Enemy
            snakefirst = new SnakeEnemy(gameSettings.PlayAreaPosition(1, 3), GraphicsDevice, spriteBatch, this);
            bugfirst = new BugEnemy(gameSettings.PlayAreaPosition(10, 2), GraphicsDevice, spriteBatch, this);
            moblinfirst = new MoblinEnemy(gameSettings.PlayAreaPosition(10, 3), GraphicsDevice, spriteBatch, this);

            enemyList = new List<IEnemy>()
            {
                snakefirst,bugfirst,moblinfirst
            };
         

            // Items
            firewallFirst = new FirewallItem(gameSettings.PlayAreaPosition(0,0), spriteBatch, this);
            bombFirst = new BombItem(gameSettings.PlayAreaPosition(1, 1), spriteBatch, this);
            throwingKnifeFirst = new ThrowingKnifeItem(gameSettings.PlayAreaPosition(2, 2), spriteBatch, this);

            itemList = new List<IItem>()
            {
                firewallFirst,bombFirst,throwingKnifeFirst
            };

            // A list of level maps for further transition cutscene 
            currentLevel = new Level(GraphicsDevice, spriteBatch, currentMap.Arrangement, currentMap.DefaultBlock,
                currentMap.OpenDoors, currentMap.HiddenDoors, currentMap.LockedDoors); 
            

            //Create list of blocks
            blockList = generator.GetBlockList(spriteBatch, SpriteFactory.Instance);

            //UI Elements
            pauseOverlay = new PauseOverlay(this, SpriteFactory.Instance.GetPauseOverlay(), 
                SpriteFactory.Instance.GetSwordSelector(), spriteBatch);
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
            if (!elapsing)
            {
                if (gamePaused) pauseOverlay.Update();
            } 
            else if (currentLevel.transitioning)
            {
                currentLevel.Update(gameTime);
                if (currentLevel.transFinsihed)
                {
                    currentLevel = generator.getLevel(GraphicsDevice, spriteBatch, currentMap);
                    blockList = generator.GetBlockList(spriteBatch, SpriteFactory.Instance);
                    enemyList = generator.GetEnemyList(spriteBatch, GraphicsDevice, this);
                    itemList = generator.GetItemList(spriteBatch, this);
                }
                    
            }
            else // When the pause or bag state is not flagged 
            {
                foreach (IController controller in controllerList)
                {
                    controller.Update();
                }
                PlayerMain.Update();

                foreach (IEnemy enemy in enemyList) enemy.Update(gameTime);

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
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(defaultFill);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp);

            if (!gamePaused)
            {
                currentLevel.Draw();

                foreach (IBlock block in blockList) block.Draw();
                foreach (IEnemy enemy in enemyList) enemy.Draw();
                PlayerMain.Draw(spriteBatch, Player.GetPos(), Color.White);
                foreach (IItem item in itemList) item.Draw();

                currentLevel.DrawOverlay();

                /*
                 * Put UI elements here 
                 */
            }
            else { 
                pauseOverlay.Draw(); 
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
