
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class FlyingSwordItem : IItem
    {
        
        private Game1 game;
        private IPlayer player; //Player reference

        private ItemSprite flyingSwordSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        private FlyingSwordStateMachine itemState;
        private int useDurationCounter = 0;
        private const int MAX_RANGE = 5; // range in # of sprites(tiles)
        private const int USE_DURATION = MAX_RANGE * 20; // length of effect
        private const int DIST = 20;
        private const int SPEED = 5;
        private const int MIDDLE = 30;
        private const int SWORD_TIP = 20; //the hitbox of the sword would be a rectangle instead of a square, this is the parameter for the narrower part of the rectangle.

        public static bool inInventory = false;

        public Rectangle[] collidableTiles;
        public ItemCollisionHandler throwingKnifeCollisionHandler;

        private GlobalSettings.Direction playerDirection = GlobalSettings.Direction.Right;
        private Vector2 playerPosition;

        // Constructor
        public FlyingSwordItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            this.player = this.game.Player; //Reference of player from Game1

            position = startPosition;
            itemState = new FlyingSwordStateMachine();
            flyingSwordSprite = (ItemSprite)SpriteFactory.Instance.CreateSword(GlobalSettings.Direction.Right);
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            throwingKnifeCollisionHandler = new ItemCollisionHandler(this.player);
        }
        private Rectangle getHitBox()
        {
            Rectangle playerSwordHitBox;
 
            switch (game.Player.GetDir())
            {
                //width and height set of the sword hitbox set to Base_SCALAR, Base_SCALAR/2
                case GlobalSettings.Direction.Right:
                    //Player's sword hitbox is located at the hand of the player
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X + GlobalSettings.BASE_SCALAR, (int)game.Player.GetPos().Y + MIDDLE, GlobalSettings.BASE_SCALAR, SWORD_TIP);
                    break;
                case GlobalSettings.Direction.Left:
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X - GlobalSettings.BASE_SCALAR, (int)game.Player.GetPos().Y + MIDDLE, GlobalSettings.BASE_SCALAR, SWORD_TIP);
                    break;
                case GlobalSettings.Direction.Up:
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X + MIDDLE, (int)game.Player.GetPos().Y - GlobalSettings.BASE_SCALAR, SWORD_TIP, GlobalSettings.BASE_SCALAR);
                    break;
                default:
                    playerSwordHitBox = new Rectangle((int)game.Player.GetPos().X + MIDDLE, (int)game.Player.GetPos().Y + GlobalSettings.BASE_SCALAR, SWORD_TIP, GlobalSettings.BASE_SCALAR);
                    break;
            }
            return playerSwordHitBox;
        }

        public void Update()
        {
            for(int i = 0; i < SPEED; i++) 
            {
                switch (itemState.state)
                {
                    case FlyingSwordStateMachine.ItemStates.Collectable:
                        // check for collision collision -> collect Item
                        // if numUses != 0
                        if (throwingKnifeCollisionHandler.CheckForPlayerCollision(collidableTiles))
                        {
                            CollectItem();
                        }
                        flyingSwordSprite.Update();
                        break;
                    case FlyingSwordStateMachine.ItemStates.Useable:
                        // check for uses
                        collidableTiles = new Rectangle[1];
                        collidableTiles[0] = getHitBox();
                        break;
                    case FlyingSwordStateMachine.ItemStates.BeingUsed:
                        // check for player position, update position
                        // check number of uses left, update, if zero->Expended
                        flyingSwordSprite.Update();
                        useDurationCounter++;
                        if (useDurationCounter >= USE_DURATION)
                        {
                            useDurationCounter = 0;

                            ChangeToUseable();

                        }
                        if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0)
                        {
                            Rectangle checkTile = getHitBox();
                            List<IBlock> blockList = game.blockList;
                            switch (playerDirection)
                            {
                                case GlobalSettings.Direction.Left: // left
                                    if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.X -= DIST;
                                    }
                                    else
                                    {
                                        ChangeToUseable();
                                    }
                                    break;
                                case GlobalSettings.Direction.Right: // right
                                    if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.X += DIST;
                                    }
                                    else
                                    {
                                        ChangeToUseable();
                                    }
                                    break;
                                case GlobalSettings.Direction.Up: // up
                                    if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.Y -= DIST;
                                    }
                                    else
                                    {
                                        ChangeToUseable();
                                    }
                                    break;
                                case GlobalSettings.Direction.Down: // down
                                    if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                                    {
                                        position.Y += DIST;
                                    }
                                    else
                                    {
                                        ChangeToUseable();
                                    }
                                    break;
                            }

                        }
                        break;
                    case FlyingSwordStateMachine.ItemStates.Expended:
                        collidableTiles = new Rectangle[1];
                        collidableTiles[0] = getHitBox();
                        break;
                }
            }
        }

        public void Draw()
        {
            switch (itemState.state)
            {
                case FlyingSwordStateMachine.ItemStates.Collectable:
                    // Draw on ground
                    flyingSwordSprite.Draw(spriteBatch, position, Color.White);
                    break;
                case FlyingSwordStateMachine.ItemStates.Useable:
                    // In bag Draw on toolbar
                    flyingSwordSprite.Draw(spriteBatch, new Vector2(-GlobalSettings.BASE_SCALAR, -GlobalSettings.BASE_SCALAR), Color.White);
                    break;
                case FlyingSwordStateMachine.ItemStates.BeingUsed:
                    // place over players head then draw wall with loop and updating position

                    int width = flyingSwordSprite.Texture.Width;
                    int height = flyingSwordSprite.Texture.Height;
                    Rectangle sourceRec = new Rectangle(0, 0, width, height);
                    Rectangle destinationRec = new Rectangle((int)position.X,(int)position.Y,GlobalSettings.BASE_SCALAR,GlobalSettings.BASE_SCALAR );
                    spriteBatch.Draw(flyingSwordSprite.Texture,destinationRec,sourceRec, Color.White,0f,Vector2.Zero,SpriteEffects.None,0.1f);

                    // draw back to player starting position
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = getHitBox();
                    break;
                case FlyingSwordStateMachine.ItemStates.Expended:
                    // Gray out in toolbar
                    flyingSwordSprite.Draw(spriteBatch, position, Color.Gray);
                    collidableTiles = new Rectangle[1];
                    collidableTiles[0] = getHitBox();
                    break;
            }
        }

        public void CollectItem()
        {
            ChangeToUseable();
        }

        public void UseItem(GlobalSettings.Direction currentPlayerDirection)
        {
            Vector2 currentPlayerPosition = this.player.GetPos();
            if (itemState.state == FlyingSwordStateMachine.ItemStates.Useable)
            {
                Rectangle checkTile = getHitBox();
                List<IBlock> blockList = game.blockList;
                flyingSwordSprite = (ItemSprite)SpriteFactory.Instance.CreateSword(currentPlayerDirection);
                switch (currentPlayerDirection)
                {
                    case GlobalSettings.Direction.Left: // left
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.X -= spriteWidth;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Right: // right
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.X += spriteWidth;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Up: // up
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.Y -= spriteHeight;
                            itemState.ChangeToBeingUsed();
                        }
                        else
                        {
                            ChangeToUseable();
                        }
                        break;
                    case GlobalSettings.Direction.Down: // down
                        if (!throwingKnifeCollisionHandler.CheckForWall(checkTile) && !throwingKnifeCollisionHandler.CheckForBlock(checkTile, blockList))
                        {
                            currentPlayerPosition.Y += spriteHeight;
                            itemState.ChangeToBeingUsed();
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
                SoundFactory.Instance.ThrowingKnifeEffect();
            }

        }

        public bool FogBreaker()
        {
            return (itemState.state == FlyingSwordStateMachine.ItemStates.Useable
                || itemState.state == FlyingSwordStateMachine.ItemStates.BeingUsed);
        }
        public Vector2 GetPos()
        {
            return position;
        }

        // returns collidableTiles for enemy damage or player collection
        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            Rectangle[] RectanglesList = { new Rectangle(0, 0, 1, 1) };
            if ((isEnemy && itemState.state == FlyingSwordStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == FlyingSwordStateMachine.ItemStates.BeingUsed))
                RectanglesList = collidableTiles;

            return RectanglesList;
        }

        public void SetToolbarPosition(int index)
        {
        }

        public void SetMax()
        {

        }


        public void ChangeToCollectable()
        {
            //player drops item
            itemState.ChangeToCollectable();
            flyingSwordSprite = (ItemSprite)SpriteFactory.Instance.CreateSword(GlobalSettings.Direction.Up);

        }

        public void ChangeToUseable()
        {
            //player collects item
            itemState.ChangeToUseable();
            position = new Vector2(-64, -64);
            flyingSwordSprite = (ItemSprite)SpriteFactory.Instance.CreateSword(GlobalSettings.Direction.Up);
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
            flyingSwordSprite = (ItemSprite)SpriteFactory.Instance.CreateSword(GlobalSettings.Direction.Up);
        }
    }

    public class FlyingSwordStateMachine
    {
        public enum ItemStates { Collectable, Useable, BeingUsed, Expended };
        public ItemStates state;
        public FlyingSwordStateMachine()
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