
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

        private FirewallStateMachine itemState;
        private static int numUses = 0;
        private const int USE_DURATION = 100; // length of effect
        private int useDurationCounter = 0; 
        private static int cooldown = 0; // item is useable if == 0 
        private const int ITEM_COOLDOWN = 30; // time in update cycles between uses 
        private const int MAX_RANGE = 5; // range in # of sprites(tiles) 
        private Vector2 toolBarPosition;

        private int playerDirection = 0;
        private Vector2 playerPosition;
        
        // Constructor
        public FirewallItem(Vector2 startPosition, SpriteBatch gameSpriteBatch)
        {
            position = startPosition;
            itemState = new FirewallStateMachine();
            firewallSprite = (ItemSprite)SpriteFactory.Instance.CreateFirewall();
            spriteWidth = firewallSprite.Texture.Width / firewallSprite.Columns;
            spriteHeight = firewallSprite.Texture.Height / firewallSprite.Rows;
            toolBarPosition = new Vector2(10, 10);
            spriteBatch = gameSpriteBatch;
        }

        public void Update()
        {
            switch (itemState.state)
            {
                case FirewallStateMachine.ItemStates.Collectable:
                    // check for collision collision -> collect Item
                    // if numUses != 0
                    firewallSprite.Update();
                    break;
                case FirewallStateMachine.ItemStates.Useable:
                    // check for uses
                    position = toolBarPosition;
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
                        
                        if (numUses > 0)
                        {
                            itemState.ChangeToUseable();
                        }
                        else
                        {
                            itemState.ChangeToExpended();
                        }
                    }
                    // slow down
                    if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0) {
                        switch (playerDirection)
                        {
                            case 0: // left
                                position.X -= spriteWidth;
                                break;
                            case 1: // right
                                position.X += spriteWidth;
                                break;
                            case 2: // up
                                position.Y -= spriteHeight;
                                break;
                            case 3: // down
                                position.Y += spriteHeight;
                                break;
                        }
                    }

                    break;
                case FirewallStateMachine.ItemStates.Expended:
                    // single instance is gone
                    position = toolBarPosition;
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
                    break;
                case FirewallStateMachine.ItemStates.BeingUsed:
                    // place over players head then draw wall with loop and updating position
                    firewallSprite.Draw(spriteBatch, position, Color.White);
                    // draw back to player starting position
                    switch (playerDirection)
                    {
                        case 0: // facing left
                            for(float i = position.X; i <= playerPosition.X; i += spriteWidth)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(i, playerPosition.Y), Color.White);
                            }
                            break;
                        case 1: // facing right
                            for (float i = position.X; i >= playerPosition.X; i -= spriteWidth)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(i, playerPosition.Y), Color.White);
                            }
                            break;
                        case 2: // facing Up
                            for (float i = position.Y; i <= playerPosition.Y; i += spriteHeight)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(playerPosition.X, i), Color.White);
                            }
                            break;
                        case 3: // facing down
                            for (float i = position.Y; i >= playerPosition.Y; i -= spriteHeight)
                            {
                                firewallSprite.Draw(spriteBatch, new Vector2(playerPosition.X, i), Color.White);
                            }
                            break;
                    }
                    break;
                case FirewallStateMachine.ItemStates.Expended:
                    // Gray out in toolbar if numUses == 0
                    if (numUses == 0)
                    {
                        firewallSprite.Draw(spriteBatch, position, Color.Gray);
                    }
                    break;
            }
        }

        public void CollectItem(IPlayer player)
        {
            itemState.ChangeToUseable();
            numUses++;
        }

        public void UseItem(int currentPlayerDirection, Vector2 currentPlayerPosition)
        {
            if (itemState.state == FirewallStateMachine.ItemStates.Useable && cooldown == 0)
            {
                switch (currentPlayerDirection)
                {
                    case 0:
                        currentPlayerPosition.X -= spriteWidth;
                        break;
                    case 1:
                        currentPlayerPosition.X += spriteWidth;
                        break;
                    case 2:
                        currentPlayerPosition.Y -= spriteHeight;
                        break;
                    case 3:
                        currentPlayerPosition.Y += spriteHeight;
                        break;
                }
                playerPosition = currentPlayerPosition; // player position when used TEMP: DEFAULT POS
                position = playerPosition;
                playerDirection = currentPlayerDirection;// player Direction
                itemState.ChangeToBeingUsed();
                cooldown = ITEM_COOLDOWN;
                numUses--;
                if (numUses < 0)
                {
                    numUses = 0;
                }
            }

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