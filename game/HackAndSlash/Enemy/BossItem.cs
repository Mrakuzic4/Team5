
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackAndSlash.Collision;

namespace HackAndSlash
{
    public class BossItem : IItem
    {

        private Game1 game;
        private IEnemy enemy; //enemy reference

        private int type;
        private ItemSprite bombSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private BossBombStateMachine itemState;
        private const int USE_DURATION = 160; // length of effect
        private int useDurationCounter = 0;
        private const int MAX_RANGE = 5; // range in # of sprites(tiles) 

        public Rectangle[] collidableTiles;
        public EnemyItemCollisionHandler bossBombCollisionHandler;

        // Constructor
        public BossItem(SpriteBatch gameSpriteBatch, Game1 game, IEnemy enemy, int type)
        {
            this.game = game;
            this.enemy = enemy;
            this.type = type;

            position = enemy.GetPos();
            itemState = new BossBombStateMachine();
            bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
            spriteWidth = GlobalSettings.BASE_SCALAR;
            spriteHeight = GlobalSettings.BASE_SCALAR;
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            bossBombCollisionHandler = new EnemyItemCollisionHandler(this.game.Player);
        }

        public void Update()
        {
            switch (itemState.state)
            {
                case BossBombStateMachine.ItemStates.BeingUsed:
                    bombSprite.Update();
                    useDurationCounter++;
                    if (useDurationCounter >= USE_DURATION)
                    {
                        useDurationCounter = 0;
                        ChangeToExpended();   
                    }

                    if (bossBombCollisionHandler.CheckForPlayerCollision(collidableTiles[0]))
                    {
                        this.game.Player.Damaged();
                        ChangeToExpended();
                    }

                    if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0)
                    {
                        Rectangle checkTile;
                        List<IBlock> blockList = game.blockList;
                        switch (type)
                        {
                            case 0:
                                {
                                    checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y - spriteHeight, spriteWidth, spriteHeight);
                                    if (!bossBombCollisionHandler.CheckForWall(checkTile) && !bossBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.X -= spriteWidth;
                                        position.Y -= spriteHeight;
                                    }
                                    else
                                    {
                                        ChangeToExpended();
                                    }

                                    break;
                                }
                            case 1:
                                {
                                    checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                    if (!bossBombCollisionHandler.CheckForWall(checkTile) && !bossBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.X -= spriteWidth;
                                    }
                                    else
                                    {
                                        ChangeToExpended();
                                    }

                                    break;
                                }
                            case 2:
                                {
                                    checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y + spriteHeight, spriteWidth, spriteHeight);
                                    if (!bossBombCollisionHandler.CheckForWall(checkTile) && !bossBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.X -= spriteWidth;
                                        position.Y += spriteHeight;
                                    }
                                    else
                                    {
                                        ChangeToExpended();
                                    }

                                    break;
                                }
                        }             
                    }
                    break;
                case BossBombStateMachine.ItemStates.Expended:
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
            }
        }

        public void Draw()
        {
            switch (itemState.state)
            {
                case BossBombStateMachine.ItemStates.BeingUsed:
                    bombSprite.Draw(spriteBatch, position, Color.White);
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
                case BossBombStateMachine.ItemStates.Expended:
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
            }
        }

        public bool InInventory()
        {
            return false;
        }
        public Vector2 GetPos()
        {
            return position;
        }
        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            Rectangle[] RectanglesList = { new Rectangle(0, 0, 1, 1) };
            if ((isEnemy && itemState.state == BossBombStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == BossBombStateMachine.ItemStates.BeingUsed))
                RectanglesList = collidableTiles;

            return RectanglesList;
        }

        public void ChangeToBeingUsed()
        {
            itemState.ChangeToBeingUsed();
            bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
            position = enemy.GetPos();
            
        }

        public void ChangeToExpended()
        {
            position = enemy.GetPos();
            itemState.ChangeToExpended();
        }

        public void ChangeToCollectable()
        {
        }

        public void ChangeToUseable()
        {
        }

        public void CollectItem()
        {
        }

        public void UseItem(GlobalSettings.Direction direction)
        {

        }

        public void SetToolbarPosition(int index)
        {

        }

    }

    public class BossBombStateMachine
    {
        public enum ItemStates { BeingUsed, Expended };
        public ItemStates state;
        public BossBombStateMachine()
        {
            state = ItemStates.Expended;
        }

        public void ChangeToBeingUsed()
        {
            state = ItemStates.BeingUsed;
        }

        public void ChangeToExpended()
        {
            state = ItemStates.Expended;
        }
    }
}