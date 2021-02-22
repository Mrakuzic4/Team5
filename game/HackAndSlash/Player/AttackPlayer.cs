

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{

    public class AttackPlayer : IPlayer
    {
        private IPlayer DecoratedPlayer;
        private Game1 game;
        private int timer;

        public AttackPlayer(IPlayer decoratedPlayer, Game1 game)
        {
            timer = 30; //Added delay.
            this.DecoratedPlayer = decoratedPlayer;
            this.game = game;
            DrawPlayer.Instance.Frame = 0;
        }

        public void Update()
        {
            timer--;
            if (timer % 10 == 0) DrawPlayer.Instance.Frame += 1;
            if (timer == 0)
            {
                DrawPlayer.Instance.Frame = 2;
                RemoveDecorator();
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            DecoratedPlayer.Draw(spriteBatch, location, color);
        }

        public void RemoveDecorator()
        {
            game.Player = DecoratedPlayer; //set it back to movement state.
        }

        public void Move()
        {
            DecoratedPlayer.Move();
        }

        public void Attack()
        {
            //Does not attack
        }

        public void Damaged()
        {
            DecoratedPlayer.Damaged();
        }

        public int GetDir()
        {
            return DecoratedPlayer.GetDir();
        }

        public void ChangeDirection(int dir)
        {
            DecoratedPlayer.ChangeDirection(dir);
        }
    }

}
