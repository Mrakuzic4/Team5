using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
	public class PlayerStateMachine
	{
		private enum direction { Left, Right, Up, Down }; //four integer values for direction.
		private direction playerDir;
		public int Direction { get { return (int) playerDir; } }

		private Game1 game;

		//TODO: implement player health.
		//private PlayerHealth health = GoombaHealth.Normal;

		/// <summary>
		/// dir is the direction in integer form, 0 is left, 1 is right, etc.
		/// </summary>
		/// <param name="dir"></param>
		public PlayerStateMachine(int dir, Game1 game)
		{
			this.game = game;
			playerDir = (direction) dir;
		}

		public void ChangeDirection(int dir)
        {
			playerDir = (direction)dir;
		}

		public void Move()
        {
            switch (playerDir)
            {
				case direction.Left:
					SpriteFactory.Instance.CreateLeftPlayer();
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X - GlobalSettings.STEP_SIZE_X, game.Pos.Y); //move the sprite
					break;
					
				case direction.Up:
					SpriteFactory.Instance.CreateUpPlayer();
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y - GlobalSettings.STEP_SIZE_Y);
					break;

				case direction.Down:
					SpriteFactory.Instance.CreateDownPlayer();
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y + GlobalSettings.STEP_SIZE_Y);
					break;

				default: //Default to be right
					SpriteFactory.Instance.CreateRightPlayer();
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X + GlobalSettings.STEP_SIZE_X, game.Pos.Y);
					break;
			}
		}

		public void Damaged()
		{
			//TODO: health goes down...

			//Player falls back when hit by enemies.
			switch (playerDir)
			{
				case direction.Left:
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X + GlobalSettings.KNOCKBACK_DIST_X, game.Pos.Y); //move the sprite
					break;

				case direction.Up:
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y + GlobalSettings.KNOCKBACK_DIST_Y);
					break;

				case direction.Down:
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y - GlobalSettings.KNOCKBACK_DIST_Y);
					break;

				default: //Default to be right
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X - GlobalSettings.KNOCKBACK_DIST_X, game.Pos.Y);
					break;
			}

		}

		public void Attack()
        {
			switch (playerDir)
			{
				case direction.Left:
					SpriteFactory.Instance.CreateLeftAttackPlayer();
					break;

				case direction.Up:
					SpriteFactory.Instance.CreateUpAttackPlayer();
					break;

				case direction.Down:
					SpriteFactory.Instance.CreateDownAttackPlayer();
					break;

				default: //Default to be right
					SpriteFactory.Instance.CreateRightAttackPlayer();
					break;
			}
		}
		public void UseItem()
		{
			switch (playerDir)
			{
				case direction.Left:
					SpriteFactory.Instance.CreateLeftPlayer();
					break;

				case direction.Up:
					SpriteFactory.Instance.CreateUpPlayer();
					break;

				case direction.Down:
					SpriteFactory.Instance.CreateDownPlayer();
					break;

				default: //Default to be right
					SpriteFactory.Instance.CreateRightPlayer();
					break;
			}
		}

	}
}
