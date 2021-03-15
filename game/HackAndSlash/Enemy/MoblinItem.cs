﻿
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
    public class MoblinItem : IItem
    {

        private Game1 game;
        private IEnemy enemy; //enemy reference

        private ItemSprite bombSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private MoblinBombStateMachine itemState;
        private const int USE_DURATION = 160; // length of effect
        private int useDurationCounter = 0;
        private const int MAX_RANGE = 5; // range in # of sprites(tiles) 

        public Rectangle[] collidableTiles;
        public EnemyItemCollisionHandler moblinBombCollisionHandler;

        private GlobalSettings.Direction enemyDirection;

        // Constructor
        public MoblinItem(SpriteBatch gameSpriteBatch, Game1 game, IEnemy enemy)
        {
            this.game = game;
            this.enemy = enemy;

            position = enemy.GetPos();
            itemState = new MoblinBombStateMachine();
            bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
            spriteWidth = GlobalSettings.BASE_SCALAR;
            spriteHeight = GlobalSettings.BASE_SCALAR;
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            moblinBombCollisionHandler = new EnemyItemCollisionHandler(this.game.Player);
        }

        public void Update()
        {
            switch (itemState.state)
            {
                case MoblinBombStateMachine.ItemStates.BeingUsed:
                    bombSprite.Update();
                    useDurationCounter++;
                    

                    if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0)
                    {
                        Rectangle checkTile;
                        List<IBlock> blockList = game.blockList;
                        switch (enemyDirection)
                        {
                            case GlobalSettings.Direction.Left: // left
                                checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.X -= spriteWidth;
                                }

                                else if(moblinBombCollisionHandler.CheckForPlayerCollision(checkTile))
                                {
                                    this.game.Player.Damaged();
                                    ChangeToExpended();
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                            case GlobalSettings.Direction.Right: // right
                                checkTile = new Rectangle((int)position.X + spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.X += spriteWidth;
                                }

                                else if (moblinBombCollisionHandler.CheckForPlayerCollision(checkTile))
                                {
                                    this.game.Player.Damaged();
                                    ChangeToExpended();
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                            case GlobalSettings.Direction.Up: // up
                                checkTile = new Rectangle((int)position.X, (int)position.Y - spriteHeight, spriteWidth, spriteHeight);
                                if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.Y -= spriteHeight;
                                    ChangeToExpended();
                                }

                                else if (moblinBombCollisionHandler.CheckForPlayerCollision(checkTile))
                                {
                                    this.game.Player.Damaged();
                                    ChangeToExpended();
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                            case GlobalSettings.Direction.Down: // down
                                checkTile = new Rectangle((int)position.X, (int)position.Y + spriteHeight, spriteWidth, spriteHeight);
                                if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.Y += spriteHeight;
                                }

                                else if (moblinBombCollisionHandler.CheckForPlayerCollision(checkTile))
                                {
                                    this.game.Player.Damaged();
                                    ChangeToExpended();
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                        }

                        
                    }

                    break;
                case MoblinBombStateMachine.ItemStates.Expended:
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
            }
        }

        public void Draw()
        {
            switch (itemState.state)
            {
                case MoblinBombStateMachine.ItemStates.BeingUsed:
                    bombSprite.Draw(spriteBatch, position, Color.White);
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
                case MoblinBombStateMachine.ItemStates.Expended:
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
            }
        }

        

        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            Rectangle[] RectanglesList = { new Rectangle(0, 0, 1, 1) };
            if ((isEnemy && itemState.state == MoblinBombStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == MoblinBombStateMachine.ItemStates.BeingUsed))
                RectanglesList = collidableTiles;

            return RectanglesList;
        }

        public void ChangeToBeingUsed()
        {
            itemState.ChangeToBeingUsed();
            bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
            position = enemy.GetPos();
            enemyDirection = enemy.GetDirection();
            
        }

        public void ChangeToExpended()
        {
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
    }

    public class MoblinBombStateMachine
    {
        public enum ItemStates { BeingUsed, Expended };
        public ItemStates state;
        public MoblinBombStateMachine()
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