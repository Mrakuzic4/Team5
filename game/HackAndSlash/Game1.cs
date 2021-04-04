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

        public bool devMode = true;     // Whether or not o enable some keybind and functionalities 

        public bool elapsing = false;    // set to false when invoking pause, bag, transition, etc.
        public bool gamePaused = false; //set to true if pause button has been pressed
        public bool gameOver = false;   
        public bool titleMenu = true;
        public bool gameWon = false;

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

        //Player's Health
        private ISprite DrawHealth;


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
        public Map currentMapInfo;
        public Level currentLevel;
        public LevelCycling levelCycleRecord; // Remove item from the map after pickup 
        private MapGenerator generator;
        private int transitionDir;
        private Minimap miniMap;
        // Partically due to planning, "level" and "map" are used interchangeable 

        private Color defaultFill = Color.Black; 

        // Sprites  
        public IItem ItemHolder { get; set; }
        
        // Please consider removing these explicit declerations since some of them are pretty much 
        // only used in load content and never used once new rooms are loaded 
        public SnakeEnemy snakefirst;
        public BugEnemy bugfirst;
        public MoblinEnemy moblinfirst;

        public FirewallItem firewallFirst;
        public BombItem bombFirst;
        public ThrowingKnifeItem throwingKnifeFirst;

        //UI Elements
        private PauseOverlay pauseOverlay;
        private GameOverOverlay gameOverOverlay;
        private TitleScreenOverlay titleScreen;
        private GameWonOverlay gameWonScreenOverlay;
        public Boolean inGameOverAnimation;
        public Boolean inGameWonAnimation;
        

        private Texture2D textSprites;

        // Object lists
        List<Object> controllerList;
        public List<IBlock> blockList { get; set; }
        public List<ILevel> levelList { get; set; }
        public List<IEnemy> enemyList { get; set; }
        public List<IItem> itemList { get; set; }
        public List<IItem> useableItemList { get; set; }

        public ISpecialCases specialCases; 

        public int numOfEnemy { get; set; }
        public int numOfDropped { get; set; }

        /* ============================================================
         * ======================== Methods ===========================
         * ============================================================ */
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            gameSettings = new GlobalSettings();
            Content.RootDirectory = "Content";
        }

        public void reset(int Direction) {

            transitionDir = Direction;
            Level NextLevel;

            if(Direction == 5)
            {
                currentLevel = generator.getLevel(GraphicsDevice, spriteBatch);
                currentLevel.levelCycler = levelCycleRecord;
                currentLevel.currentMapInfo = currentMapInfo;
                currentLevel.Generate();

                blockList = generator.GetBlockList(spriteBatch, SpriteFactory.Instance, currentMapInfo);
                enemyList = generator.GetEnemyList(spriteBatch, GraphicsDevice, this);
                itemList = generator.GetItemList(spriteBatch, this);

                numOfEnemy = enemyList.Count();
                numOfDropped = 0;
            }
            else
            {
                currentMapInfo = currentLevel.NextRoom(Direction);
                generator = new MapGenerator(currentMapInfo);

                // Pre-launch warmup for transition 
                NextLevel = generator.getLevel(GraphicsDevice, spriteBatch);
                NextLevel.levelCycler = levelCycleRecord;
                NextLevel.currentMapInfo = currentMapInfo;
                NextLevel.Generate();

                currentLevel.nextLevelTexture = NextLevel.levelTexture;
                currentLevel.transitioning = true;
                currentLevel.transFinsihed = false;

                // Empty the list to avoid things being drawn during transition 
                // Recreation of the lists is in Update() 
                blockList = new List<IBlock>();
                enemyList = new List<IEnemy>();
                itemList = new List<IItem>();
            }
            
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
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Get sprite from spriteFactory
            SpriteFactory.Instance.LoadAllTextures(Content);

            //Load sounds
            SoundFactory.Instance.LoadFactory(Content);
            SoundFactory.Instance.TitleScreenSong();

            /* =============================== In game contents =============================== */

            // The level map
            levelCycleRecord = new LevelCycling(); 
            currentLevel = new Level(GraphicsDevice, spriteBatch);
            currentLevel.levelCycler = levelCycleRecord; 
            currentLevel.FirstTimeStartUp();
            currentLevel.Generate();
            currentMapInfo = currentLevel.currentMapInfo;
            generator = new MapGenerator(currentMapInfo);

            miniMap = new Minimap(GraphicsDevice, spriteBatch, levelCycleRecord);
            miniMap.SetPivot(currentLevel.mapIndex);

            specialCases = new LevelEagleSpecialCases(); 

            //Player
            PlayerMain = new Player(this);//Player object

            //Player's Health 
            this.DrawHealth = new DrawPlayerHealth(this,SpriteFactory.Instance.GetEmptyHeart(), 
                SpriteFactory.Instance.GetHalfHeart(),
                SpriteFactory.Instance.GetFullHeart(),
                SpriteFactory.Instance.GetFontLife());

            /// TODO: Consider removing all explicit declarations of emeyies and only leave them in the list
            ///       and access only by list index. 
            //Enemy
            snakefirst = new SnakeEnemy(gameSettings.PlayAreaPosition(1, 3), GraphicsDevice, spriteBatch, this);
            bugfirst = new BugEnemy(gameSettings.PlayAreaPosition(10, 2), GraphicsDevice, spriteBatch, this);
            moblinfirst = new MoblinEnemy(gameSettings.PlayAreaPosition(10, 3), GraphicsDevice, spriteBatch, this);

            enemyList = new List<IEnemy>()
            {
                snakefirst,bugfirst,moblinfirst
            };

            // Items
            itemList = generator.GetItemList(spriteBatch, this);
            useableItemList = new List<IItem>();
            textSprites = SpriteFactory.Instance.GetTextCharacters();

            //Create list of blocks
            blockList = generator.GetBlockList(spriteBatch, SpriteFactory.Instance, currentMapInfo);

            //UI Elements
            pauseOverlay = new PauseOverlay(this, SpriteFactory.Instance.GetPauseOverlay(), 
                SpriteFactory.Instance.GetSwordSelector(), SpriteFactory.Instance.GetInventoryText(), SpriteFactory.Instance.GetItemSelector(), spriteBatch);;
            gameOverOverlay = new GameOverOverlay(this, SpriteFactory.Instance.GetGameOverOverlay(),
                            SpriteFactory.Instance.GetSwordSelector(), spriteBatch);
            titleScreen = new TitleScreenOverlay(this, SpriteFactory.Instance.GetTitleScreen(), spriteBatch);
            gameWonScreenOverlay = new GameWonOverlay(this, SpriteFactory.Instance.getGameWonScreen(), SpriteFactory.Instance.GetSwordSelector(), spriteBatch);
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
            if (titleMenu) titleScreen.Update();
            // Pausing or other thing that takes over the screen 
            else if (!elapsing)
            {
                if (gamePaused) pauseOverlay.Update();
                if (gameOver)
                {
                    currentLevel.setGameOver();
                    gameOverOverlay.Update(gameTime);
                    
                }
                if(gameWon)
                {
                    currentLevel.setGameWon();
                    gameWonScreenOverlay.Update(gameTime);
                }
            } 
            // Transitioning between rooms 
            else if (currentLevel.transitioning)
            {
                currentLevel.Update(gameTime);
                miniMap.UpdateTransition(transitionDir);
                // Flagging transFinsihed into true is done in Update() method in Level.cs 
                if (currentLevel.transFinsihed) 
                {
                    currentLevel = generator.getLevel(GraphicsDevice, spriteBatch);
                    currentLevel.levelCycler = levelCycleRecord;
                    currentLevel.currentMapInfo = currentMapInfo;
                    currentLevel.MovedToRoom(transitionDir);
                    currentLevel.Generate();
                    transitionDir = 5;

                    miniMap.FlagExplored(currentLevel.mapIndex);
                    miniMap.SetPivot(currentLevel.mapIndex);

                    blockList = generator.GetBlockList(spriteBatch, SpriteFactory.Instance, currentMapInfo);
                    enemyList = generator.GetEnemyList(spriteBatch, GraphicsDevice, this);
                    itemList = generator.GetItemList(spriteBatch, this);

                    numOfEnemy = enemyList.Count();
                    numOfDropped = 0;
                }
            }
            // When the pause, transit, or bag state is not flagged 
            // i.e. the game area is running 
            else
            {
                foreach (IController controller in controllerList)
                {
                    controller.Update();
                }

                foreach (IEnemy enemy in enemyList) enemy.Update(gameTime);

                foreach (IItem item in itemList) item.Update();
                foreach (IItem item in useableItemList)
                {
                    // keep item uses between rooms
                    if (!itemList.Contains(item))
                    {
                        itemList.Add(item);
                    }
                    item.SetToolbarPosition(useableItemList.IndexOf(item));
                }

                if (blockList.OfType<BlockMovable>().Any())
                {
                    List<BlockMovable> movableBlocks = blockList.OfType<BlockMovable>().ToList();
                    foreach (BlockMovable block in movableBlocks)
                    {
                        block.Update();
                    }
                }

                specialCases.Update(this);
                miniMap.UpdatePlayer(Player.GetPos());

                PlayerMain.Update();
                DrawHealth.Update();

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

            if (titleMenu) titleScreen.Draw();
            else if(gamePaused){ 
                pauseOverlay.Draw(); 
            }else if (gameOver && !inGameOverAnimation)
            {
                gameOverOverlay.Draw();
            }

            else if(gameWon && !inGameWonAnimation)
            {
                gameWonScreenOverlay.Draw();
            }
            else
            {
                currentLevel.Draw();

                foreach (IBlock block in blockList) block.Draw();
                foreach (IEnemy enemy in enemyList) enemy.Draw();
                foreach (IItem item in itemList) item.Draw();

                // Player is not drawn during transition 
                if (!currentLevel.transitioning)
                {
                    PlayerMain.Draw(spriteBatch, Player.GetPos(), Color.White);
                }

                // Masking part of the display, also used for masking extra transition animation 
                currentLevel.DrawOverlay();

                /*
                 * Put UI and Headsup elements below to avoid being covered by overlay  
                 */
                DrawHealth.Draw(spriteBatch, new Vector2(0, 100), Color.White);

                miniMap.Draw();
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void resetCurrentLevelGameOver()
        {
            currentLevel.resetGameOver();
        }
    }
}
