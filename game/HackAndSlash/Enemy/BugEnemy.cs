using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

        //Bug position
        public Rectangle rectangle { get; set;  }

        //make the constructor for the class
        public BugEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            position = startPosition;
            bugState = new bugStateMachine();
            Graphics = graphics;
            this.spriteBatch = spriteBatch;
            rectangle = new Rectangle((int)position.X + 10, (int)position.Y + 10, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);


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

            //temporary code for sprint2 to shuffle in between states
            if (temp == 0 && bugState.state !=  bugStateMachine.bugHealth.Not) {
                bugState.changeToIdle();
            }

            if (temp == 200 && bugState.state !=  bugStateMachine.bugHealth.Not) {
                bugState.changeToMoveUp();
            }

            if (temp == 400 && bugState.state !=  bugStateMachine.bugHealth.Not) {
                bugState.changeToMoveDown();
            }

            if (temp == 600 && bugState.state !=  bugStateMachine.bugHealth.Not) {
                bugState.changeToDie();
            }

            if (temp == 800 && bugState.state !=  bugStateMachine.bugHealth.Not) {
                bugState.changeToIdle();
            }

            if (temp > 800)
            {
                temp = 0;
            }
            temp++;
            //end of temporary code for sprint2 to shuffle in between states

            //updating position of enemy according to state
            /*if (bugState.state == bugStateMachine.bugHealth.MoveUp) {
                position.Y--;
            }

            if (bugState.state == bugStateMachine.bugHealth.MoveDown) {
                position.Y++;
            }

            if ((bugState.state != bugStateMachine.bugHealth.MoveUp) && (bugState.state != bugStateMachine.bugHealth.MoveDown)) {
                position.Y = 100;
            }

            */
            rectangle = new Rectangle((int)position.X + 10, (int)position.Y + 10, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

        }

        public void Draw()
        {
            if (bugState.state != bugStateMachine.bugHealth.Not)
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
        public enum bugHealth { Idle, MoveUp,MoveDown, Die, Not }; // the different possible states
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
