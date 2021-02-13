using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprint0
{
    /// <summary>
    /// Sorry for the big mess, had the fun of doing something I'd like to try,
    /// at the expense of making the entire code almost unreadable 
    /// 
    /// The core concept for this implementation is to seperate animation and movement,
    /// thus, there's one part to decide if the sprite moves based on the stateKB,
    /// and another part that decides if it animates. 
    /// 
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Sprites  
        private AnimatedSpriteMC AnimatedSpriteMC; // The animted sprite 
        private StaticSpriteMC StaticSpriteMC;     // The static sprite
        private SpriteBG SpriteBG; 
        private ISprite SpriteHolder;
        // Text sprite and related 
        private TextSprite TextSprite;
        private SpriteFont font;

        // Controls 
        private keyboardCtrl keyboardCtrl;
        private MouseCtrl mouseCtrl;

        // Character positions 
        private Vector2 relPositionMC; // Relative position. As position in display window 
        private Vector2 absPositionMC; // Absolute position. As position in the game map

        // Window size 
        private const bool FULLSCREEN = false; 
        // Fullscreen on multiple displays is a giant pain 
        private const int WINDOW_WIDTH = 600;
        private const int WINDOW_HEIGHT = 400;

        // Attributes for the movement 
        private const int STEP_SIZE_X = 5;
        private const int STEP_SIZE_Y = 5;
        private const int PATROL_CYCLE = 50;
        private int directionFlag = -1;
        private int cycleCounter = 0;

        // Attributes for camera clipping and FX 
        private const int MAX_DISPLAY_DIV = 6;
        private Vector2 horizontal;
        private Vector2 vertical;
        private Vector2 offset; // not used 

        // Attributes related to the state of the game 
        private const int DEFAULT_STATE = 1;
        private List<int> animatedStates = new List<int>() {2, 4, 5};
        private int currentState;


        // Masking method for the movement on X axis 
        private int StateMultiplierX(int currentState)
        {
            if (currentState == 4 || currentState == 5)
                return 1;
            return 0;
        }
        // Masking method for the movement on Y axis 
        private int StateMultiplierY(int currentState)
        {
            if (currentState == 3 || currentState == 5)
                return 1;
            return 0;
        }
        // Controlling the direction of the patrol 
        private int PatrolMultiplier(int currentState)
        {
            if (currentState == 3 || currentState == 4)
            {
                if (cycleCounter == PATROL_CYCLE)
                {
                    cycleCounter = 0;
                    directionFlag = -directionFlag;
                    return 0;
                }
                else
                {
                    cycleCounter++;
                    return directionFlag;
                }
            }
            return 0;
        }

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
            this.IsMouseVisible = true;

            keyboardCtrl = new keyboardCtrl();
            mouseCtrl = new MouseCtrl(graphics);

            offset = new Vector2(0, 0);

            // Initilize character position 
            absPositionMC.X = absPositionMC.Y = 0;

            currentState = DEFAULT_STATE; // Set current state as default 

            // Setup window size 
            if (FULLSCREEN)
            {
                graphics.IsFullScreen = FULLSCREEN;
            } else
            {
                graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
                graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            }

            // Place character on the up left corner 
            relPositionMC.X = WINDOW_WIDTH / MAX_DISPLAY_DIV + 1;
            relPositionMC.Y = WINDOW_HEIGHT / MAX_DISPLAY_DIV + 1;

            // Setup camera clipping aera 
            horizontal.X = graphics.PreferredBackBufferWidth / MAX_DISPLAY_DIV;
            horizontal.Y = graphics.PreferredBackBufferWidth - 2 * horizontal.X;
            vertical.X = graphics.PreferredBackBufferHeight / MAX_DISPLAY_DIV;
            vertical.Y = graphics.PreferredBackBufferHeight - 2 * vertical.X;

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("File");

            // Original image from https://www.spriters-resource.com/fullview/146744/
            // Edited in Photoshop to align the textures 
            // Create the maincharacter sprite with delay, might look odd depending on your machine 
            Texture2D textureMC = Content.Load<Texture2D>("images/sucOva");
            Texture2D textureBG = Content.Load<Texture2D>("images/BG");

            SpriteBG = new SpriteBG(textureBG, graphics);
            AnimatedSpriteMC = new AnimatedSpriteMC(textureMC, 1, 7, 4);
            StaticSpriteMC = new StaticSpriteMC(textureMC, 1, 7);
            TextSprite = new TextSprite(graphics, font);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            Vector2 delta;

            // Update 
            currentState = keyboardCtrl.CheckState(stateKB, currentState);
            currentState = mouseCtrl.CheckState(stateM, currentState, graphics);
            TextSprite.SetState(currentState);

            // Handling stateKB 0, which quits. Also left the escape key quit option
            if (stateKB.IsKeyDown(Keys.Escape) || currentState == 0)
                Exit();


            // Calculate direct keyboard controlled movement (state 5)
            delta.X = STEP_SIZE_X * keyboardCtrl.DirMultiplier(stateKB, Keys.Right, Keys.Left);
            delta.Y = STEP_SIZE_Y * keyboardCtrl.DirMultiplier(stateKB, Keys.Down, Keys.Up);
            // Calculate patrol mode movement (state 3 and 4)
            delta.X += STEP_SIZE_X * PatrolMultiplier(currentState);
            delta.Y += STEP_SIZE_Y * PatrolMultiplier(currentState);
            // Mask off movement if it's not in the permitted state 
            delta.X *= StateMultiplierX(currentState);
            delta.Y *= StateMultiplierY(currentState);

            // Temporally add the movement 
            relPositionMC += delta;

            // Check if it's within camera clipping aera, revoke movement if exceeded 
            if (relPositionMC.X < horizontal.X || relPositionMC.X > horizontal.Y)
            {
                relPositionMC.X -= delta.X;
                SpriteBG.UpdateCoordX((int)delta.X);
            }
            else
            {
                absPositionMC.X += delta.X;
            }    
            if (relPositionMC.Y < vertical.X || relPositionMC.Y > vertical.Y)
                relPositionMC.Y -= delta.Y;
            else
                absPositionMC.Y += delta.Y;

            
            // Pick sprite type, animted or static 
            if (animatedStates.Contains(currentState))
            {
                AnimatedSpriteMC.Update();
                SpriteHolder = AnimatedSpriteMC;
            } else
            {
                StaticSpriteMC.SetCurrentFrame(AnimatedSpriteMC.GetCurrentFrame());
                SpriteHolder = StaticSpriteMC;
            }

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
            TextSprite.Draw(spriteBatch, new Vector2(0, 0));

            base.Draw(gameTime);
        }
    }
}
