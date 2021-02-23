

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{

    public class UseItemPlayer : IPlayer
    {
        private IPlayer DecoratedPlayer;
        private Game1 game;
        private int timer;
        private const int TIME = 85; //added delay

        private Vector2 relPositionMC; // Relative position. As position in display window 


        public UseItemPlayer(IPlayer decoratedPlayer, Game1 game)
        {
            timer = TIME; //Added delay.
            this.DecoratedPlayer = decoratedPlayer;
            this.game = game;
            DrawPlayer.Instance.Frame = 0;
            this.relPositionMC = decoratedPlayer.GetPos();
        }
        public Vector2 GetPos()
        {
            return relPositionMC;
        }

        public void SetPos(Vector2 pos)
        {
            relPositionMC = pos;
        }

        public void Update()
        {
            timer--;
            if (timer == 0)
            {
                DecoratedPlayer.Move();
                RemoveDecorator(); //Set it back to orignal state
            }
            DecoratedPlayer.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            DecoratedPlayer.Draw(spriteBatch, location, color);
        }

        public void RemoveDecorator()
        {
            game.Player = DecoratedPlayer; //set it back to movement state.
            DrawPlayer.Instance.Attack = false;
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

        public void ChangeDirection(GlobalSettings.Direction dir)
        {
            DecoratedPlayer.ChangeDirection(dir);
        }
        public void UseItem()
        {
            DecoratedPlayer.UseItem();
        }
    }

}
