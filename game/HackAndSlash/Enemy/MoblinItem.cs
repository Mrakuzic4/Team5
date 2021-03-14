
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    //public class MoblinItem : IItem
    //{
    //    /*
    //     * Copy Throwing knife to new item class
    //     * Reskin with new sprites iand add to database
    //     * have player check if they are colliding with an enmemy item by using getCollidableTiles(true)
    //     * enemy will call use item with their direction
    //     * change collectable and expended to usable states
    //     * make a new IItem in game (or maybe in moblin) so it can appear with player items
    //     * 
    //     */

    //    private Game1 game;
    //    private IPlayer player; //Player reference

    //    private ItemSprite bombSprite;
    //    public int spriteWidth, spriteHeight;
    //    public Vector2 position;

    //    private SpriteBatch spriteBatch;

    //    private MoblinBombStateMachine itemState;
    //    //private static int numUses = 0;
    //    private const int USE_DURATION = 100; // length of effect
    //    private int useDurationCounter = 0;
    //    // private static int cooldown = 0; // item is useable if == 0 
    //    // private const int ITEM_COOLDOWN = 30; // time in update cycles between uses 
    //    private const int MAX_RANGE = 5; // range in # of sprites(tiles) 
    //    // private Vector2 toolBarPosition;

    //    public Rectangle[] collidableTiles;
    //    public ItemCollisionHandler moblinBombCollisionHandler;

    //    private GlobalSettings.Direction playerDirection = GlobalSettings.Direction.Right;
    //    private Vector2 playerPosition;

    //    // Constructor
    //    public MoblinItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
    //    {
    //        this.game = game;
    //        this.player = this.game.Player; //Reference of player from Game1

    //        position = startPosition;
    //        itemState = new MoblinBombStateMachine();
    //        bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
    //        spriteWidth = bombSprite.Texture.Width / bombSprite.Columns;
    //        spriteHeight = bombSprite.Texture.Height / bombSprite.Rows;
    //        //toolBarPosition = new Vector2(64, 0);
    //        spriteBatch = gameSpriteBatch;
    //        collidableTiles = new Rectangle[1];
    //        collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
    //        moblinBombCollisionHandler = new ItemCollisionHandler(this.player);
    //    }

    //    public void Update()
    //    {
    //        switch (itemState.state)
    //        {
    //            case MoblinBombStateMachine.ItemStates.BeingUsed:
    //                // check for player position, update position
    //                // check number of uses left, update, if zero->Expended
    //                bombSprite.Update();
    //                useDurationCounter++;
    //                if (useDurationCounter >= USE_DURATION)
    //                {
    //                    useDurationCounter = 0;

    //                    if (numUses > 0)
    //                    {
    //                        ChangeToUseable();
    //                    }
    //                    else
    //                    {
    //                        ChangeToExpended();
    //                    }
    //                }
    //                // TODO: Check for collisions that shorten total length (walls)
    //                if (useDurationCounter % (USE_DURATION / MAX_RANGE) == 0)
    //                {
    //                    Rectangle checkTile;
    //                    List<IBlock> blockList = game.blockList;
    //                    switch (playerDirection)
    //                    {
    //                        case GlobalSettings.Direction.Left: // left
    //                            checkTile = new Rectangle((int)position.X - spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
    //                            if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                            {
    //                                position.X -= spriteWidth;
    //                            }
    //                            else
    //                            {
    //                                ChangeToExpended();
    //                            }
    //                            break;
    //                        case GlobalSettings.Direction.Right: // right
    //                            checkTile = new Rectangle((int)position.X + spriteWidth, (int)position.Y, spriteWidth, spriteHeight);
    //                            if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                            {
    //                                position.X += spriteWidth;
    //                            }
    //                            else
    //                            {
    //                                ChangeToExpended();
    //                            }
    //                            break;
    //                        case GlobalSettings.Direction.Up: // up
    //                            checkTile = new Rectangle((int)position.X, (int)position.Y - spriteHeight, spriteWidth, spriteHeight);
    //                            if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                            {
    //                                position.Y -= spriteHeight;
    //                            }
    //                            else
    //                            {
    //                                ChangeToExpended();
    //                            }
    //                            break;
    //                        case GlobalSettings.Direction.Down: // down
    //                            checkTile = new Rectangle((int)position.X, (int)position.Y + spriteHeight, spriteWidth, spriteHeight);
    //                            if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                            {
    //                                position.Y += spriteHeight;
    //                            }
    //                            else
    //                            {
    //                                ChangeToExpended();
    //                            }
    //                            break;
    //                    }
    //                }

    //                break;
    //            case MoblinBombStateMachine.ItemStates.Expended:
    //                collidableTiles = new Rectangle[1];
    //                collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
    //                break;
    //        }
    //    }

    //    public void Draw()
    //    {
    //        switch (itemState.state)
    //        {
    //            case MoblinBombStateMachine.ItemStates.BeingUsed:
    //                // place over players head then draw wall with loop and updating position
    //                bombSprite.Draw(spriteBatch, position, Color.White);
    //                // draw back to player starting position
    //                collidableTiles = new Rectangle[1];
    //                collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
    //                break;
    //            case MoblinBombStateMachine.ItemStates.Expended:
    //                // Gray out in toolbar
    //                bombSprite.Draw(spriteBatch, position, Color.Gray);
    //                collidableTiles = new Rectangle[1];
    //                collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
    //                break;
    //        }
    //    }

    //    public void UseItem(GlobalSettings.Direction currentPlayerDirection)
    //    {
    //        Vector2 currentPlayerPosition = this.player.GetPos();
    //        if (itemState.state == MoblinBombStateMachine.ItemStates.Useable)
    //        {
    //            Rectangle checkTile;
    //            List<IBlock> blockList = game.blockList;
    //            switch (currentPlayerDirection)
    //            {
    //                case GlobalSettings.Direction.Left: // left
    //                    checkTile = new Rectangle((int)currentPlayerPosition.X - spriteWidth, (int)currentPlayerPosition.Y, spriteWidth, spriteHeight);
    //                    if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                    {
    //                        currentPlayerPosition.X -= spriteWidth;
    //                        itemState.ChangeToBeingUsed();
    //                    }
    //                    else
    //                    {
    //                        itemState.ChangeToUseable();
    //                    }
    //                    break;
    //                case GlobalSettings.Direction.Right: // right
    //                    checkTile = new Rectangle((int)currentPlayerPosition.X + spriteWidth, (int)currentPlayerPosition.Y, spriteWidth, spriteHeight);
    //                    if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                    {
    //                        currentPlayerPosition.X += spriteWidth;
    //                        itemState.ChangeToBeingUsed();
    //                    }
    //                    else
    //                    {
    //                        itemState.ChangeToUseable();
    //                    }
    //                    break;
    //                case GlobalSettings.Direction.Up: // up
    //                    checkTile = new Rectangle((int)currentPlayerPosition.X, (int)currentPlayerPosition.Y - spriteHeight, spriteWidth, spriteHeight);
    //                    if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                    {
    //                        currentPlayerPosition.Y -= spriteHeight;
    //                        itemState.ChangeToBeingUsed();
    //                    }
    //                    else
    //                    {
    //                        itemState.ChangeToUseable();
    //                    }
    //                    break;
    //                case GlobalSettings.Direction.Down: // down
    //                    checkTile = new Rectangle((int)currentPlayerPosition.X, (int)currentPlayerPosition.Y + spriteHeight, spriteWidth, spriteHeight);
    //                    if (!moblinBombCollisionHandler.CheckForWall(checkTile) && !moblinBombCollisionHandler.CheckForBlock(checkTile, blockList))
    //                    {
    //                        currentPlayerPosition.Y += spriteHeight;
    //                        itemState.ChangeToBeingUsed();
    //                    }
    //                    else
    //                    {
    //                        itemState.ChangeToUseable();
    //                    }
    //                    break;
    //            }
    //            bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
    //            playerPosition = currentPlayerPosition; // player position when used TEMP: DEFAULT POS
    //            position = playerPosition;
    //            playerDirection = currentPlayerDirection;// player Direction
    //            // itemState.ChangeToBeingUsed();
    //            cooldown = ITEM_COOLDOWN;
    //            numUses--;
    //            if (numUses < 0)
    //            {
    //                numUses = 0;
    //            }
    //        }

    //    }

    //    // returns collidableTiles for enemy damage or player collection
    //    public Rectangle[] getCollidableTiles(bool isEnemy)
    //    {
    //        Rectangle[] RectanglesList = { new Rectangle(0, 0, 1, 1) };
    //        if ((isEnemy && itemState.state == MoblinBombStateMachine.ItemStates.BeingUsed) || (!isEnemy && itemState.state == MoblinBombStateMachine.ItemStates.BeingUsed))
    //            RectanglesList = collidableTiles;

    //        return RectanglesList;
    //    }

    //    public void ChangeToBeingUsed()
    //    {
    //        //player is using item
    //        itemState.ChangeToBeingUsed();
    //        bombSprite = (ItemSprite)SpriteFactory.Instance.CreateEnemyBomb();
    //    }

    //    public void ChangeToExpended()
    //    {
    //        //player used this instance of the item
    //        itemState.ChangeToExpended();
    //    }

    //    public void ChangeToCollectable()
    //    {
    //    }

    //    public void ChangeToUseable()
    //    {
    //    }

    //    public void CollectItem()
    //    {
    //    }
    //}

    //public class MoblinBombStateMachine
    //{
    //    public enum ItemStates { BeingUsed, Expended };
    //    public ItemStates state;
    //    public MoblinBombStateMachine()
    //    {
    //        state = ItemStates.Expended;
    //    }

    //    public void ChangeToBeingUsed()
    //    {
    //        //player is using item
    //        state = ItemStates.BeingUsed;
    //    }

    //    public void ChangeToExpended()
    //    {
    //        //player used this instance of the item
    //        state = ItemStates.Expended;
    //    }
    //}
}