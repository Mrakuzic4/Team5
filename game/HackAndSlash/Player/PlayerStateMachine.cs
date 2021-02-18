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


		//TODO: implement player health.
		//private PlayerHealth health = GoombaHealth.Normal;

		/// <summary>
		/// dir is the direction in integer form, 0 is left, 1 is right, etc.
		/// </summary>
		/// <param name="dir"></param>
		public PlayerStateMachine(int dir)
		{
			playerDir = (direction) dir;
		}

		public void ChangeDirection(int dir)
        {
			playerDir = (direction)dir;
		}

		public ISprite Move()
        {
			if (playerDir == direction.Left) return SpriteFactory.Instance.CreateLeftPlayer();
			if (playerDir == direction.Right) return SpriteFactory.Instance.CreateRightPlayer();
			if (playerDir == direction.Down) return SpriteFactory.Instance.CreateDownPlayer();
			if (playerDir == direction.Up) return SpriteFactory.Instance.CreateUpPlayer();
			//default to be facing right
			return SpriteFactory.Instance.CreateRightPlayer();
		}

		public ISprite Damaged()
		{
			if (playerDir == direction.Left) return SpriteFactory.Instance.CreateLeftPlayer();
			if (playerDir == direction.Right) return SpriteFactory.Instance.CreateRightPlayer();
			if (playerDir == direction.Down) return SpriteFactory.Instance.CreateDownPlayer();
			if (playerDir == direction.Up) return SpriteFactory.Instance.CreateUpPlayer();
			//default to be facing right
			return SpriteFactory.Instance.CreateRightPlayer();
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
