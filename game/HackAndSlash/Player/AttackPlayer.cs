

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
        public int GetMaxHealth()
        {
            return DecoratedPlayer.GetMaxHealth();
        }
        public int GetCurrentHealth()
        {
            return DecoratedPlayer.GetCurrentHealth();
        }
        public Vector2 GetPos()
        {
            return this.DecoratedPlayer.GetPos();
        }

        public void SetPos(Vector2 pos)
        {
            this.DecoratedPlayer.SetPos(pos);
        }

        public bool isShield()
        {
            return this.DecoratedPlayer.isShield();
        }

        public void ShieldUp()
        {
            this.DecoratedPlayer.ShieldUp();
        }

        public void HealthUp()
        {
            this.DecoratedPlayer.HealthUp();
        }

        public void Update()
        {
            timer--;
            if (timer % 10 == 0) DrawPlayer.Instance.Frame += 1;
            if (timer == 0)
            {
                DecoratedPlayer.Move();
                RemoveDecorator(); //Set it back to orignal state
            }
        }

        public void unlockMovement()
        {
            this.DecoratedPlayer.unlockMovement();
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
            //Does not move during attack
        }

        public void Attack()
        {
            //Does not attack again
        }

        public void Damaged()
        {
            DecoratedPlayer.Damaged();
        }
        public void Healed()
        {
            DecoratedPlayer.Healed();
        }

        public GlobalSettings.Direction GetDir()
        {
            return DecoratedPlayer.GetDir();
        }

        public void ChangeDirection(GlobalSettings.Direction dir)
        {
            DecoratedPlayer.ChangeDirection(dir);
        }
        public void UseItem()
        {
            //Does not use item when attacking.
        }
    }

}
