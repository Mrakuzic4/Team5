using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class FoodItem : IItem
    {
        private ItemSprite foodSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private Game1 game;
        private IPlayer player; //Player reference

        private FoodStateMachine itemState;
        private static int numUses = 0;
        private const int USE_DURATION = 20; // length of effect
        private int useDurationCounter = 0;

        private Vector2 toolBarPosition;

        public static bool inInventory = false;

        public Rectangle[] collidableTiles;
        public ItemCollisionHandler foodCollisionHandler;

        private TextSprite textSprites;

        public FoodItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            player = game.Player; //Reference of player from Game1

            position = startPosition;
            spriteBatch = gameSpriteBatch;
            toolBarPosition = new Vector2(4 * GlobalSettings.BASE_SCALAR, 0);
            itemState = new FoodStateMachine();
            itemState.ChangeToCollectable();
            foodSprite = (ItemSprite)SpriteFactory.Instance.CreateFood();
            spriteWidth = foodSprite.Texture.Width / foodSprite.Columns;
            spriteHeight = foodSprite.Texture.Height / foodSprite.Rows;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteHeight, spriteWidth);
            foodCollisionHandler = new ItemCollisionHandler(this.player);
            textSprites = (TextSprite)SpriteFactory.Instance.CreateTextCharacters(1);
        }
        public void Update()
        {
            switch (itemState.state)
            {
                case FoodStateMachine.ItemStates.Collectable:
                    // check for collision collision -> collect Item
                    if(foodCollisionHandler.CheckForPlayerCollision(collidableTiles))
                    {
                        game.levelCycleRecord.RemoveOneIndex(GlobalSettings.FOOD_ITEM);
                        CollectItem();
                    }
                    break;
                case FoodStateMachine.ItemStates.Useable:
                    // check for uses
                    position = toolBarPosition;
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteHeight, spriteWidth);
                    useDurationCounter = 0;
                    if (numUses < 0)
                    {
                        numUses = 0;
                        ChangeToExpended();
                    }
                    break;
                case FoodStateMachine.ItemStates.BeingUsed:
                    // check for player position, update position
                    // check number of uses left, update, if zero->Expended

                    useDurationCounter++;
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
                case FoodStateMachine.ItemStates.Expended:
                    // single instance is gone
                    position = toolBarPosition;
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteHeight, spriteWidth);
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
                case FoodStateMachine.ItemStates.Collectable:
                    // Draw on ground
                    foodSprite.Draw(spriteBatch, position, Color.White);
                    break;
                case FoodStateMachine.ItemStates.Useable:
                    // In bag Draw on toolbar
                    foodSprite.DrawOnBar(spriteBatch, position, Color.White);
                    // draw text with numUses
                    textSprites.Draw(spriteBatch, numUses.ToString(), new Vector2(toolBarPosition.X, toolBarPosition.Y + 64), Color.White);
                    break;
                case FoodStateMachine.ItemStates.BeingUsed:
                    // place over next to player then draw wall with loop and updating position
                    break;
                case FoodStateMachine.ItemStates.Expended:
                    // Gray out in toolbar if numUses == 0
                    if (numUses == 0)
                    {
                        foodSprite.Draw(spriteBatch, position, Color.Gray);
                    }
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
            SoundFactory.Instance.GetItemEffect();
            numUses++;
        }

        public void UseItem(GlobalSettings.Direction currentPlayerDirection)
        {
            if (itemState.state == FoodStateMachine.ItemStates.Useable)
            {
                player.Healed();
                SoundFactory.Instance.GetHeartEffect();
                numUses--;
                itemState.ChangeToExpended();

            }
        }

        public bool FogBreaker()
        {
            return (itemState.state == FoodStateMachine.ItemStates.Useable);
        }
        public Vector2 GetPos()
        {
            return position;
        }

        // returns collidableTiles for enemy damage or player collection
        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            Rectangle[] RectanglesList = { new Rectangle(0, 0, 1, 1) };
            if ((isEnemy && itemState.state == FoodStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == FoodStateMachine.ItemStates.Collectable))
                RectanglesList = collidableTiles;

            return RectanglesList;
        }

        public void SetToolbarPosition(int index)
        {
            toolBarPosition = new Vector2((index + 4) * GlobalSettings.BASE_SCALAR, 0);
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

        public class FoodStateMachine
        {
            public enum ItemStates { Collectable, Useable, BeingUsed, Expended };
            public ItemStates state;
            public FoodStateMachine()
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
