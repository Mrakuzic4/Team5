

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class Player : IPlayer
    {
        private PlayerStateMachine playerStateMachine;
        private int timer;
        // private Game1 game;
        private Rectangle hitbox;
        private PlayerCollisionDetector playerCollisionDetector;
        private PlayerBlockCollision playerBlockCollision;

        // Character positions 
        private Vector2 relPositionMC; // Relative position. As position in display window 

        public Player(Game1 game)
        {
            playerStateMachine = new PlayerStateMachine(GlobalSettings.Direction.Right, game, this); //inital state face right
            SpriteFactory.Instance.SetRightPlayer();//Set up the inital sprite
            timer = 7; //adding delay to the player sprite animation
            //this.game = game;
            relPositionMC.X = GlobalSettings.WINDOW_WIDTH / GlobalSettings.MAX_DISPLAY_DIV + 1;
            relPositionMC.Y = GlobalSettings.WINDOW_HEIGHT / GlobalSettings.MAX_DISPLAY_DIV + 1;
            hitbox = new Rectangle((int)relPositionMC.X, (int)relPositionMC.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            playerCollisionDetector = new PlayerCollisionDetector(this, game);
            playerBlockCollision = new PlayerBlockCollision();
        }

        public Vector2 GetPos()
        {
            return relPositionMC;
        }

        public void SetPos(Vector2 pos)
        {
            relPositionMC = pos;
        }

        public GlobalSettings.Direction GetDir()
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
            hitbox.Location = new Point((int)relPositionMC.X, (int)relPositionMC.Y);
            playerBlockCollision.HandleCollision(this, playerCollisionDetector.CheckBlockCollisions(hitbox));
            //Player Collision Detector Here????????????
            if (this.relPositionMC.X < 0) this.relPositionMC.X = 0;
            if (this.relPositionMC.Y < 0) this.relPositionMC.Y = 0;

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
