using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackAndSlash
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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

        // Sprites  
        private SpriteBG SpriteBG;
        public ISprite PlayerSprite { set { SpriteHolder = value; } }
        private ISprite SpriteHolder { get; set; }
        public IItem ItemHolder { get; set; }
        public IBlock BlockHolder { get; set; }
        
        private Texture2D textureFirewall { get; set; }
        
        public SnakeEnemy snakefirst;
        public BugEnemy bugfirst;
        private Vector2 EnemyPosition;


        public FirewallItem firewallFirst;
        public BombItem bombFirst;

        // Character positions 
        private Vector2 relPositionMC; // Relative position. As position in display window 

        public Vector2 Pos
        {
            get
            {
                return relPositionMC;
            }
            set
            {
                relPositionMC = value;
            }
        }

        // Attributes for camera clipping and FX 

        // Camera clipping 
        private Vector2 horizontal;
        private Vector2 vertical;

        // Object lists
        List<Object> controllerList;
        public List<IBlock> blockList { get; set; }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        public void reset() {
            this.Initialize();
            this.LoadContent();
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

            Player = new Player();//Player object
            SpriteFactory.Instance.CreateRightPlayer();//Set up the inital sprite

            controllerList = new List<Object>();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));

            // Setup window size 
            graphics.PreferredBackBufferWidth = GlobalSettings.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GlobalSettings.WINDOW_HEIGHT;


            // Place character on the up left corner 
            relPositionMC.X = GlobalSettings.WINDOW_WIDTH / GlobalSettings.MAX_DISPLAY_DIV + 1;
            relPositionMC.Y = GlobalSettings.WINDOW_HEIGHT / GlobalSettings.MAX_DISPLAY_DIV + 1;

            // Setup camera clipping aera 
            horizontal.X = graphics.PreferredBackBufferWidth / GlobalSettings.MAX_DISPLAY_DIV;
            horizontal.Y = graphics.PreferredBackBufferWidth - 2 * horizontal.X;
            vertical.X = graphics.PreferredBackBufferHeight / GlobalSettings.MAX_DISPLAY_DIV;
            vertical.Y = graphics.PreferredBackBufferHeight - 2 * vertical.X;

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

            SpriteBG = new SpriteBG(SpriteFactory.Instance.CreateBG(), graphics);
           // SpriteHolder = SpriteFactory.Instance.CreateRightPlayer();

            snakefirst = new SnakeEnemy(new Vector2(300,200), GraphicsDevice, spriteBatch);
            bugfirst = new BugEnemy(new Vector2(200,100), GraphicsDevice, spriteBatch);

            //snakefirst.LoadContent();
            //bugfirst.LoadContent();

            // Items
            firewallFirst = new FirewallItem(new Vector2(200, 200), spriteBatch);
            bombFirst = new BombItem(new Vector2(200, 200), spriteBatch);
            ItemHolder = firewallFirst;

            //firewallFirst.LoadContent(); ;

            //Create list of blocks and set blockholder to first block in the list
            blockList = new List<IBlock>()
            {
                {SpriteFactory.Instance.CreateChipBlock(spriteBatch)},
                {SpriteFactory.Instance.CreateSmoothBlock(spriteBatch)}
            };
            BlockHolder = blockList[0];
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
            Player.Update();
            
            snakefirst.Update(gameTime);
            bugfirst.Update(gameTime);

            ItemHolder.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //SpriteBG.Draw(spriteBatch, new Vector2(relPositionMC.X, relPositionMC.Y), Color.White);sd
            BlockHolder.Draw();
            Player.Draw(spriteBatch, new Vector2(relPositionMC.X, relPositionMC.Y), Color.White);
            ItemHolder.Draw();
            snakefirst.Draw();
            bugfirst.Draw();

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
