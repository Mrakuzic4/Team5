

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class Player : IPlayer
    {
        private PlayerStateMachine playerStateMachine;

        public Player()
        {
            playerStateMachine = new PlayerStateMachine(1); //inital state face right
        }

        public int GetDir()
        {
            return playerStateMachine.Direction;
        }

        public void ChangeDirection(int dir)
        {
            playerStateMachine.ChangeDirection(dir);
        }

        public void Move()
        {
            playerStateMachine.Move();
        }

        public void Attack()
        {
            playerStateMachine.Attack(); //to be fixed...
        }

        public void Damaged()
        {
            playerStateMachine.Damaged();
        }

        public void Update()
        {
            DrawPlayer.Instance.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            DrawPlayer.Instance.Draw(spriteBatch, location, color);
        }
    }
    
}
