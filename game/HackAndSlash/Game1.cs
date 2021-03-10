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
        public IItem ItemHolder { get; set; }
        public IBlock BlockHolder { get; set; }
        
        private Texture2D textureFirewall { get; set; }
        
        public SnakeEnemy snakefirst;
        public BugEnemy bugfirst;


        public FirewallItem firewallFirst;
        public BombItem bombFirst;

        private ILevel level; 

        // Object lists
        List<Object> controllerList;
        public List<IBlock> blockList { get; set; }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        public void reset() {
            Initialize();
            LoadContent();
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

            level = new Level(GraphicsDevice, spriteBatch);

            controllerList = new List<Object>();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));

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

            SpriteBG = new SpriteBG(SpriteFactory.Instance.CreateBG(), graphics);
           // SpriteHolder = SpriteFactory.Instance.CreateRightPlayer();

            snakefirst = new SnakeEnemy(new Vector2(300,200), GraphicsDevice, spriteBatch, this);
            bugfirst = new BugEnemy(new Vector2(300,50), GraphicsDevice, spriteBatch, this);
         
            //Player
            PlayerMain = new Player(this);//Player object

            // Items
            firewallFirst = new FirewallItem(new Vector2(200, 200), spriteBatch, this.PlayerMain);
            bombFirst = new BombItem(new Vector2(200, 200), spriteBatch, this.PlayerMain);
            ItemHolder = firewallFirst;

            //firewallFirst.LoadContent(); ;

            //Create list of blocks and set blockholder to first block in the list
            blockList = new List<IBlock>()
            {
                {SpriteFactory.Instance.CreateBlockX(spriteBatch)},
                {SpriteFactory.Instance.CreateBlockWater(spriteBatch)}
            };
            BlockHolder = blockList[0];
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
            level.Draw();
            BlockHolder.Draw();
            PlayerMain.Draw(spriteBatch, Player.GetPos(), Color.White);
            ItemHolder.Draw();
            snakefirst.Draw();
            bugfirst.Draw();

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
