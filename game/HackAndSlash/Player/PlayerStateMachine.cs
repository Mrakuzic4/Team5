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
		private IPlayer Player;

		//TODO: implement player health.
		//private PlayerHealth health = GoombaHealth.Normal;

		/// <summary>
		/// dir is the direction in integer form, 0 is left, 1 is right, etc.
		/// </summary>
		/// <param name="dir"></param>
		public PlayerStateMachine(GlobalSettings.Direction dir, Game1 game, IPlayer Player)
		{
			this.game = game;
			playerDir = dir;
			this.Player = Player;
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
					//Change the Pos
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X - GlobalSettings.STEP_SIZE_X, Player.GetPos().Y));
					break;
					
				case GlobalSettings.Direction.Up:
					SpriteFactory.Instance.SetUpPlayer();
					//move the sprite
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X, Player.GetPos().Y - GlobalSettings.STEP_SIZE_Y));
					break;

				case GlobalSettings.Direction.Down:
					SpriteFactory.Instance.SetDownPlayer();
					//move the sprite
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X, Player.GetPos().Y + GlobalSettings.STEP_SIZE_Y));
					break;

				default: //Default to be right
					SpriteFactory.Instance.SetRightPlayer();
					//move the sprite
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X + GlobalSettings.STEP_SIZE_X, Player.GetPos().Y));
					break;
			}
		}

		public void Damaged()
		{
			//TODO: health goes down...

			game.Player = new DamagedPlayer(Player, game); //Decorator of the PlayerSprite

			//Player falls back when hit by enemies.
			switch (playerDir)
			{
				case GlobalSettings.Direction.Left:
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X + GlobalSettings.KNOCKBACK_DIST_X, Player.GetPos().Y));
					break;

				case GlobalSettings.Direction.Up:
					//move the sprite
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X , Player.GetPos().Y + GlobalSettings.KNOCKBACK_DIST_X));

					break;

				case GlobalSettings.Direction.Down:
					//move the sprite
					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X, Player.GetPos().Y - GlobalSettings.KNOCKBACK_DIST_X));
					break;

				default: //Default to be right
						 //move the sprite

					Player.SetPos(new Microsoft.Xna.Framework.Vector2(Player.GetPos().X - GlobalSettings.KNOCKBACK_DIST_X, Player.GetPos().Y));
					break;
			}

		}

		public void Attack()
        {
			game.Player = new AttackPlayer(Player, game); //Decorator of the PlayerSprite
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
			game.Player = new UseItemPlayer(Player, game); //Decorator of the PlayerSprite
			switch (playerDir)
			{
				case GlobalSettings.Direction.Left:
					SpriteFactory.Instance.SetLeftUseItemPlayer();
					break;

				case GlobalSettings.Direction.Up:
					SpriteFactory.Instance.SetUpUseItemPlayer();
					break;

				case GlobalSettings.Direction.Down:
					SpriteFactory.Instance.SetDownUseItemPlayer();
					break;

				default: //Default to be right
					SpriteFactory.Instance.SetRightUseItemPlayer();
					break;
			}
		}

	}
}
