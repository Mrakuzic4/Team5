using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class BombItem : IItem
    {
        private ItemSprite bombSprite, explosionSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private BombStateMachine itemState;
        private static int numUses = 0;
        private const int USE_DURATION = 80; // length of effect
        private int useDurationCounter = 0;
        private static int cooldown = 0; // item is useable if == 0 
        private const int ITEM_COOLDOWN = 30; // time in update cycles between uses
        private const int EXPLOSION_DELAY = 60;
        private const int NUM_BOMB_BLINKS = 4;
        private const int EXPLOSION_DIAMETER = 3;
        private Vector2 toolBarPosition;

        public Vector2[] collidableTiles;
        private enum animationState { blinkWhite, blinkRed, explode };
        private animationState bombAnimationState;

        public BombItem(Vector2 startPosition, SpriteBatch gameSpriteBatch)
        {
            position = startPosition;
            spriteBatch = gameSpriteBatch;
            toolBarPosition = new Vector2(20, 10);
            itemState = new BombStateMachine();
            itemState.ChangeToCollectable();
            bombSprite = (ItemSprite)SpriteFactory.Instance.CreateBomb();
            explosionSprite = (ItemSprite)SpriteFactory.Instance.CreateExplosion();
            spriteWidth = explosionSprite.Texture.Width / explosionSprite.Columns;
            spriteHeight = explosionSprite.Texture.Height / explosionSprite.Rows;
            collidableTiles = new Vector2[1];
            collidableTiles[0] = position;
        }
        public void Update()
        {
            switch (itemState.state)
            {
                case BombStateMachine.ItemStates.Collectable:
                    // check for collision collision -> collect Item
                    break;
                case BombStateMachine.ItemStates.Useable:
                    // check for uses
                    position = toolBarPosition;
                    collidableTiles = new Vector2[1];
                    collidableTiles[0] = position;
                    cooldown--;
                    if (cooldown <= 0)
                    {
                        cooldown = 0;
                    }
                    useDurationCounter = 0;
                    break;
                case BombStateMachine.ItemStates.BeingUsed:
                    // check for player position, update position
                    // check number of uses left, update, if zero->Expended

                    useDurationCounter++;
                    if (useDurationCounter < EXPLOSION_DELAY)
                    {
                        if (useDurationCounter  % (EXPLOSION_DELAY / NUM_BOMB_BLINKS) <= (EXPLOSION_DELAY / (2 * NUM_BOMB_BLINKS)))
                        {

                            bombAnimationState = animationState.blinkRed;
                        }
                        else
                        {
                            bombAnimationState = animationState.blinkWhite;
                        }
                    }
                    else if (useDurationCounter == EXPLOSION_DELAY)
                    {
                        // explode then gone
                        // move to top left corner of explosion
                        position = new Vector2(position.X - spriteWidth * ((EXPLOSION_DIAMETER - 1) / 2), position.Y - spriteHeight * ((EXPLOSION_DIAMETER - 1) / 2));
                        bombAnimationState = animationState.explode;
                    }
                    else if (useDurationCounter > EXPLOSION_DELAY) 
                    { 
                        explosionSprite.Update(); 
                    }
                    if (useDurationCounter >= USE_DURATION)
                    {
                        useDurationCounter = 0;
                        position = toolBarPosition;
                        if (numUses > 0)
                        {
                            itemState.ChangeToUseable();
                            
                        }
                        else
                        {
                            itemState.ChangeToExpended();
                        }
                    }
                    break;
                case BombStateMachine.ItemStates.Expended:
                    // single instance is gone
                    position = toolBarPosition;
                    collidableTiles = new Vector2[1];
                    collidableTiles[0] = position;
                    break;
            }
        }

        public void Draw()
        {
            switch (itemState.state)
            {
                case BombStateMachine.ItemStates.Collectable:
                    // Draw on ground
                    bombSprite.Draw(spriteBatch, position, Color.White);
                    break;
                case BombStateMachine.ItemStates.Useable:
                    // In bag Draw on toolbar
                    bombSprite.Draw(spriteBatch, position, Color.White);
                    // draw text with numUses
                    break;
                case BombStateMachine.ItemStates.BeingUsed:
                    // place over next to player then draw wall with loop and updating position
                    switch (bombAnimationState)
                    {
                        case animationState.blinkWhite:
                            bombSprite.Draw(spriteBatch, position, Color.White);
                            break;
                        case animationState.blinkRed:
                            bombSprite.Draw(spriteBatch, position, Color.Red);
                            break;
                        case animationState.explode:
                            // draw full explosion
                            collidableTiles = new Vector2[EXPLOSION_DIAMETER * EXPLOSION_DIAMETER];
                            int c = 0;
                            for (float i = position.X; i < position.X + EXPLOSION_DIAMETER * spriteWidth; i += spriteWidth)
                            {
                                for(float j = position.Y; j < position.Y + EXPLOSION_DIAMETER * spriteHeight; j += spriteHeight)
                                {
                                    Vector2 tempPosition = new Vector2(i, j);
                                    explosionSprite.Draw(spriteBatch, tempPosition, Color.White);
                                    collidableTiles[c] = tempPosition;
                                    c++;
                                }
                            }


                            break;
                    }
                    break;
                case BombStateMachine.ItemStates.Expended:
                    // Gray out in toolbar if numUses == 0
                    if (numUses == 0)
                    {
                        bombSprite.Draw(spriteBatch, position, Color.Gray);
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
            if (itemState.state == BombStateMachine.ItemStates.Useable && cooldown == 0)
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
                position = currentPlayerPosition; // player Direction
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

        public class BombStateMachine
        {
            public enum ItemStates { Collectable, Useable, BeingUsed, Expended };
            public ItemStates state;
            public BombStateMachine()
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
}
