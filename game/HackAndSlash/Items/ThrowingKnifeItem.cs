
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class ThrowingKnifeItem : IItem
    {
        
        private Game1 game;
        private IPlayer player; //Player reference

        private ItemSprite throwingKnifeSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private ThrowingKnifeStateMachine itemState;
        private static int numUses = 0;
        private const int USE_DURATION = 100; // length of effect
        private int useDurationCounter = 0;
        private static int cooldown = 0; // item is useable if == 0 
        private const int ITEM_COOLDOWN = 30; // time in update cycles between uses 
        private const int MAX_RANGE = 5; // range in # of sprites(tiles) 
        private Vector2 toolBarPosition;

        public static bool inInventory = false;

        public Rectangle[] collidableTiles;
        public ItemCollisionHandler throwingKnifeCollisionHandler;

        private TextSprite textSprites;

        private GlobalSettings.Direction playerDirection = GlobalSettings.Direction.Right;
        private Vector2 playerPosition;

        // Constructor
        public ThrowingKnifeItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            this.player = this.game.Player; //Reference of player from Game1

            position = startPosition;
            itemState = new ThrowingKnifeStateMachine();
            throwingKnifeSprite = (ItemSprite)SpriteFactory.Instance.CreateThrowingKnife(GlobalSettings.Direction.Up);
            spriteWidth = throwingKnifeSprite.Texture.Width / throwingKnifeSprite.Columns;
            spriteHeight = throwingKnifeSprite.Texture.Height / throwingKnifeSprite.Rows;
            toolBarPosition = new Vector2(2 * GlobalSettings.BASE_SCALAR, 0);
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            throwingKnifeCollisionHandler = new ItemCollisionHandler(this.player);
            textSprites = (TextSprite)SpriteFactory.Instance.CreateTextCharacters();
        }

        public void Update()
        {
            switch (itemState.state)
            {
                case ThrowingKnifeStateMachine.ItemStates.Collectable:
                    // check for collision collision -> collect Item
                    // if numUses != 0
                    if (throwingKnifeCollisionHandler.CheckForPlayerCollision(collidableTiles))
                    {
                        CollectItem();
                        game.levelCycleRecord.RemoveOneIndex(GlobalSettings.THROWING_KNIFE_ITEM);
                    }
                    throwingKnifeSprite.Update();
                    break;
                case ThrowingKnifeStateMachine.ItemStates.Useable:
                    // check for uses
                    position = toolBarPosition;
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    cooldown--;
                    if (cooldown <= 0)
                    {
                        cooldown = 0;
                    }
                    break;
                case ThrowingKnifeStateMachine.ItemStates.BeingUsed:
                    // check for player position, update position
                    // check number of uses left, update, if zero->Expended
                    throwingKnifeSprite.Update();
                    useDurationCounter++;
                    if (useDurationCounter >= USE_DURATION)
                    {
                        useDurationCounter = 0;

                        if (numUses > 0)
                        {
                            ChangeToUseable();
                        }
                        else
                        {
                            ChangeToExpended();
                        }
                    }
                    // TODO: Check for collisions that shorten total length (walls)
                    if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0)
                    {
                        Rectangle checkTile;
                        List<IBlock> blockList = game.blockList;
                        switch (playerDirection)
                        {
                            case GlobalSettings.Direction.Left: // left
                                checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.X -= spriteWidth;
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                            case GlobalSettings.Direction.Right: // right
                                checkTile = new Rectangle((int)position.X + spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.X += spriteWidth;
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                            case GlobalSettings.Direction.Up: // up
                                checkTile = new Rectangle((int)position.X, (int)position.Y - spriteHeight, spriteWidth, spriteHeight);
                                if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.Y -= spriteHeight;
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                            case GlobalSettings.Direction.Down: // down
                                checkTile = new Rectangle((int)position.X, (int)position.Y + spriteHeight, spriteWidth, spriteHeight);
                                if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.Y += spriteHeight;
                                }
                                else
                                {
                                    ChangeToExpended();
                                }
                                break;
                        }
                    }

                    break;
                case ThrowingKnifeStateMachine.ItemStates.Expended:
                    // single instance is gone
                    position = toolBarPosition;
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    if (numUses > 0)
                    {
                        ChangeToUseable();
                    }
                    break;
            }
        }

        public void Draw()
        {
            switch (itemState.state)
            {
                case ThrowingKnifeStateMachine.ItemStates.Collectable:
                    // Draw on ground
                    throwingKnifeSprite.Draw(spriteBatch, position, Color.White);
                    break;
                case ThrowingKnifeStateMachine.ItemStates.Useable:
                    // In bag Draw on toolbar
                    throwingKnifeSprite.Draw(spriteBatch, position, Color.White);
                    // draw text with numUses
                    textSprites.Draw(spriteBatch, numUses.ToString(), new Vector2(toolBarPosition.X, toolBarPosition.Y + 64), Color.White);
                    break;
                case ThrowingKnifeStateMachine.ItemStates.BeingUsed:
                    // place over players head then draw wall with loop and updating position
                    throwingKnifeSprite.Draw(spriteBatch, position, Color.White);
                    // draw back to player starting position
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
                case ThrowingKnifeStateMachine.ItemStates.Expended:
                    // Gray out in toolbar
                    throwingKnifeSprite.Draw(spriteBatch, toolBarPosition, Color.Gray);
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    break;
            }
        }

        public void CollectItem()
        {
            if (!inInventory)
            {
                ChangeToUseable();
                inInventory = true;
                game.useableItemList.Add(this);
            }
            else if (game.useableItemList.Contains(this))
            {
                ChangeToUseable();
            }
            else
            {
                ChangeToExpended();
                toolBarPosition = new Vector2(0, -128);
            }
            numUses++;
        }

        public void UseItem(GlobalSettings.Direction currentPlayerDirection)
        {
            Vector2 currentPlayerPosition = this.player.GetPos();
            if (itemState.state == ThrowingKnifeStateMachine.ItemStates.Useable && cooldown == 0)
            {
                Rectangle checkTile;
                List<IBlock> blockList = game.blockList;
                throwingKnifeSprite = (ItemSprite)SpriteFactory.Instance.CreateThrowingKnife(currentPlayerDirection);
                switch (currentPlayerDirection)
                {
                    case GlobalSettings.Direction.Left: // left
                        checkTile = new Rectangle((int)currentPlayerPosition.X - spriteWidth, (int)currentPlayerPosition.Y, spriteWidth, spriteHeight);
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.X -= spriteWidth;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            itemState.ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Right: // right
                        checkTile = new Rectangle((int)currentPlayerPosition.X + spriteWidth, (int)currentPlayerPosition.Y, spriteWidth, spriteHeight);
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.X += spriteWidth;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            itemState.ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Up: // up
                        checkTile = new Rectangle((int)currentPlayerPosition.X, (int)currentPlayerPosition.Y - spriteHeight, spriteWidth, spriteHeight);
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.Y -= spriteHeight;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            itemState.ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Down: // down
                        checkTile = new Rectangle((int)currentPlayerPosition.X, (int)currentPlayerPosition.Y + spriteHeight, spriteWidth, spriteHeight);
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.Y += spriteHeight;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            itemState.ChangeToUseable();
                        }
                        break;
                }

                playerPosition = currentPlayerPosition; // player position when used TEMP: DEFAULT POS
                position = playerPosition;
                playerDirection = currentPlayerDirection;// player Direction
                // itemState.ChangeToBeingUsed();
                cooldown = ITEM_COOLDOWN;
                numUses--;
                if (numUses < 0)
                {
                    numUses = 0;
                }
            }

        }

        // returns collidableTiles for enemy damage or player collection
        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            Rectangle[] RectanglesList = { new Rectangle(0, 0, 1, 1) };
            if ((isEnemy && itemState.state == ThrowingKnifeStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == ThrowingKnifeStateMachine.ItemStates.BeingUsed))
                RectanglesList = collidableTiles;

            return RectanglesList;
        }

        public void SetToolbarPosition(int index)
        {
            toolBarPosition = new Vector2(index * GlobalSettings.BASE_SCALAR, 0);
        }


        public void ChangeToCollectable()
        {
            //player drops item
            itemState.ChangeToCollectable();
            throwingKnifeSprite = (ItemSprite)SpriteFactory.Instance.CreateThrowingKnife(GlobalSettings.Direction.Up);

        }

        public void ChangeToUseable()
        {
            //player collects item
            itemState.ChangeToUseable();
            throwingKnifeSprite = (ItemSprite)SpriteFactory.Instance.CreateThrowingKnife(GlobalSettings.Direction.Up);
        }

        public void ChangeToBeingUsed()
        {
            //player is using item
            itemState.ChangeToBeingUsed();
        }

        public void ChangeToExpended()
        {
            //player used this instance of the item
            itemState.ChangeToExpended();
            throwingKnifeSprite = (ItemSprite)SpriteFactory.Instance.CreateThrowingKnife(GlobalSettings.Direction.Up);
        }
    }

    public class ThrowingKnifeStateMachine
    {
        public enum ItemStates { Collectable, Useable, BeingUsed, Expended };
        public ItemStates state;
        public ThrowingKnifeStateMachine()
        {
            state = ItemStates.Collectable;
        }

        public void ChangeToCollectable()
        {
            //player drops item
            state = ItemStates.Collectable;
        }

        public void ChangeToUseable()
        {
            //player collects item
            state = ItemStates.Useable;
        }

        public void ChangeToBeingUsed()
        {
            //player is using item
            state = ItemStates.BeingUsed;
        }

        public void ChangeToExpended()
        {
            //player used this instance of the item
            state = ItemStates.Expended;
        }
    }
}