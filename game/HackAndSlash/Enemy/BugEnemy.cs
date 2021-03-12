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
    public class BugEnemy : IEnemy
    {
        private bugStateMachine bugState; // to keep track of the current state of the bug 
        private Vector2 position; // the position of the enemy on screen
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch

        private int timeSinceDirectionChange = 0;
        private int deathTimer = 0;
        private int timeSinceLastFrame = 0; // used to slow down the rate of animation 
        private int milliSecondsPerFrame = 80;
        private int temp = 0;//counter to change states after a certain number of calls to update

        private System.Random random;
        private int randomDirection = 0; //0-left, 1-up, 2-right, 3- down

        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        private Rectangle hitbox;

        private int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        private int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        //Bug position
        public Rectangle rectangle { get; set;  }

        //make the constructor for the class
        public BugEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch spriteBatch, Game1 game)
        {
            position = startPosition;
            bugState = new bugStateMachine();
            Graphics = graphics;
            this.spriteBatch = spriteBatch;
            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);


            random = new System.Random();

            enemyCollisionDetector = new EnemyCollisionDetector(game, this);
            enemyBlockCollision = new EnemyBlockCollision();
            hitbox = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        //Loading the spritebatch 
        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Graphics);
        }

        public Vector2 GetPos()
        {
            return position;
        }

        public void SetPos(Vector2 pos)
        {
            position = pos;
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            timeSinceDirectionChange += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;
                if (bugState.state != bugStateMachine.bugHealth.Not) // call update on the EnemySprite when not in 'NOT'
                {
                    bugState.MachineEnemySprite.Update();
                }
            }

            //Boundary collisions
            if (bugState.state == bugStateMachine.bugHealth.MoveUp)
            {
                // Move up
                if (position.Y >= GlobalSettings.BORDER_OFFSET)
                {
                    position.Y--;
                }

                else
                {
                    bugState.changeToMoveDown();
                }
            }
            else if (bugState.state == bugStateMachine.bugHealth.MoveDown)
            {
                //Move down
                if (position.Y <= bottomBound)
                {
                    position.Y++;
                }

                else
                {
                    bugState.changeToMoveUp();
                }
            }
            else if (bugState.state == bugStateMachine.bugHealth.MoveLeft)
            {
                //Move left
                if (position.X >= GlobalSettings.BORDER_OFFSET)
                {
                    position.X--;
                } else
                {
                    bugState.changeToMoveRight();
                }
            }
            else if (bugState.state == bugStateMachine.bugHealth.MoveRight)
            {
                //Move right
                if (position.X <= rightBound)
                {
                    position.X++;
                }
                else
                {
                    bugState.changeToMoveLeft();
                }
            }

            if (timeSinceDirectionChange > 8000 && bugState.state!= bugStateMachine.bugHealth.Not && bugState.state != bugStateMachine.bugHealth.Die)
            {
                timeSinceDirectionChange = 0;
                randomDirection = random.Next(0, 3);
                switch (randomDirection)
                {
                    case 0:
                        bugState.changeToMoveLeft();
                        break;
                    case 1:
                        bugState.changeToMoveUp();
                        break;
                    case 2:
                        bugState.changeToMoveRight();
                        break;
                    case 3:
                        bugState.changeToMoveDown();
                        break;
                }
            }

            hitbox.Location = new Point((int)position.X , (int)position.Y);
            enemyBlockCollision.HandleCollision(this, enemyCollisionDetector.CheckBlockCollisions(hitbox));
            if (enemyCollisionDetector.CheckItemCollision(hitbox) != GlobalSettings.CollisionType.None)
            {
                bugState.changeToDie();
            }




            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            //Remove bug from screen 3 seconds after death
            if (bugState.state == bugStateMachine.bugHealth.Die)
            {
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;
                //wait 3 seconds
                if (deathTimer > 3000)
                {
                    deathTimer = 0;
                    bugState.changeToNot();
                }
            }
        }

        public void Draw()
        {
            if (bugState.state == bugStateMachine.bugHealth.Die)
            {
                bugState.MachineEnemySprite.Draw(spriteBatch, position, Color.Red);
            }

            else if((bugState.state != bugStateMachine.bugHealth.Not) && (bugState.state != bugStateMachine.bugHealth.Die))
            {
                bugState.MachineEnemySprite.Draw(spriteBatch, position, Color.White);
            }

        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }

        //Functions to switch the states
        public void changeToIdle()
        {
            bugState.changeToIdle();
        }

        public void changeToMoveUp()
        {
            bugState.changeToMoveUp();
        }

        public void changeToMoveDown()
        {
            bugState.changeToMoveDown();
        }

        public void changeToMoveLeft()
        {
            bugState.changeToMoveDown();
        }

        public void changeToMoveRight()
        {
            bugState.changeToMoveDown();
        }

        public void changeToDie()
        {
            bugState.changeToDie();
        }

        public void changeToNot()
        {
            bugState.changeToNot();
        }
    }

    //The state machine holding the bug health
    public class bugStateMachine
    {
        public enum bugHealth { Idle, MoveUp,MoveDown, MoveLeft, MoveRight, Die, Not }; // the different possible states
        public bugHealth state;// the current health state of the bug
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public bugStateMachine()
        {
            state = bugHealth.MoveLeft;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveLeft();
        }
        public void changeToIdle()
        {
            //change to idle if not already Idle
            if (state != bugHealth.Idle)
            {
                state = bugHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugIdle();
                //get appropriate sprite from sprite factory
            }

        }

        public void changeToMoveUp()
        {
            //change to moveUp if not already moveUp
            if (state != bugHealth.MoveUp)
            {
                state = bugHealth.MoveUp;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveUp();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveDown()
        {
            //change to MoveDown if not already MoveDown
            if (state != bugHealth.MoveDown)
            {
                state = bugHealth.MoveDown;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveDown();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveLeft()
        {
            if (state != bugHealth.MoveLeft)
            {
                state = bugHealth.MoveLeft;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveLeft();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveRight()
        {
            if (state != bugHealth.MoveRight)
            {
                state = bugHealth.MoveRight;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveRight();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToDie()
        {
            if (state != bugHealth.Die)
            {
                state = bugHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugDie();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToNot()
        {
            //change to Not if not already Not
            if (state != bugHealth.Not)
            {
                state = bugHealth.Not;
            }
        }

    }
}
