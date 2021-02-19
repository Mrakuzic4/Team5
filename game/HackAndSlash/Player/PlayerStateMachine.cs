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
			playerDir = (direction) dir;
			this.game = game;
		}

		public void ChangeDirection(int dir)
        {
			playerDir = (direction)dir;
		}

		public IPlayer Move()
        {
			if (playerDir == direction.Left) return SpriteFactory.Instance.CreateLeftPlayer();
			if (playerDir == direction.Right) return SpriteFactory.Instance.CreateRightPlayer();
			if (playerDir == direction.Down) return SpriteFactory.Instance.CreateDownPlayer();
			if (playerDir == direction.Up) return SpriteFactory.Instance.CreateUpPlayer();
			//default to be facing right
			return SpriteFactory.Instance.CreateRightPlayer();
		}

		public IPlayer Damaged()
		{
			if (playerDir == direction.Left) return new DamagedPlayer(SpriteFactory.Instance.CreateLeftPlayer(),game);
			if (playerDir == direction.Right) return new DamagedPlayer(SpriteFactory.Instance.CreateRightPlayer(), game);
			if (playerDir == direction.Down) return new DamagedPlayer(SpriteFactory.Instance.CreateDownPlayer(), game);
			if (playerDir == direction.Up) return new DamagedPlayer(SpriteFactory.Instance.CreateUpPlayer(), game);
			//default to be facing right
			return new DamagedPlayer(SpriteFactory.Instance.CreateRightPlayer(), game);
		}

		public void Attack()
        {

        }

		public void Update()
		{
			//draw here???


			// if-else logic based on the current values of facingLeft and health to determine how to move
		}
	}
}
