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
        private bugStateMachine bugState;
        private Texture2D texture;
        private EnemySprite EnemySprite;
        private Vector2 position;
        private SpriteBatch spriteBatch;
        private GraphicsDevice Graphics;

        private int timeSinceLastFrame = 0;
        private int milliSecondsPerFrame = 80;

        //make the constructor for the class
        public BugEnemy(Vector2 startPosition, GraphicsDevice graphics)
        {
            position = startPosition;
            bugState = new bugStateMachine();
            Graphics = graphics;
            EnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugIdle();
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
                if (bugState.state != bugStateMachine.bugHealth.Not)
                {
                    bugState.MachineEnemySprite.Update();
                }
            }
        }

        public void Draw()
        {
            if (bugState.state == bugStateMachine.bugHealth.Idle)
            {
                //get the appropriate bug sprite from factory and set it to EnemySprite
                //call EnemySprite.draw
                bugState.MachineEnemySprite.Draw(spriteBatch, position);

            }

            else if (bugState.state == bugStateMachine.bugHealth.MoveUp)
            {
                //get the appropriate bug sprite from factory and set it to EnemySprite
                //call EnemySprite.draw
                bugState.MachineEnemySprite.Draw(spriteBatch, position);
            }

            else if (bugState.state == bugStateMachine.bugHealth.MoveDown)
            {
                //get the appropriate bug sprite from factory and set it to EnemySprite
                //call EnemySprite.draw
                bugState.MachineEnemySprite.Draw(spriteBatch, position);
            }

            else if (bugState.state == bugStateMachine.bugHealth.Die)
            {
                //get the appropriate bug sprite from factory and set it to EnemySprite
                //call EnemySprite.draw
                bugState.MachineEnemySprite.Draw(spriteBatch, position);
            }

            else if (bugState.state == bugStateMachine.bugHealth.Not)
            {
                //do not call draw
            }
        }

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

    public class bugStateMachine
    {
        public enum bugHealth { Idle, MoveUp,MoveDown, Die, Not };
        public bugHealth state;
        public EnemySprite MachineEnemySprite;

        public bugStateMachine()
        {
            state = bugHealth.Not;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugIdle();
        }
        public void changeToIdle()
        {
            //change to idle
            if (state != bugHealth.Idle)
            {
                state = bugHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugIdle();
            }

        }

        public void changeToMoveUp()
        {
            if (state != bugHealth.MoveUp)
            {
                state = bugHealth.MoveUp;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveUp();
            }
        }

        public void changeToMoveDown()
        {
            if (state != bugHealth.MoveDown)
            {
                state = bugHealth.MoveDown;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugMoveDown();
            }
        }

        public void changeToDie()
        {
            if (state != bugHealth.Die)
            {
                state = bugHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateBugDie();
            }
        }

        public void changeToNot()
        {
            if (state != bugHealth.Not)
            {
                state = bugHealth.Not;
            }
        }

    }
}