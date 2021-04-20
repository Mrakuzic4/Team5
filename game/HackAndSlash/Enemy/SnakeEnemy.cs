using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HackAndSlash.Collision;

namespace HackAndSlash
{
    public class SnakeEnemy : IEnemy
    {
        private snakeStateMachine snakeState; // to keep track of the current state of the snake
        private Vector2 position; // the position of the enemy on screen
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch
        private Game1 game;

        private int timeSinceLastFrame=0; // used to slow down the rate of animation
        private int timeSinceDirectionChange = 0;
        private int deathTimer = 0;
        private int milliSecondsPerFrame=80;
        private int temp = 0; //counter to change states after a certain number of calls to update

        private System.Random random;
        private int[] directionHistory = new int[] { 0, 0, 0, 0 };
        private int randomDirection = 0; //0-left, 1-up, 2-right, 3- down

        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        private Rectangle hitbox;

        private int damageTaken;
        private Color tintColor = Color.White;

        private int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        private int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        //Snake position
        private Rectangle rectangle { get; set; }

        //make the constructor for the class
        public SnakeEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch SB, Game1 game)
        {
            position = startPosition;
            snakeState = new snakeStateMachine();
            Graphics = graphics;
            spriteBatch = SB;
            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            this.game = game;

            random = new System.Random();

            enemyCollisionDetector = new EnemyCollisionDetector(game, this);
            enemyBlockCollision = new EnemyBlockCollision();
            hitbox = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            damageTaken = 0;

            directionHistory[Turn(GlobalSettings.RND.Next(3))] += 1;
        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }



        public Vector2 GetPos()
        {
            return position;
        }

        public void SetPos(Vector2 pos)
        {
            position = pos;
        }

        public int Turn(int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Left:
                    snakeState.changeToLeftMove();
                    break;
                case (int)GlobalSettings.Direction.Up:
                    snakeState.changeToMoveUp();
                    break;
                case (int)GlobalSettings.Direction.Right:
                    snakeState.changeToRightMove();
                    break;
                case (int)GlobalSettings.Direction.Down:
                    snakeState.changeToMoveDown();
                    break;
            }
            return Direction; 
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            timeSinceDirectionChange += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;

                if (snakeState.state != snakeStateMachine.snakeHealth.Not)// call update on the EnemySprite when not in 'NOT'
                {
                    snakeState.MachineEnemySprite.Update();
                }
            }

            //Boundary collisions

            if (snakeState.state == snakeStateMachine.snakeHealth.MoveUp)
            {
                // Move up
                if (position.Y > GlobalSettings.BORDER_OFFSET)
                {
                    position.Y--;
                }

                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, false, false, true });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (snakeState.state == snakeStateMachine.snakeHealth.MoveDown)
            {
                //Move down
                if (position.Y < bottomBound)
                {
                    position.Y++;
                }

                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, false, true, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (snakeState.state == snakeStateMachine.snakeHealth.MoveLeft)
            {
                //Move left
                if (position.X > GlobalSettings.BORDER_OFFSET)
                {
                    position.X--;
                }
                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { true, false, false, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (snakeState.state == snakeStateMachine.snakeHealth.MoveRight)
            {
                //Move right
                if (position.X < rightBound)
                {
                    position.X++;
                }
                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, true, false, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }

            if (timeSinceDirectionChange > 8000 && snakeState.state != snakeStateMachine.snakeHealth.Not && snakeState.state != snakeStateMachine.snakeHealth.Die)
            {
                timeSinceDirectionChange = 0;
                randomDirection = random.Next(0, 3);
                switch (randomDirection)
                {
                    case 0:
                        snakeState.changeToLeftMove();
                        break;
                    case 1:
                        snakeState.changeToMoveUp();
                        break;
                    case 2:
                        snakeState.changeToRightMove();
                        break;
                    case 3:
                        snakeState.changeToMoveDown();
                        break;
                }
            }

            hitbox.Location = new Point((int)position.X, (int)position.Y);
            enemyBlockCollision.HandleCollision(this, enemyCollisionDetector.CheckBlockCollisions(hitbox));
            if (enemyCollisionDetector.CheckItemCollision(hitbox) != GlobalSettings.CollisionType.None)
            {
                snakeState.changeToDie();
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            //Remove bug from screen 3 seconds after death
            if (snakeState.state == snakeStateMachine.snakeHealth.Die)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;
                //wait 3 seconds
                if (deathTimer > 1500)
                {
                    SoundFactory.Instance.SnakeDies();
                    deathTimer = 0;
                    // randomly drop rupys
                    if (random.Next(0, 3) == 0) {
                        game.itemList.Add(new RupyItem(position, spriteBatch, game)); 
                    }  
                    snakeState.changeToNot();
                    game.levelCycleRecord.RemoveOneIndex(GlobalSettings.SNAKE_ENEMY);
                }
            }

            if(snakeState.state == snakeStateMachine.snakeHealth.Not)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            }

        }


        public void Draw()
        {
            if (snakeState.state == snakeStateMachine.snakeHealth.Die)
            {
                //tintColor = Color.Red;
                tintColor = Color.White;
                snakeState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
            }

            else if ((snakeState.state != snakeStateMachine.snakeHealth.Not) && (snakeState.state != snakeStateMachine.snakeHealth.Die))
            {
                if (damageTaken == 1)
                {
                    tintColor = Color.OrangeRed;
                    snakeState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else
                {
                    tintColor = Color.White;
                    snakeState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
            }

        }

       
        public void damage()
        {
            damageTaken++;

            if(damageTaken == 2)
            {
                damageTaken = 0;
                snakeState.changeToDie();
            }

        }
        //Functions to switch the states
        public void changeToIdle()
        {
            snakeState.changeToIdle();
        }

        public void changeToMoveRight()
        {
            snakeState.changeToRightMove();
        }

        public void changeToMoveLeft()
        {
            snakeState.changeToLeftMove();
        }

        public void changeToMoveUp()
        {
            snakeState.changeToMoveUp();
        }

        public void changeToMoveDown()
        {
            snakeState.changeToMoveDown();
        }

        public void changeToDie()
        {
            snakeState.changeToDie();
        }

        public void changeToNot()
        {
            snakeState.changeToNot();
        }

        public GlobalSettings.Direction GetDirection()
        {
            return GlobalSettings.Direction.Right;
        }
    }

    
    public class snakeStateMachine
    {
        public enum snakeHealth { Idle,MoveUp, MoveDown, MoveLeft, MoveRight, Die, Not }; // the different possible states
        public snakeHealth state; // the current health state of the bug
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public snakeStateMachine()
        {
            state = snakeHealth.MoveUp;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
        }
        public void changeToIdle()
        {
            //change to idle if not already Idle
            if (state != snakeHealth.Idle)
            {
                state = snakeHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //get appropriate sprite from sprite factory
            }

        }

        public void changeToRightMove()
        {
            //change to Attack if not already in Attack
            if (state != snakeHealth.MoveRight)
            {
                state = snakeHealth.MoveRight;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeRightMoving();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToLeftMove()
        {
            //change to Move if not already Move
            if (state != snakeHealth.MoveLeft)
            {
                state = snakeHealth.MoveLeft;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeLeftMoving();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveUp()
        {
            //change to Move if not already Move
            if (state != snakeHealth.MoveUp)
            {
                state = snakeHealth.MoveUp;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveDown()
        {
            //change to Move if not already Move
            if (state != snakeHealth.MoveDown)
            {
                state = snakeHealth.MoveDown;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToDie()
        {
            //change to Die if not already Die
            if (state != snakeHealth.Die)
            {
                state = snakeHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeDie();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToNot()
        {
            //change to Not if not already Not
            if (state != snakeHealth.Not)
            {
                state = snakeHealth.Not;
            }
        }

    }
}
