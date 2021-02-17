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
        private EnemySprite emenySprite;
        private Vector2 position;

        //make the constructor for the class
        public SnakeEnemy(Vector2 startPosition)
        {
            position = startPosition;
            snakeState = new snakeStateMachine();

        }
        //loadcontent method where we load the texture in? Sprite factory?

        void update()
        {
            emenySprite.Update();
        }

        //calling draw should pass in the stae, so the sprite factory can choose and return appropriate sprite rows and columns?

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
    }

    //2nd row - Idle
    //3rd row - Moving
    //4th row - Attack
    //5th row - Die
    public class snakeStateMachine
    {
        private enum snakeState { Idle, Attack, Move, Die };
        private snakeState state = snakeState.Idle;
        public void changeToIdle()
        {
            //change to idle
            if (state != snakeState.Idle)
            {
                state = snakeState.Idle;
            }

        }

        public void changeToAttack()
        {
            if (state != snakeState.Attack)
            {
                state = snakeState.Attack;
            }
        }

        public void changeToMove()
        {
            if (state != snakeState.Move)
            {
                state = snakeState.Move;
            }
        }

        public void changeToDie()
        {
            if (state != snakeState.Die)
            {
                state = snakeState.Die;
            }
        }

    }
}