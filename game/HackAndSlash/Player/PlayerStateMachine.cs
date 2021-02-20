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

		bool takingDamaged;
		int timer;



		//TODO: implement player health.
		//private PlayerHealth health = GoombaHealth.Normal;

		/// <summary>
		/// dir is the direction in integer form, 0 is left, 1 is right, etc.
		/// </summary>
		/// <param name="dir"></param>
		public PlayerStateMachine(int dir)
		{
			playerDir = (direction) dir;
			timer = 200;
			takingDamaged = false;
		}

		public void ChangeDirection(int dir)
        {
			playerDir = (direction)dir;
		}

		public void Move()
        {
			if (playerDir == direction.Left)  SpriteFactory.Instance.CreateLeftPlayer();
			if (playerDir == direction.Down)  SpriteFactory.Instance.CreateDownPlayer();
			if (playerDir == direction.Up)  SpriteFactory.Instance.CreateUpPlayer();
			//default to be facing right
			if (playerDir == direction.Right) SpriteFactory.Instance.CreateRightAttackPlayer();
		}

		public void Damaged()
		{
			//health goes down...
		}

		public void Attack()
        {
			if (playerDir == direction.Left)  SpriteFactory.Instance.CreateLeftAttackPlayer();
			if (playerDir == direction.Down)  SpriteFactory.Instance.CreateDownAttackPlayer();
			if (playerDir == direction.Up)  SpriteFactory.Instance.CreateUpAttackPlayer();
			//default to be facing right
			if (playerDir == direction.Right) SpriteFactory.Instance.CreateRightAttackPlayer();
		}
	}
}
