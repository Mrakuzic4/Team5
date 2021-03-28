
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class FirewallItem : IItem
    {
        private Game1 game;
        private IPlayer player; //Player reference

        private ItemSprite firewallSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private FirewallStateMachine itemState;
        private static int numUses = 0;
        private const int USE_DURATION = 100; // length of effect
        private int useDurationCounter = 0; 
        private static int cooldown = 0; // item is useable if == 0 
        private const int ITEM_COOLDOWN = 30; // time in update cycles between uses 
        private const int MAX_RANGE = 5; // range in # of sprites(tiles) 
        private Vector2 toolBarPosition;

        public static bool inInventory = false;

        public Rectangle[] collidableTiles;
        public ItemCollisionHandler firewallCollisionHandler;

        private GlobalSettings.Direction playerDirection = GlobalSettings.Direction.Right;
        private Vector2 playerPosition;

        private TextSprite textSprites;
        
        // Constructor
        public FirewallItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            this.player = this.game.Player; //Reference of player from Game1

            position = startPosition;
            itemState = new FirewallStateMachine();
            firewallSprite = (ItemSprite)SpriteFactory.Instance.CreateFirewall();
            spriteWidth = firewallSprite.Texture.Width / firewallSprite.Columns;
            spriteHeight = firewallSprite.Texture.Height / firewallSprite.Rows;
            toolBarPosition = new Vector2(0, 0);
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            firewallCollisionHandler = new ItemCollisionHandler(this.player);
            textSprites = (TextSprite)SpriteFactory.Instance.CreateTextCharacters();
        }

        public void Update()
        {
            switch (itemState.state)
            {
                case FirewallStateMachine.ItemStates.Collectable:
                    // check for collision collision -> collect Item
                    // if numUses != 0
                    if (firewallCollisionHandler.CheckForPlayerCollision(collidableTiles))
                    {
                        CollectItem();
                    }
                    firewallSprite.Update();
                    break;
                case FirewallStateMachine.ItemStates.Useable:
                    // check for uses
                    position = toolBarPosition;
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
                    cooldown--;
                    if (cooldown <= 0)
                    {
                        cooldown = 0;
                    }
                    if (numUses == 0)
                    {
                        ChangeToExpended();
                    }
                    break;
                case FirewallStateMachine.ItemStates.BeingUsed:
                    // check for player position, update position
                    // check number of uses left, update, if zero->Expended

                    firewallSprite.Update();
                    useDurationCounter++;
                    if(useDurationCounter >= USE_DURATION)
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

                    // Check for collisions that shorten total length (walls)
                    else if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0) {
                        Rectangle checkTile;
                        List<IBlock> blockList = game.blockList;
                        switch (playerDirection)
                        {
                            case GlobalSettings.Direction.Left: // left
                                checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList)) 
                                {
                                    position.X -= spriteWidth;
                                }
                                break;
                            case GlobalSettings.Direction.Right: // right
                                checkTile = new Rectangle((int)position.X + spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
                                if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.X += spriteWidth;
                                }
                                break;
                            case GlobalSettings.Direction.Up: // up
                                checkTile = new Rectangle((int)position.X, (int)position.Y - spriteHeight, spriteWidth, spriteHeight);
                                if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.Y -= spriteHeight;
                                }
                                break;
                            case GlobalSettings.Direction.Down: // down
                                checkTile = new Rectangle((int)position.X, (int)position.Y + spriteHeight, spriteWidth, spriteHeight);
                                if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                                {
                                    position.Y += spriteHeight;
                                }
                                break;
                        }
                    }

                    break;
                case FirewallStateMachine.ItemStates.Expended:
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
                case FirewallStateMachine.ItemStates.Collectable:
                    // Draw on ground
                    firewallSprite.Draw(spriteBatch, position, Color.White);
                    break;
                case FirewallStateMachine.ItemStates.Useable:
                    // In bag Draw on toolbar
                    firewallSprite.Draw(spriteBatch, position, Color.White);
                    // draw text with numUses
                    textSprites.Draw(spriteBatch, numUses.ToString(), new Vector2(toolBarPosition.X, toolBarPosition.Y + 64), Color.White);
                    break;
                case FirewallStateMachine.ItemStates.BeingUsed:
                    // place over players head then draw wall with loop and updating position
                    firewallSprite.Draw(spriteBatch, position, Color.White);
                    // draw back to player starting position
                    collidableTiles = new Rectangle[MAX_RANGE];
                    int c = 0;
                    Rectangle tempTile;
                    Vector2 tempPosition;
                    switch (playerDirection)
                    {
                        case GlobalSettings.Direction.Left : // facing left
                            for(float i = position.X; i <= playerPosition.X; i += spriteWidth)
                            {
                                tempPosition = new Vector2(i, playerPosition.Y);
                                tempTile = new Rectangle((int)i, (int)playerPosition.Y, spriteWidth, spriteHeight);
                                firewallSprite.Draw(spriteBatch, tempPosition, Color.White);
                                collidableTiles[c] = tempTile;
                                c++;
                            }
                            break;
                        case GlobalSettings.Direction.Right: // facing right
                            for (float i = position.X; i >= playerPosition.X; i -= spriteWidth)
                            {
                                tempPosition = new Vector2(i, playerPosition.Y);
                                tempTile = new Rectangle((int)i, (int)playerPosition.Y, spriteWidth, spriteHeight);
                                firewallSprite.Draw(spriteBatch, tempPosition, Color.White);
                                collidableTiles[c] = tempTile;
                                c++;
                            }
                            break;
                        case GlobalSettings.Direction.Up: // facing Up
                            for (float i = position.Y; i <= playerPosition.Y; i += spriteHeight)
                            {
                                tempPosition = new Vector2(playerPosition.X, i);
                                tempTile = new Rectangle((int)playerPosition.X, (int)i, spriteWidth, spriteHeight);
                                firewallSprite.Draw(spriteBatch, tempPosition, Color.White);
                                collidableTiles[c] = tempTile;
                                c++;

                            }
                            break;
                        case GlobalSettings.Direction.Down: // facing down
                            for (float i = position.Y; i >= playerPosition.Y; i -= spriteHeight)
                            {
                                tempPosition = new Vector2(playerPosition.X, i);
                                tempTile = new Rectangle((int)playerPosition.X, (int)i, spriteWidth, spriteHeight);

                                firewallSprite.Draw(spriteBatch, tempPosition, Color.White);
                                collidableTiles[c] = tempTile;
                                c++;
                            }
                            break;
                    }
                    // check for enemies to damage them
                    break;
                case FirewallStateMachine.ItemStates.Expended:
                    // Gray out in toolbar
                    firewallSprite.Draw(spriteBatch, toolBarPosition, Color.Gray);
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
            if (itemState.state == FirewallStateMachine.ItemStates.Useable && cooldown == 0)
            {
                Rectangle checkTile;
                List<IBlock> blockList = game.blockList;
                switch (currentPlayerDirection)
                {
                    case GlobalSettings.Direction.Left: // left
                        checkTile = new Rectangle((int)currentPlayerPosition.X - spriteWidth, (int)currentPlayerPosition.Y, spriteWidth, spriteHeight);
                        if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.X -= spriteWidth;
                            ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Right: // right
                        checkTile = new Rectangle((int)currentPlayerPosition.X + spriteWidth, (int)currentPlayerPosition.Y, spriteWidth, spriteHeight);
                        if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.X += spriteWidth;
                            ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Up: // up
                        checkTile = new Rectangle((int)currentPlayerPosition.X, (int)currentPlayerPosition.Y - spriteHeight, spriteWidth, spriteHeight);
                        if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.Y -= spriteHeight;
                            ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Down: // down
                        checkTile = new Rectangle((int)currentPlayerPosition.X, (int)currentPlayerPosition.Y + spriteHeight, spriteWidth, spriteHeight);
                        if (!firewallCollisionHandler.CheckForWall(checkTile) && !firewallCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.Y += spriteHeight;
                            ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
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
            if ((isEnemy && itemState.state == FirewallStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == FirewallStateMachine.ItemStates.BeingUsed))
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
        }

        public void ChangeToUseable()
        {
            //player collects item
            itemState.ChangeToUseable();

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
            position = toolBarPosition;

        }
    }

    public class FirewallStateMachine
    {
        public enum ItemStates { Collectable, Useable, BeingUsed, Expended };
        public ItemStates state;
        public FirewallStateMachine()
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