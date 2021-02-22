using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class SnakeEnemy : IEnemy
    {
        private snakeStateMachine snakeState; // to keep track of the current state of the snake
        private Vector2 position; // the position of the enemy on screen
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch

        private int timeSinceLastFrame=0; // used to slow down the rate of animation 
        private int milliSecondsPerFrame=80;
        private int temp = 0; //counter to change states after a certain number of calls to update


        //make the constructor for the class
        public SnakeEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch SB)
        {
            position = startPosition;
            snakeState = new snakeStateMachine();
            Graphics = graphics;
            spriteBatch = SB;


        }

        public void LoadContent()
        {
           // spriteBatch = new SpriteBatch(Graphics);
            
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;

                if (snakeState.state != snakeStateMachine.snakeHealth.Not)// call update on the EnemySprite when not in 'NOT'
                {
                    snakeState.MachineEnemySprite.Update();
                }
            }

            //temporary code for sprint2 to shuffle in between states
            if (temp == 0 && snakeState.state != snakeStateMachine.snakeHealth.Not) {
                snakeState.changeToIdle();
            }

            if (temp == 200 && snakeState.state != snakeStateMachine.snakeHealth.Not) {
                snakeState.changeToAttack();
            }

            if (temp == 400 && snakeState.state != snakeStateMachine.snakeHealth.Not) {
                snakeState.changeToMove();
            }

            if (temp == 600 && snakeState.state != snakeStateMachine.snakeHealth.Not) {
                snakeState.changeToDie();
            }

            if (temp == 800 && snakeState.state != snakeStateMachine.snakeHealth.Not) {
                snakeState.changeToIdle();
            }

            if (temp > 800)
            {
                temp = 0;
            }
            temp++;
            //end of temporary code for sprint2 to shuffle in between states

            //updating position of enemy according to state
            if (snakeState.state == snakeStateMachine.snakeHealth.Move) {
                position.X--;
            }

            if (snakeState.state != snakeStateMachine.snakeHealth.Move) {
                position.X = 300;
            }

            if (temp > 800) {
                temp = 0;
            }
            temp++;
        }


        public void Draw()
        {
            if(snakeState.state != snakeStateMachine.snakeHealth.Not)
            {
                snakeState.MachineEnemySprite.Draw(spriteBatch, position, Color.White);
            }

        }

        //Functions to switch the states
        public void changeToIdle()
        {
            snakeState.changeToIdle();
        }

        public void changeToAttack()
        {
            snakeState.changeToAttack();
        }

        public void changeToMove()
        {
            snakeState.changeToMove();
        }

        public void changeToDie()
        {
            snakeState.changeToDie();
        }

        public void changeToNot()
        {
            snakeState.changeToNot();
        }
    }

    
    public class snakeStateMachine
    {
        public enum snakeHealth { Idle, Attack, Move, Die, Not }; // the different possible states
        public snakeHealth state; // the current health state of the bug
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public snakeStateMachine()
        {
            state = snakeHealth.Idle;
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

        public void changeToAttack()
        {
            //change to Attack if not already in Attack
            if (state != snakeHealth.Attack)
            {
                state = snakeHealth.Attack;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeAttack();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMove()
        {
            //change to Move if not already Move
            if (state != snakeHealth.Move)
            {
                state = snakeHealth.Move;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeMoving();
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
