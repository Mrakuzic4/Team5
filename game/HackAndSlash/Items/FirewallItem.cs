using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class FirewallItem : IItem
    {
        private ItemSprite firewallSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;
        private GraphicsDevice Graphics;

        private FirewallStateMachine itemState;
        private static int numUses = 0;
        private const int USE_DURATION = 30; // length of effect
        private int useDurationCounter = 0; 
        private static int cooldown = 0; // item is useable if == 0
        private const int ITEM_COOLDOWN = 30; // time in update cycles between uses
        private const int MAX_RANGE = 5; // range in # of sprites(tiles

        private int playerDirection = 0;
        private Vector2 playerPosition;
        
        // Constructor
        public FirewallItem(Vector2 startPosition, GraphicsDevice graphics)
        {
            position = startPosition;
            itemState = new FirewallStateMachine();
            Graphics = graphics;
            firewallSprite = (ItemSprite)SpriteFactory.Instance.CreateFirewall();
            spriteWidth = firewallSprite.Texture.Width / firewallSprite.Columns;
            spriteHeight = firewallSprite.Texture.Height / firewallSprite.Rows;

        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Graphics);

        }

        public void Update()
        {
            ;
            switch (itemState.state)
            {
                case FirewallStateMachine.ItemStates.Collectable:
                    // check for collision collision -> collect Item
                    // if numUses != 0
                    firewallSprite.Update();
                    break;
                case FirewallStateMachine.ItemStates.Useable:
                    // check for uses
                    cooldown--;
                    if (cooldown <= 0)
                    {
                        cooldown = 0;
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
                        numUses--;
                        
                        if (numUses > 0)
                        {
                            itemState.changeToUseable();
                        }
                        else
                        {
                            itemState.changeToExpended();
                        }
                    }
                    // slow down
                    switch (playerDirection)
                    {
                        case 0: // left
                            position.X -= USE_DURATION / (MAX_RANGE * spriteWidth);
                            break;
                        case 1: // right
                            position.X += USE_DURATION / (MAX_RANGE * spriteWidth);
                            break;
                        case 2: // up
                            position.Y -= USE_DURATION / (MAX_RANGE * spriteHeight);
                            break;
                        case 3: // down
                            position.Y += USE_DURATION / (MAX_RANGE * spriteHeight);
                            break;
                    }

                    break;
                case FirewallStateMachine.ItemStates.Expended:
                    // single instance is gone
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
                    break;
                case FirewallStateMachine.ItemStates.BeingUsed:
                    // place over players head then draw wall with loop and updating position
                    firewallSprite.Draw(spriteBatch, position, Color.White);
                    // draw back to player starting position
                    switch (playerDirection)
                    {
                        case 0: // facing left
                            for(float i = position.X; i > playerPosition.X; i += spriteWidth)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(i, playerPosition.Y), Color.White);
                            }
                            break;
                        case 1: // facing right
                            for (float i = position.X; i < playerPosition.X; i -= spriteWidth)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(i, playerPosition.Y), Color.White);
                            }
                            break;
                        case 3: // facing Up
                            for (float i = position.Y; i > playerPosition.Y; i += spriteWidth)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(playerPosition.X, i), Color.White);
                            }
                            break;
                        case 4: // facing down
                            for (float i = position.Y; i < playerPosition.Y; i -= spriteWidth)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(playerPosition.X, i), Color.White);
                            }
                            break;
                    }
                    break;
                case FirewallStateMachine.ItemStates.Expended:
                    // Gray out in toolbar if numUses == 0
                    break;
            }
        }

        void collectItem(IPlayer player)
        {
            itemState.changeToUseable();
            position = new Vector2(0,0);
            numUses++;
        }

        void useItem(int currentPlayerDirection, Vector2 currentPlayerPosition)
        {
            if (itemState.state == FirewallStateMachine.ItemStates.Useable && cooldown == 0)
            {
                playerPosition = currentPlayerPosition; // player position when used TEMP: DEFAULT POS
                position = playerPosition;
                playerDirection = currentPlayerDirection;// player Direction
                itemState.changeToBeingUsed();
                cooldown = ITEM_COOLDOWN;
            }

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

        public void changeToCollectable()
        {
            //player drops item
            state = ItemStates.Collectable;
        }

        public void changeToUseable()
        {
            //player collects item
            state = ItemStates.Useable;
        }

        public void changeToBeingUsed()
        {
            //player is using item
            state = ItemStates.BeingUsed;
        }

        public void changeToExpended()
        {
            //player used this instance of the item
            state = ItemStates.Expended;
        }
    }
}