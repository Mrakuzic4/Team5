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
		private GlobalSettings.Direction playerDir;
		public int Direction { get { return (int) playerDir; } }

		private Game1 game;

		//TODO: implement player health.
		//private PlayerHealth health = GoombaHealth.Normal;

		/// <summary>
		/// dir is the direction in integer form, 0 is left, 1 is right, etc.
		/// </summary>
		/// <param name="dir"></param>
		public PlayerStateMachine(GlobalSettings.Direction dir, Game1 game)
		{
			this.game = game;
			playerDir = dir;
		}

		public void ChangeDirection(GlobalSettings.Direction dir)
        {
			playerDir = dir;
		}

		public void Move()
        {
            switch (playerDir)
            {
				case GlobalSettings.Direction.Left:
					SpriteFactory.Instance.SetLeftPlayer();
					//CHange the POs
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X - GlobalSettings.STEP_SIZE_X, game.Pos.Y); //move the sprite
					break;
					
				case GlobalSettings.Direction.Up:
					SpriteFactory.Instance.SetUpPlayer();
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y - GlobalSettings.STEP_SIZE_Y);
					break;

				case GlobalSettings.Direction.Down:
					SpriteFactory.Instance.SetDownPlayer();
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y + GlobalSettings.STEP_SIZE_Y);
					break;

				default: //Default to be right
					SpriteFactory.Instance.SetRightPlayer();
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
				case GlobalSettings.Direction.Left:
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X + GlobalSettings.KNOCKBACK_DIST_X, game.Pos.Y); //move the sprite
					break;

				case GlobalSettings.Direction.Up:
					//move the sprite
					game.Pos = new Microsoft.Xna.Framework.Vector2(game.Pos.X, game.Pos.Y + GlobalSettings.KNOCKBACK_DIST_Y);
					break;

				case GlobalSettings.Direction.Down:
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
			DrawPlayer.Instance.Attack = true;
			switch (playerDir)
			{
				case GlobalSettings.Direction.Left:
					SpriteFactory.Instance.SetLeftAttackPlayer();
					DrawPlayer.Instance.SetDirection(GlobalSettings.Direction.Left);
					break;

				case GlobalSettings.Direction.Up:
					SpriteFactory.Instance.SetUpAttackPlayer();
					DrawPlayer.Instance.SetDirection(GlobalSettings.Direction.Up);
					break;

				case GlobalSettings.Direction.Down:
					SpriteFactory.Instance.SetDownAttackPlayer();
					DrawPlayer.Instance.SetDirection(GlobalSettings.Direction.Down);
					break;

				default: //Default to be right
					SpriteFactory.Instance.SetRightAttackPlayer();
					DrawPlayer.Instance.SetDirection(GlobalSettings.Direction.Right);
					break;
			}
		}
		public void UseItem()
		{
			switch (playerDir)
			{
				case GlobalSettings.Direction.Left:
					SpriteFactory.Instance.SetLeftPlayer();
					break;

				case GlobalSettings.Direction.Up:
					SpriteFactory.Instance.SetUpPlayer();
					break;

				case GlobalSettings.Direction.Down:
					SpriteFactory.Instance.SetDownPlayer();
					break;

				default: //Default to be right
					SpriteFactory.Instance.SetRightPlayer();
					break;
			}
		}

	}
}
