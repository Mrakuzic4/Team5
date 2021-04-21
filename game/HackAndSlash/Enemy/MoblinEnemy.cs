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
    public class MoblinEnemy : IEnemy
    {
        private moblinStateMachine moblinState; // to keep track of the current state of the moblin
        private Vector2 position; // the position of the enemy on screen
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch
        private Game1 game;

        private int timeSinceLastFrame = 0; // used to slow down the rate of animation
        private int timeSinceLastBomb = 0;
        private int timeSinceDirectionChange = 0;
        private int deathTimer = 0;
        private int milliSecondsPerFrame = 60;
        private int dieTotalTime = 500;
        private int temp = 0; //counter to change states after a certain number of calls to update

        private System.Random random;
        private int[] directionHistory = new int[] { 0, 0, 0, 0 };
        private int randomDirection = 0; //0-left, 1-up, 2-right, 3- down

        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        private Rectangle hitbox;

        private IItem bombItem;
        public GlobalSettings.Direction direction {get;set;}

        private int damageTaken;
        private Color tintColor;

        private int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        private int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        //Moblin position
        private Rectangle rectangle { get; set; }

        //make the constructor for the class
        public MoblinEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch SB, Game1 game)
        {
            
            position = startPosition;
            moblinState = new moblinStateMachine();
            Graphics = graphics;
            spriteBatch = SB;
            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            random = new System.Random();

            enemyCollisionDetector = new EnemyCollisionDetector(game, this);
            enemyBlockCollision = new EnemyBlockCollision();
            hitbox = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            damageTaken = 0;

            bombItem = new MoblinItem(spriteBatch, game, this);
            directionHistory[Turn(GlobalSettings.RND.Next(3))] += 1;
            this.game = game;
        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }



        public Vector2 GetPos()
        {
            return position;
        }

        public void SetPos(Vector2 pos)
        {
            position = pos;
        }

        public int Turn(int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Left:
                    moblinState.changeToLeftMove();
                    break;
                case (int)GlobalSettings.Direction.Up:
                    moblinState.changeToMoveUp();
                    break;
                case (int)GlobalSettings.Direction.Right:
                    moblinState.changeToRightMove();
                    break;
                case (int)GlobalSettings.Direction.Down:
                    moblinState.changeToMoveDown();
                    break;
            }
            return Direction;
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            timeSinceLastBomb += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceDirectionChange += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;

                if (moblinState.state != moblinStateMachine.moblinHealth.Not)// call update on the EnemySprite when not in 'NOT'
                {
                    moblinState.MachineEnemySprite.Update();
                }
            }

            System.Console.WriteLine("In update"+moblinState.state);
            //Boundary collisions
            if (moblinState.state == moblinStateMachine.moblinHealth.MoveUp)
            {
                // Move up
                if (position.Y > GlobalSettings.BORDER_OFFSET)
                {
                    position.Y--;
                }

                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, false, false, true });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (moblinState.state == moblinStateMachine.moblinHealth.MoveDown)
            {
                //Move down
                if (position.Y < bottomBound)
                {
                   position.Y++;
                }

                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, false, true, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (moblinState.state == moblinStateMachine.moblinHealth.MoveLeft)
            {
                //Move left
                if (position.X > GlobalSettings.BORDER_OFFSET)
                {
                    position.X--;
                }
                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { true, false, false, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);

                }
            }
            else if (moblinState.state == moblinStateMachine.moblinHealth.MoveRight)
            {
                //Move right
                if (position.X < rightBound)
                {
                    position.X++;
                }
                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, true, false, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }

            if (timeSinceDirectionChange > 8000 && moblinState.state != moblinStateMachine.moblinHealth.Not && moblinState.state != moblinStateMachine.moblinHealth.Die)
            {
                timeSinceDirectionChange = 0;
                randomDirection = random.Next(0, 3);
                switch (randomDirection)
                {
                    case 0:
                        direction = GlobalSettings.Direction.Left;
                        moblinState.changeToLeftMove();
                        break;
                    case 1:
                        direction = GlobalSettings.Direction.Up;
                        moblinState.changeToMoveUp();
                        break;
                    case 2:
                        direction = GlobalSettings.Direction.Right;
                        moblinState.changeToRightMove();
                        break;
                    case 3:
                        direction = GlobalSettings.Direction.Down;
                        moblinState.changeToMoveDown();
                        break;
                }
            }

            if (timeSinceLastBomb > 4000 && moblinState.state != moblinStateMachine.moblinHealth.Not && moblinState.state != moblinStateMachine.moblinHealth.Die)
            {
                timeSinceLastBomb = 0;
                bombItem.ChangeToBeingUsed();
            }

            hitbox.Location = new Point((int)position.X, (int)position.Y);
            enemyBlockCollision.HandleCollision(this, enemyCollisionDetector.CheckBlockCollisions(hitbox));
            if (enemyCollisionDetector.CheckItemCollision(hitbox) != GlobalSettings.CollisionType.None)
            {
                moblinState.changeToDie();
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            //Remove moblin from screen shortly after death
            if (moblinState.state == moblinStateMachine.moblinHealth.Die)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (deathTimer > dieTotalTime)
                {
                    SoundFactory.Instance.MoblinDies();
                    deathTimer = 0;
                    game.levelCycleRecord.RemoveOneIndex(GlobalSettings.MOBLIN_ENEMY);
                    if (GlobalSettings.RND.Next(100) < game._DropRateBaseline) 
                        game.itemList.Add(new RandomDrop(position, spriteBatch, game).RandItem());
                    moblinState.changeToNot();
                }
            }

            if (moblinState.state == moblinStateMachine.moblinHealth.Not)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            }

            bombItem.Update();

        }


        public void Draw()
        {
            if (moblinState.state == moblinStateMachine.moblinHealth.Die)
            {
                tintColor = Color.Red;
                moblinState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
            }

            else if ((moblinState.state != moblinStateMachine.moblinHealth.Not) && (moblinState.state != moblinStateMachine.moblinHealth.Die))
            {
                if (damageTaken == 1)
                {
                    tintColor = Color.OrangeRed;
                    moblinState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else if (damageTaken == 2)
                {
                    tintColor = Color.Magenta;
                    moblinState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else
                {
                    tintColor = Color.White;
                    moblinState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
            }
            bombItem.Draw();
        }


        public void damage()
        {
            damageTaken++;

            if (damageTaken == 3)
            {
                damageTaken = 0;
                moblinState.changeToDie();
            }

        }
        //Functions to switch the states
        public void changeToIdle()
        {
            moblinState.changeToIdle();
        }

        public void changeToMoveRight()
        {
            direction = GlobalSettings.Direction.Right;
            moblinState.changeToRightMove();
        }

        public void changeToMoveLeft()
        {
            direction = GlobalSettings.Direction.Left;
            moblinState.changeToLeftMove();
        }

        public void changeToMoveUp()
        {
            direction = GlobalSettings.Direction.Up;
            moblinState.changeToMoveUp();
        }

        public void changeToMoveDown()
        {
            direction = GlobalSettings.Direction.Down;
            moblinState.changeToMoveDown();
        }

        public void changeToDie()
        {
            moblinState.changeToDie();
        }

        public void changeToNot()
        {
            moblinState.changeToNot();
        }

        public GlobalSettings.Direction GetDirection()
        {
            return direction;
        }
    }


    public class moblinStateMachine
    {
        public enum moblinHealth { Idle, MoveUp, MoveDown, MoveLeft, MoveRight, Die, Not }; // the different possible states
        public moblinHealth state; // the current health state of the moblin
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public moblinStateMachine()
        {
            state = moblinHealth.MoveLeft;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinMoveLeft();
        }


        public void changeToIdle()
        {
            //change to idle if not already Idle
            if (state != moblinHealth.Idle)
            {
                state = moblinHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinIdle();
                //get appropriate sprite from sprite factory
            }

        }

        public void changeToRightMove()
        {
            //change to Attack if not already in Attack
            if (state != moblinHealth.MoveRight)
            {
                state = moblinHealth.MoveRight;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinMoveRight();
                System.Console.WriteLine("changed state to: " + state);
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToLeftMove()
        {
            //change to Move if not already Move
            if (state != moblinHealth.MoveLeft)
            {
                state = moblinHealth.MoveLeft;
                System.Console.WriteLine("changed state to: " + state);
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinMoveLeft();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveUp()
        {
            //change to Move if not already Move
            if (state != moblinHealth.MoveUp)
            {
                state = moblinHealth.MoveUp;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinMoveUp();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveDown()
        {
            //change to Move if not already Move
            if (state != moblinHealth.MoveDown)
            {
                state = moblinHealth.MoveDown;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinMoveDown();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToDie()
        {
            //change to Die if not already Die
            if (state != moblinHealth.Die)
            {
                state = moblinHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateMoblinDie();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToNot()
        {
            //change to Not if not already Not
            if (state != moblinHealth.Not)
            {
                state = moblinHealth.Not;
            }
        }

    }
}
