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
        private snakeStateMachine snakeState;
        private Texture2D texture;
        public EnemySprite EnemySprite;
        private Vector2 position;
        private SpriteBatch spriteBatch;
        private GraphicsDevice Graphics;

        private int timeSinceLastFrame=0;
        private int milliSecondsPerFrame=80;


        //make the constructor for the class
        public SnakeEnemy(Vector2 startPosition, GraphicsDevice graphics)
        {
            position = startPosition;
            snakeState = new snakeStateMachine();
            Graphics = graphics;
            EnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
            
        }

        public void LoadContent()
        {
           spriteBatch = new SpriteBatch(Graphics);
            
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > milliSecondsPerFrame)
            {
                timeSinceLastFrame = 0;

                if (snakeState.state != snakeStateMachine.snakeHealth.Not)
                {
                    snakeState.MachineEnemySprite.Update();
                }
            }
        }

        //use the state machine in draw to choose the texture. set enemysprite to selected texture, then call draw 

        public void Draw()
        {
            if(snakeState.state == snakeStateMachine.snakeHealth.Idle)
            {
                //get the appropriate snake sprite from factory and set it to EnemySprite
                //EnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //call EnemySprite.draw
                snakeState.MachineEnemySprite.Draw(spriteBatch,position);
                
            }

            else if(snakeState.state == snakeStateMachine.snakeHealth.Move)
            {
                //get the appropriate snake sprite from factory and set it to EnemySprite
                //EnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeMoving();
                //call EnemySprite.draw
                snakeState.MachineEnemySprite.Draw(spriteBatch, position);
            }

            else if(snakeState.state == snakeStateMachine.snakeHealth.Attack)
            {
                //get the appropriate snake sprite from factory and set it to EnemySprite
                //EnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeAttack();
                //call EnemySprite.draw
                snakeState.MachineEnemySprite.Draw(spriteBatch, position);
            }

            else if(snakeState.state == snakeStateMachine.snakeHealth.Die)
            {
                //get the appropriate snake sprite from factory and set it to EnemySprite
                //EnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeDie();
                //call EnemySprite.draw
                snakeState.MachineEnemySprite.Draw(spriteBatch, position);
            }

            else if(snakeState.state == snakeStateMachine.snakeHealth.Not)
            {
                //do not call draw
            }
        }

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

    //2nd row - Idle
    //3rd row - Moving
    //4th row - Attack
    //5th row - Die
    public class snakeStateMachine
    {
        public enum snakeHealth { Idle, Attack, Move, Die, Not };
        public snakeHealth state;
        public EnemySprite MachineEnemySprite;
        public snakeStateMachine()
        {
            state = snakeHealth.Idle;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
        }
        public void changeToIdle()
        {
            //change to idle
            if (state != snakeHealth.Idle)
            {
                state = snakeHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
            }

        }

        public void changeToAttack()
        {
            if (state != snakeHealth.Attack)
            {
                state = snakeHealth.Attack;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeAttack();
            }
        }

        public void changeToMove()
        {
            if (state != snakeHealth.Move)
            {
                state = snakeHealth.Move;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeMoving();
            }
        }

        public void changeToDie()
        {
            if (state != snakeHealth.Die)
            {
                state = snakeHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeDie();
            }
        }

        public void changeToNot()
        {
            if (state != snakeHealth.Not)
            {
                state = snakeHealth.Not;
            }
        }

    }
}
