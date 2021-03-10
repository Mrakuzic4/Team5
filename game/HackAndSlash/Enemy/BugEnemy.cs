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

        private int timeSinceLastFrame = 0; // used to slow down the rate of animation 
        private int milliSecondsPerFrame = 80;
        private int temp = 0;//counter to change states after a certain number of calls to update

        private Rectangle hitbox;
        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        
        //make the constructor for the class
        public BugEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch spriteBatch, Game1 game)
        {
            position = startPosition;
            bugState = new bugStateMachine();
            Graphics = graphics;
            this.spriteBatch = spriteBatch;
            hitbox = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            enemyCollisionDetector = new EnemyCollisionDetector(game,this);
            enemyBlockCollision = new EnemyBlockCollision();
        }

        public Vector2 GetPos()
        {
            return position;
        }

        public void SetPos(Vector2 pos)
        {
            position = pos;
        }

        //Loading the spritebatch 
        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Graphics);
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;
                if (bugState.state != bugStateMachine.bugHealth.Not) // call update on the EnemySprite when not in 'NOT'
                {
                    bugState.MachineEnemySprite.Update();
                }
            }

            if (bugState.state == bugStateMachine.bugHealth.MoveUp)
            {
                // Move up
            }
            else if (bugState.state == bugStateMachine.bugHealth.MoveDown)
            {
                //Move down
            }
            else if (bugState.state == bugStateMachine.bugHealth.MoveLeft)
            {
                //Move left
            }
            else if (bugState.state == bugStateMachine.bugHealth.MoveRight)
            {
                //Move right
            }
        }

        public void Draw()
        {
            if (bugState.state != bugStateMachine.bugHealth.Not)
            {
              
                bugState.MachineEnemySprite.Draw(spriteBatch, position, Color.White);

            }

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
        public enum bugHealth { Idle, MoveUp, MoveDown, MoveLeft, MoveRight, Die, Not }; // the different possible states
        public bugHealth state;// the current health state of the bug
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public bugStateMachine()
        {
            state = bugHealth.Idle;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugIdle();
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
