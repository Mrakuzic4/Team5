

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class Player : IPlayer
    {
        private PlayerStateMachine playerStateMachine;
        private int timer;
        private Game1 game;

        //Collision
        private Rectangle playerHitBox;
        private Rectangle playerSwordHitBox;

        private PlayerCollisionDetector playerCollisionDetector;


        private PlayerBlockCollisionHandler playerBlockCollisionHandler;
        private PlayerEnemyCollisionHandler playerEnemyCollisionHandler;
        private ItemEnemyCollisionHandler itemEnemyCollisionHandler;


        // Character positions 
        private Vector2 relPositionMC; // Relative position. As position in display window 

        public Player(Game1 game)
        {
            playerStateMachine = new PlayerStateMachine(GlobalSettings.Direction.Right, game, this); //inital state face right
            SpriteFactory.Instance.SetRightPlayer();//Set up the inital sprite
            timer = 7; //adding delay to the player sprite animation
           
            
            this.game = game;

            //Inital Position
            relPositionMC.X = GlobalSettings.BORDER_OFFSET;
            relPositionMC.Y = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET;

            //Collision
            playerHitBox = new Rectangle((int)relPositionMC.X, (int)relPositionMC.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            playerCollisionDetector = new PlayerCollisionDetector(this, game);
            playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            itemEnemyCollisionHandler = new ItemEnemyCollisionHandler();
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
            //Sprite Animation and Decorator
            playerStateMachine.Attack();

            //Player sword collides with enemies
            switch (GetDir()){
                case GlobalSettings.Direction.Right:
                    playerSwordHitBox = new Rectangle((int)relPositionMC.X+GlobalSettings.BASE_SCALAR,(int) relPositionMC.Y+GlobalSettings.BASE_SCALAR/2,50,10); //width is 50, height is 10 for now
                    break;
                case GlobalSettings.Direction.Left:
                    break;
            }

            itemEnemyCollisionHandler.HandleCollision(game.bugfirst, game.Player, playerCollisionDetector.CheckEnemyCollisions(playerHitBox));
        }

        public void Damaged()
        {
            playerStateMachine.Damaged();
        }

        public void Update()
        {
            DrawPlayer.Instance.Update();

            //Player Boundary Check
            stayInBoundary();

            //Player Collision Detector

            //hitbox for player, wraps around player.
            playerHitBox.Location = new Point((int)relPositionMC.X, (int)relPositionMC.Y);
            //Player Block Collision
            playerBlockCollisionHandler.HandleCollision(game.Player, playerCollisionDetector.CheckBlockCollisions(playerHitBox));
            //Player Enemy Collision
            playerEnemyCollisionHandler.HandleCollision(game.Player, playerCollisionDetector.CheckEnemyCollisions(playerHitBox));

            
            //TODO: More Collision...
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            DrawPlayer.Instance.Draw(spriteBatch, location, color);
        }

        public void UseItem()
        {
            //Player Animation
            playerStateMachine.UseItem();
            //Item animation
            game.ItemHolder.UseItem(GetDir());

            //Item collides with Enemy

        }

        private void stayInBoundary()
        {
            //Top and Left Boundary work just fine
            if (this.relPositionMC.X < GlobalSettings.BORDER_OFFSET) this.relPositionMC.X = GlobalSettings.BORDER_OFFSET;
            if (this.relPositionMC.Y < GlobalSettings.BORDER_OFFSET) this.relPositionMC.Y = GlobalSettings.BORDER_OFFSET;
            //Bottom and Right Boundary need to take the window and sprite size into account.
            int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
            int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;

            if (this.relPositionMC.Y > bottomBound) this.relPositionMC.Y = bottomBound;
            if (this.relPositionMC.X > rightBound) this.relPositionMC.X = rightBound;
        }

       
    }
    
}
