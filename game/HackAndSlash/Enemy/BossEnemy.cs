using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HackAndSlash.Collision;

namespace HackAndSlash
{
    public class BossEnemy : IEnemy
    {
        private bossStateMachine bossState; // to keep track of the current state of the moblin
        private Vector2 position; // the position of the enemy on screen
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch

        private int timeSinceLastFrame = 0; // used to slow down the rate of animation
        private int timeSinceLastBomb = 0;
        private int timeSinceDirectionChange = 0;
        private int deathTimer = 0;
        private int milliSecondsPerFrame = 80;
        private int temp = 0; //counter to change states after a certain number of calls to update
        private bool firstTimeEncounter = true; 

        private System.Random random;
        private int randomDirection = 0; //0-left, 1-up, 2-right, 3- down

        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        private Rectangle hitbox;

        private IItem[] bombItem;
        public GlobalSettings.Direction direction {get;set;}

        private int damageTaken;
        private Color tintColor;

        private int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        private int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        //Moblin position

        private const int BOSS_HEIGHT = 4 * GlobalSettings.BASE_SCALAR;
        private const int BOSS_WIDTH = 3 * GlobalSettings.BASE_SCALAR;
        private Rectangle rectangle { get; set; }

        private Game1 game; 

        //make the constructor for the class
        public BossEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch SB, Game1 game)
        {

            position = new Vector2(startPosition.X - GlobalSettings.BASE_SCALAR, startPosition.Y - 2 * GlobalSettings.BASE_SCALAR);
            bossState = new bossStateMachine();
            Graphics = graphics;
            spriteBatch = SB;
            this.game = game; 
            rectangle = new Rectangle((int)position.X, (int)position.Y, BOSS_WIDTH, BOSS_HEIGHT);

            random = new System.Random();

            enemyCollisionDetector = new EnemyCollisionDetector(game, this);
            enemyBlockCollision = new EnemyBlockCollision();
            hitbox = new Rectangle((int)position.X, (int)position.Y, BOSS_WIDTH, BOSS_HEIGHT);
            damageTaken = 0;
            bombItem = new IItem[3];
            for (int i = 0; i <= 2; i++)
            {
                bombItem[i] = new BossItem(spriteBatch, game, this, i);
            }
        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }



        public Vector2 GetPos()
        {
            return new Vector2(position.X - GlobalSettings.BASE_SCALAR, position.Y + GlobalSettings.BASE_SCALAR);
        }

        public void SetPos(Vector2 pos)
        {
            position = pos;
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            
            timeSinceLastBomb += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;

                if (bossState.state != bossStateMachine.bossHealth.Not)// call update on the EnemySprite when not in 'NOT'
                {
                    bossState.MachineEnemySprite.Update();
                }
            }

            if (timeSinceLastBomb > 4000 && bossState.state != bossStateMachine.bossHealth.Not && bossState.state != bossStateMachine.bossHealth.Die)
            {
                timeSinceLastBomb = 0;
                foreach(IItem bomb in bombItem)
                {
                     bomb.ChangeToBeingUsed();
                }

            }

            hitbox.Location = new Point((int)position.X, (int)position.Y);
            enemyBlockCollision.HandleCollision(this, enemyCollisionDetector.CheckBlockCollisions(hitbox));
            if (enemyCollisionDetector.CheckItemCollision(hitbox) != GlobalSettings.CollisionType.None)
            {
                bossState.changeToDie();
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, BOSS_WIDTH, BOSS_HEIGHT);

            //Remove moblin from screen 3 seconds after death
            if (bossState.state == bossStateMachine.bossHealth.Die)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;
                //wait 3 seconds
                if (deathTimer > 3000)
                {
                    deathTimer = 0;
                    game.levelCycleRecord.RemoveOneIndex(GlobalSettings.BOSS_ENEMY);
                    game.itemList.Add(new RandomDrop(position, spriteBatch, game).DropFinal());
                    bossState.changeToNot();
                }
            }

            if (bossState.state == bossStateMachine.bossHealth.Not)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            }
            foreach (IItem bomb in bombItem)
            {
                bomb.Update();
            }

        }


        public void Draw()
        {
            if (firstTimeEncounter)
            {
                SoundFactory.Instance.BossScreamEffect();
                firstTimeEncounter = false;
            }
            if (bossState.state == bossStateMachine.bossHealth.Die)
            {
                tintColor = Color.Red;
                bossState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
            }

            else if ((bossState.state != bossStateMachine.bossHealth.Not) && (bossState.state != bossStateMachine.bossHealth.Die))
            {
                if (damageTaken == 1)
                {
                    tintColor = Color.OrangeRed;
                    bossState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else if (damageTaken == 2)
                {
                    tintColor = Color.Magenta;
                    bossState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else
                {
                    tintColor = Color.White;
                    bossState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
            }
            foreach (IItem bomb in bombItem)
            {
                bomb.Draw();
            }
        }


        public void damage()
        {
            damageTaken++;

            if (damageTaken == 3)
            {
                damageTaken = 0;
                bossState.changeToDie();
                game.levelCycleRecord.RemoveOneIndex(GlobalSettings.BOSS_ENEMY);
            }

        }
        //Functions to switch the states
        public void changeToIdle()
        {
            bossState.changeToIdle();
        }
        public void changeToDie()
        {
            bossState.changeToDie();
        }

        public void changeToNot()
        {
            bossState.changeToNot();
        }

        public GlobalSettings.Direction GetDirection()
        {
            return direction;
        }

        public void changeToMoveLeft()
        {

        }

        public void changeToMoveRight()
        {

        }
        public void changeToMoveUp()
        {

        }
        public void changeToMoveDown()
        {

        }
    }


    public class bossStateMachine
    {
        public enum bossHealth { Idle, Die, Not }; // the different possible states
        public bossHealth state; // the current health state of the moblin
        public BossSprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public bossStateMachine()
        {
            state = bossHealth.Idle;
            MachineEnemySprite = (BossSprite)SpriteFactory.Instance.CreateBoss();

        }


        public void changeToIdle()
        {
            //change to idle if not already Idle
            if (state != bossHealth.Idle)
            {
                state = bossHealth.Idle;
                MachineEnemySprite = (BossSprite)SpriteFactory.Instance.CreateBoss();

                //get appropriate sprite from sprite factory
            }

        }

        public void changeToRightMove()
        {

        }

        public void changeToLeftMove()
        {

        }

        public void changeToMoveUp()
        {

        }
        public void changeToMoveDown()
        {

        }

        public void changeToDie()
        {
            //change to Die if not already Die
            if (state != bossHealth.Die)
            {
                state = bossHealth.Die;
                MachineEnemySprite = (BossSprite)SpriteFactory.Instance.CreateBossDie();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToNot()
        {
            //change to Not if not already Not
            if (state != bossHealth.Not)
            {
                state = bossHealth.Not;
            }
        }

    }
}
