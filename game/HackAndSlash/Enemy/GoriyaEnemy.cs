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
    public class GoriyaEnemy : IEnemy
    {
        private goriyaStateMachine goriyaState; // to keep track of the current state of the moblin
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

        private System.Random random;
        private int[] directionHistory = new int[] { 0, 0, 0, 0 };
        private int randomDirection = 0; //0-left, 1-up, 2-right, 3- down

        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        private Rectangle hitbox;

        private IItem boomerangItem;
        public GlobalSettings.Direction direction {get;set;}

        private int damageTaken;
        private int totalLives;
        private Color tintColor;

        private int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        private int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        //Moblin position
        private Rectangle rectangle { get; set; }

        //make the constructor for the class
        public GoriyaEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch SB, Game1 game)
        {
            
            position = startPosition;
            goriyaState = new goriyaStateMachine();
            direction = GlobalSettings.Direction.Left;
            Graphics = graphics;
            spriteBatch = SB;
            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            random = new System.Random();

            enemyCollisionDetector = new EnemyCollisionDetector(game, this);
            enemyBlockCollision = new EnemyBlockCollision();
            hitbox = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            damageTaken = 0;
            totalLives = 3;

            boomerangItem = new GoriyaItem(spriteBatch, game, this);
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
                    changeToMoveLeft();
                    break;
                case (int)GlobalSettings.Direction.Up:
                    changeToMoveUp();
                    break;
                case (int)GlobalSettings.Direction.Right:
                    changeToMoveRight();
                    break;
                case (int)GlobalSettings.Direction.Down:
                    changeToMoveDown();
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

                if (goriyaState.state != goriyaStateMachine.goriyaHealth.Not)// call update on the EnemySprite when not in 'NOT'
                {
                    goriyaState.MachineEnemySprite.Update();
                }
            }

            //Boundary collisions
            if (goriyaState.state == goriyaStateMachine.goriyaHealth.MoveUp)
            {
                // Move up
                if (position.Y > GlobalSettings.BORDER_OFFSET)
                {
                    if (GlobalSettings.NIGHTMAREMODE)
                    {
                        position.Y -= 2;
                    }
                    else
                    {
                        position.Y--;
                    }
                }

                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, false, false, true });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (goriyaState.state == goriyaStateMachine.goriyaHealth.MoveDown)
            {
                //Move down
                if (position.Y < bottomBound)
                {
                    if (GlobalSettings.NIGHTMAREMODE)
                    {
                        position.Y += 2;
                    }
                    else
                    {
                        position.Y++;
                    }
                }

                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, false, true, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }
            else if (goriyaState.state == goriyaStateMachine.goriyaHealth.MoveLeft)
            {
                //Move left
                if (position.X > GlobalSettings.BORDER_OFFSET)
                {
                    if (GlobalSettings.NIGHTMAREMODE)
                    {
                        position.X -= 2;
                    }

                    else
                    {
                        position.X--;
                    }
                }
                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { true, false, false, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);

                }
            }
            else if (goriyaState.state == goriyaStateMachine.goriyaHealth.MoveRight)
            {
                //Move right
                if (position.X < rightBound)
                {
                    if (GlobalSettings.NIGHTMAREMODE)
                    {
                        position.X += 2;
                    }

                    else
                    {
                        position.X++;
                    }
                }
                else
                {
                    int NewDirection = new PRNG().DirectionMask(directionHistory,
                        new bool[] { false, true, false, false });
                    directionHistory[NewDirection] += 1;
                    Turn(NewDirection);
                }
            }

            if (timeSinceDirectionChange > 8000 && goriyaState.state != goriyaStateMachine.goriyaHealth.Not && goriyaState.state != goriyaStateMachine.goriyaHealth.Die)
            {
                timeSinceDirectionChange = 0;
                randomDirection = random.Next(0, 3);
                switch (randomDirection)
                {
                    case 0:
                        direction = GlobalSettings.Direction.Left;
                        changeToMoveLeft();
                        break;
                    case 1:
                        direction = GlobalSettings.Direction.Up;
                        changeToMoveUp();
                        break;
                    case 2:
                        direction = GlobalSettings.Direction.Right;
                        changeToMoveRight();
                        break;
                    case 3:
                        direction = GlobalSettings.Direction.Down;
                        changeToMoveDown();
                        break;
                }
            }

            if (timeSinceLastBomb > 4000 && goriyaState.state != goriyaStateMachine.goriyaHealth.Not && goriyaState.state != goriyaStateMachine.goriyaHealth.Die)
            {
                timeSinceLastBomb = 0;
                boomerangItem.ChangeToBeingUsed();
            }

            hitbox.Location = new Point((int)position.X, (int)position.Y);
            enemyBlockCollision.HandleCollision(this, enemyCollisionDetector.CheckBlockCollisions(hitbox));
            if (enemyCollisionDetector.CheckItemCollision(hitbox) != GlobalSettings.CollisionType.None)
            {
                changeToDie();
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            //Remove moblin from screen shortly after death
            if (goriyaState.state == goriyaStateMachine.goriyaHealth.Die)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (deathTimer > dieTotalTime)
                {
                    SoundFactory.Instance.MoblinDies();
                    deathTimer = 0;
                    game.levelCycleRecord.RemoveOneIndex(GlobalSettings.GORIYA_ENEMY);
                    if (GlobalSettings.RND.Next(100) < 50)
                        game.itemList.Add(new RandomDrop(position, spriteBatch, game).RandItem());
                    changeToNot();
                }
            }

            if (goriyaState.state == goriyaStateMachine.goriyaHealth.Not)
            {
                hitbox.Location = new Point(0, 0);
                rectangle = new Rectangle(hitbox.X, hitbox.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
            }

            boomerangItem.Update();

        }


        public void Draw()
        {
            if (goriyaState.state == goriyaStateMachine.goriyaHealth.Die)
            {
                tintColor = Color.Red;
                goriyaState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
            }

            else if ((goriyaState.state != goriyaStateMachine.goriyaHealth.Not) && (goriyaState.state != goriyaStateMachine.goriyaHealth.Die))
            {
                if (damageTaken == 1)
                {
                    tintColor = Color.OrangeRed;
                    goriyaState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else if (damageTaken == 2)
                {
                    tintColor = Color.Magenta;
                    goriyaState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else if (damageTaken == 3)
                {
                    tintColor = Color.Brown;
                    goriyaState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else if (damageTaken == 4)
                {
                    tintColor = Color.Pink;
                    goriyaState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
                else
                {
                    tintColor = Color.White;
                    goriyaState.MachineEnemySprite.Draw(spriteBatch, position, tintColor);
                }
            }
            boomerangItem.Draw();
        }


        public void damage()
        {
            Boolean shielded = isGoriyaShielded();

            if (!shielded)
            {
                damageTaken++;
                if (GlobalSettings.NIGHTMAREMODE)
                {
                    totalLives = 5;
                }
                else
                {
                    totalLives = 3;
                }
                if (damageTaken == totalLives)
                {
                    damageTaken = 0;
                    changeToDie();
                }
            }

        }

        Boolean isGoriyaShielded()
        {
            bool shield = true;
            if ((direction == GlobalSettings.Direction.Up || direction == GlobalSettings.Direction.Down) && (game.Player.GetDir() == GlobalSettings.Direction.Left || game.Player.GetDir() == GlobalSettings.Direction.Right))
            {
                shield = false;
            }

            if ((direction == GlobalSettings.Direction.Right || direction == GlobalSettings.Direction.Left) && (game.Player.GetDir() == GlobalSettings.Direction.Up || game.Player.GetDir() == GlobalSettings.Direction.Down))
            {
                shield = false;
            }

            return shield;
        }
        //Functions to switch the states
        public void changeToIdle()
        {
            goriyaState.changeToIdle();
        }

        public void changeToMoveRight()
        {
            direction = GlobalSettings.Direction.Right;
            goriyaState.changeToRightMove();
        }

        public void changeToMoveLeft()
        {
            direction = GlobalSettings.Direction.Left;
            goriyaState.changeToLeftMove();
        }

        public void changeToMoveUp()
        {
            direction = GlobalSettings.Direction.Up;
            goriyaState.changeToMoveUp();
        }

        public void changeToMoveDown()
        {
            direction = GlobalSettings.Direction.Down;
            goriyaState.changeToMoveDown();
        }

        public void changeToDie()
        {
            goriyaState.changeToDie();
        }

        public void changeToNot()
        {
            goriyaState.changeToNot();
        }

        public GlobalSettings.Direction GetDirection()
        {
            return direction;
        }
    }


    public class goriyaStateMachine
    {
        public enum goriyaHealth { Idle, MoveUp, MoveDown, MoveLeft, MoveRight, Die, Not }; // the different possible states
        public goriyaHealth state; // the current health state of the moblin
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public goriyaStateMachine()
        {
            state = goriyaHealth.MoveLeft;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaMoveLeft();
        }


        public void changeToIdle()
        {
            //change to idle if not already Idle
            if (state != goriyaHealth.Idle)
            {
                state = goriyaHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaIdle();
                //get appropriate sprite from sprite factory
            }

        }

        public void changeToRightMove()
        {
            //change to Attack if not already in Attack
            if (state != goriyaHealth.MoveRight)
            {
                state = goriyaHealth.MoveRight;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaMoveRight();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToLeftMove()
        {
            //change to Move if not already Move
            if (state != goriyaHealth.MoveLeft)
            {
                state = goriyaHealth.MoveLeft;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaMoveLeft();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveUp()
        {
            //change to Move if not already Move
            if (state != goriyaHealth.MoveUp)
            {
                state = goriyaHealth.MoveUp;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaMoveUp();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveDown()
        {
            //change to Move if not already Move
            if (state != goriyaHealth.MoveDown)
            {
                state = goriyaHealth.MoveDown;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaMoveDown();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToDie()
        {
            //change to Die if not already Die
            if (state != goriyaHealth.Die)
            {
                state = goriyaHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateGoriyaDie();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToNot()
        {
            //change to Not if not already Not
            if (state != goriyaHealth.Not)
            {
                state = goriyaHealth.Not;
            }
        }

    }
}
