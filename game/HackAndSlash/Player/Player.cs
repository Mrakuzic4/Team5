

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class Player : IPlayer
    {
        private PlayerStateMachine playerStateMachine;
        private int timer;
        private Game1 game;

        public Player(Game1 game)
        {
            playerStateMachine = new PlayerStateMachine(GlobalSettings.Direction.Right, game); //inital state face right
            timer = 7; //adding delay to the player sprite animation
            this.game = game;
        }

        public int GetDir()
        {
            return playerStateMachine.Direction;
        }

        public void ChangeDirection(GlobalSettings.Direction dir)
        {
            playerStateMachine.ChangeDirection(dir);
        }

        public void Move()
        {
            timer--;
            if(timer == 0)
            {
                DrawPlayer.Instance.Frame += 1;
                timer = 7;
            }
            playerStateMachine.Move();
        }

        public void Attack()
        {
            playerStateMachine.Attack();
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

        public void UseItem()
        {
            playerStateMachine.UseItem();
        }
    }
    
}
