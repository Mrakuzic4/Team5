

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
        private int INIT_MAX_HEALTH = GlobalSettings.saveSets.MaxHealth; //6 indicates 3 full hearts, maxHealth can change when player picks up heart
        private int maxHealth; 
        private int currentHealth;
       // private int healPower = GlobalSettings.saveSets.HealPower;

        //Player's Shield point
        int shieldPoint;

        public Player(Game1 game)
        {
            playerStateMachine = new PlayerStateMachine(GlobalSettings.Direction.Up, game, this); //inital state face right
            SpriteFactory.Instance.SetRightPlayer();//Set up the inital sprite
            timer = DELAY;//adding delay to the player sprite animation

            this.game = game;

            //Inital Position
            relPositionMC.X = GlobalSettings.WINDOW_WIDTH / 2 - GlobalSettings.BASE_SCALAR / 2; 
            relPositionMC.Y = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;

            //Collision
            playerHitBox = new Rectangle((int)relPositionMC.X + GlobalSettings.PLAYER_HITBOX_X_OFFSET, 
                (int)relPositionMC.Y + GlobalSettings.PLAYER_HITBOX_Y_OFFSET, 
                GlobalSettings.PLAYER_HITBOX_WIDTH, GlobalSettings.PLAYER_HITBOX_HEIGHT);
            playerCollisionDetector = new PlayerCollisionDetector(game);
            playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            swordEnemyCollisionHandler = new SwordEnemyCollisionHandler();

            canMove = true;
            //Player's health initialize
            maxHealth = INIT_MAX_HEALTH;
            currentHealth = maxHealth;
            shieldPoint = 0;
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
        public bool isShield()
        {
            if (shieldPoint == 1) return true;
            return false;
        }
        public void ShieldUp()
        {
            this.shieldPoint = 1;
        }
        public void HealthUp()
        {
            this.maxHealth += 2;
        }

        /// <summary>
        /// Player only allows to move in one direction at a time.
        /// </summary>
        public void unlockMovement()
        {
            this.canMove = true;
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
            //Sound
            SoundFactory.Instance.SwordSlashEffect();

            if (currentHealth == maxHealth)
            {
                //shoot the sword when at full health
                game.fullHealthSword.CollectItem();
                game.fullHealthSword.UseItem(this.GetDir());
            }
            else
            {
                //Sprite Animation and Decorator
                playerStateMachine.Attack();
                this.swordEnemyCollisionHandler.HandleCollision(game.Player, playerCollisionDetector.CheckSwordEnemyCollisions());
            }
        }

        public void Damaged()
        {
            if (!GlobalSettings.GODMODE) 
            {
                //Health goes down by a half heart when damaged
                //If has shield, health only goes down 1.
                //If no shield, health goes down 2.
                currentHealth = currentHealth - 2 + shieldPoint;
                playerStateMachine.Damaged();

                //Sound
                SoundFactory.Instance.LinkDamagedEffect();
                if (currentHealth == 0)
                {
                    this.game.elapsing = false;
                    this.game.gameOver = true;
                    this.game.inGameOverAnimation = true;
                    SoundFactory.Instance.StopSong();
                    SoundFactory.Instance.LinkDeathEffect();
                }
            }

        } 
        // make player heal method in IPlayer and all player wrappers
        public void Healed()
        {
            currentHealth += GlobalSettings.saveSets.HealPower;
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
            int TriggerDistance = (int)(1.5 * GlobalSettings.BASE_SCALAR);
            int compensateDistance = (int)(1.25 * GlobalSettings.BASE_SCALAR);

            int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
            int topBound = GlobalSettings.BORDER_OFFSET + GlobalSettings.HEADSUP_DISPLAY;
            int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
            int leftBound = GlobalSettings.BORDER_OFFSET; 


            // left 
            if (this.relPositionMC.X < leftBound)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Left))
                {
                    if (this.relPositionMC.X < leftBound - TriggerDistance)
                    {
                        game.currentLevel.TransDir = (int)GlobalSettings.Direction.Left;
                        SetPos(new Vector2(rightBound + GlobalSettings.BASE_SCALAR, relPositionMC.Y));
                        game.reset((int)GlobalSettings.Direction.Left);
                    }
                    
                }
                else
                    this.relPositionMC.X = leftBound;
            }

            // right 
            if (this.relPositionMC.X > rightBound)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Right))
                {
                    if (this.relPositionMC.X > rightBound + TriggerDistance)
                    {
                        game.currentLevel.TransDir = (int)GlobalSettings.Direction.Right;
                        SetPos(new Vector2(GlobalSettings.BASE_SCALAR, relPositionMC.Y));
                        game.reset((int)GlobalSettings.Direction.Right);
                    }
                    
                }
                else
                    this.relPositionMC.X = rightBound;
            }

            // Up
            if (this.relPositionMC.Y < topBound)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Up))
                {
                    if (this.relPositionMC.Y < topBound - TriggerDistance)
                    {
                        game.currentLevel.TransDir = (int)GlobalSettings.Direction.Up;
                        SetPos(new Vector2(relPositionMC.X, bottomBound + GlobalSettings.BASE_SCALAR));
                        game.reset((int)GlobalSettings.Direction.Up);
                    }
                }
                else
                    this.relPositionMC.Y = topBound;
            }

            //Bottom
            if (this.relPositionMC.Y > bottomBound)
            {
                if (game.currentLevel.CanGoThrough((int)GlobalSettings.Direction.Down))
                {
                    if (this.relPositionMC.Y > bottomBound + TriggerDistance)
                    {
                        game.currentLevel.TransDir = (int)GlobalSettings.Direction.Down;
                        SetPos(new Vector2(relPositionMC.X, topBound - compensateDistance));
                        game.reset((int)GlobalSettings.Direction.Down);
                    }
                }
                else
                    this.relPositionMC.Y = bottomBound;
            }   
        }

       
    }
    
}
