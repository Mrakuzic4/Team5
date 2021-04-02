

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class Player : IPlayer
    {
        private PlayerStateMachine playerStateMachine;
        private Game1 game;

        private int timer;
        private const int DELAY = 7;//adding delay to the player sprite animation

        //Collision
        private Rectangle playerHitBox;

        private PlayerCollisionDetector playerCollisionDetector;

        private PlayerBlockCollisionHandler playerBlockCollisionHandler;
        private PlayerEnemyCollisionHandler playerEnemyCollisionHandler;
        private SwordEnemyCollisionHandler swordEnemyCollisionHandler;


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

        //Player's Movement Lock
        private bool canMove;

        //Player's Health
        private const int INIT_MAX_HEALTH = 6; //6 indicates 3 full hearts, maxHealth can change when player picks up heart
        private int maxHealth; 
        private int currentHealth;


        public Player(Game1 game)
        {
            playerStateMachine = new PlayerStateMachine(GlobalSettings.Direction.Right, game, this); //inital state face right
            SpriteFactory.Instance.SetRightPlayer();//Set up the inital sprite
            timer = DELAY;//adding delay to the player sprite animation

            this.game = game;

            //Inital Position
            relPositionMC.X = GlobalSettings.BORDER_OFFSET;
            relPositionMC.Y = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;

            //Collision
            playerHitBox = new Rectangle((int)relPositionMC.X + GlobalSettings.PLAYER_HITBOX_X_OFFSET, 
                (int)relPositionMC.Y + GlobalSettings.PLAYER_HITBOX_Y_OFFSET, 
                GlobalSettings.PLAYER_HITBOX_WIDTH, GlobalSettings.PLAYER_HITBOX_HEIGHT);
            playerCollisionDetector = new PlayerCollisionDetector( game);
            playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            swordEnemyCollisionHandler = new SwordEnemyCollisionHandler();

            canMove = true;

            //Player's health initialize
            maxHealth = INIT_MAX_HEALTH;
            currentHealth = maxHealth;
        }

        public void unlockMovement()
        {
            this.canMove = true;
        }

        public int GetMaxHealth()
        {
            return this.maxHealth;
        }
        public int GetCurrentHealth()
        {
            return this.currentHealth;
        }

        public Vector2 GetPos()
        {
            return relPositionMC;
        }

        public void SetPos(Vector2 pos)
        {
            relPositionMC = pos;
        }

        public GlobalSettings.Direction GetDir()
        {
            return playerStateMachine.Direction;
        }

        public void ChangeDirection(GlobalSettings.Direction dir)
        {
            playerStateMachine.ChangeDirection(dir);
        }

        public void Move()
        {
            if (canMove)
            {
                canMove = false;

                timer--;
                if (timer == 0)
                {
                    DrawPlayer.Instance.Frame++;
                    timer = DELAY;
                }
                playerStateMachine.Move();
            }
        }

        public void Attack()
        {
            //Sprite Animation and Decorator
            playerStateMachine.Attack();

            this.swordEnemyCollisionHandler.HandleCollision(game.Player, playerCollisionDetector.CheckSwordEnemyCollisions());
        }

        public void Damaged()
        {
            //Health goes down by a half heart when damaged
            currentHealth--;
            playerStateMachine.Damaged();
            if (currentHealth == 0)
            {
                this.game.elapsing = false;
                this.game.gameOver = true;
                this.game.inGameOverAnimation = true;
                //game.reset(5); //Reset the room upon player's death.
                //currentHealth = maxHealth; //fully heal Player after death
            }
        } 
        // make player heal method in IPlayer and all player wrappers
        public void Healed()
        {
            currentHealth++;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public void Update()
        {
            DrawPlayer.Instance.Update();

            //Player Boundary Check
            stayInBoundary();

            //Player Collision Detector
            //hitbox for player, wraps around player.
            playerHitBox.Location = new Point((int)relPositionMC.X + GlobalSettings.PLAYER_HITBOX_X_OFFSET,
                (int)relPositionMC.Y + GlobalSettings.PLAYER_HITBOX_Y_OFFSET);
            //Player Block Collision
            playerBlockCollisionHandler.HandleCollision(game.Player, playerCollisionDetector.CheckBlockCollisions(playerHitBox));
            //Player Enemy Collision
            playerEnemyCollisionHandler.HandleCollision(game.Player, playerCollisionDetector.CheckEnemyCollisions(playerHitBox));

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            DrawPlayer.Instance.Draw(spriteBatch, location, color);
        }

        public void UseItem()
        {
            //Player Animation
            playerStateMachine.UseItem();
            //Item animation
            game.ItemHolder.UseItem(GetDir());

        }

        private void stayInBoundary()
        {
            // Note that the level used a different directional index
            int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
            int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;

            // left 
            if (this.relPositionMC.X < GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Left))
                {
                    game.currentLevel.TransDir = (int)GlobalSettings.Direction.Left;
                    SetPos(new Vector2(rightBound + GlobalSettings.BASE_SCALAR, relPositionMC.Y));
                    game.reset((int)GlobalSettings.Direction.Left);
                }
                else
                    this.relPositionMC.X = GlobalSettings.BORDER_OFFSET;
            }

            // right 
            if (this.relPositionMC.X > rightBound + GlobalSettings.BASE_SCALAR)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Right))
                {
                    game.currentLevel.TransDir = (int)GlobalSettings.Direction.Right;
                    SetPos(new Vector2(GlobalSettings.BASE_SCALAR, relPositionMC.Y));
                    game.reset((int)GlobalSettings.Direction.Right);
                }
                else
                    this.relPositionMC.X = rightBound;
            }

            // Up
            if (this.relPositionMC.Y < GlobalSettings.BORDER_OFFSET + GlobalSettings.HEADSUP_DISPLAY - GlobalSettings.BASE_SCALAR)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Up))
                {
                    game.currentLevel.TransDir = (int)GlobalSettings.Direction.Up;
                    SetPos(new Vector2(relPositionMC.X, bottomBound));
                    game.reset((int)GlobalSettings.Direction.Up);
                }
                else
                    this.relPositionMC.Y = GlobalSettings.BORDER_OFFSET + GlobalSettings.HEADSUP_DISPLAY;
            }

            //Bottom
            if (this.relPositionMC.Y > bottomBound + GlobalSettings.BASE_SCALAR)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Down))
                {
                    game.currentLevel.TransDir = (int)GlobalSettings.Direction.Down;
                    SetPos(new Vector2(relPositionMC.X, GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BASE_SCALAR));
                    game.reset((int)GlobalSettings.Direction.Down);
                }
                else
                    this.relPositionMC.Y = bottomBound;
            }   
        }

       
    }
    
}
