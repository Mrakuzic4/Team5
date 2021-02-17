﻿using System;
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

        // Sprites  
        private SpriteBG SpriteBG;
        public ISprite PlayerSprite { set { SpriteHolder = value; } }
        private ISprite SpriteHolder { get; set; }
        private ISprite EnemyHolder { get; set; }
        private ISprite ItemHolder { get; set; }
        private IBlock BlockHolder { get; set; }
        private Texture2D textureSnake { get; set; }
        private Texture2D textureBug { get; set; }
        private Texture2D textureFirewall { get; set; }
        private Texture2D textureChipBlock { get; set; }
        // Character positions 
        private Vector2 relPositionMC; // Relative position. As position in display window 
        private Vector2 absPositionMC; // Absolute position. As position in the game map

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

        List<Object> controllerList;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
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

            // Initilize character position 
            absPositionMC.X = absPositionMC.Y = 0;

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
            SpriteHolder = SpriteFactory.Instance.CreateRightPlayer();

            //Enemy textures
            textureSnake = Content.Load<Texture2D>("images/snakespritesheet");
            textureBug = Content.Load<Texture2D>("images/bug");

            // Item Textures
            textureFirewall = Content.Load<Texture2D>("images/firewall");

            //Block Textures
            textureChipBlock = Content.Load<Texture2D>("images/ChipBlock");
              
            
            EnemyHolder = new EnemySprite(textureSnake, 5, 10);
            ItemHolder = new ItemSprite(textureFirewall, 1, 2);
            BlockHolder = new ChipBlock(textureChipBlock, new Vector2(200, 300), spriteBatch);
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
            KeyboardState stateKB = Keyboard.GetState(); // State for keyboard 
            MouseState stateM = Mouse.GetState();        // State for mouse

            // Update 
            // Handling stateKB 0, which quits. Also left the escape key quit option
            if (stateKB.IsKeyDown(Keys.Escape))
                Exit();

            if (stateKB.IsKeyDown(Keys.Q))
            {
                EnemyHolder = new EnemySprite(textureSnake, 5, 10);
            }

            if (stateKB.IsKeyDown(Keys.O))
            {
                EnemyHolder = new EnemySprite(textureBug, 4, 6);
            }

            foreach (IController controller in controllerList)
            {
                controller.Update();
            }
            SpriteHolder.Update();
            EnemyHolder.Update();
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

            SpriteBG.Draw(spriteBatch, new Vector2(relPositionMC.X, relPositionMC.Y));
            SpriteHolder.Draw(spriteBatch, new Vector2(relPositionMC.X, relPositionMC.Y));
            EnemyHolder.Draw(spriteBatch, new Vector2(300, 300));
            ItemHolder.Draw(spriteBatch, new Vector2(300, 200));

            base.Draw(gameTime);
        }
    }
}
