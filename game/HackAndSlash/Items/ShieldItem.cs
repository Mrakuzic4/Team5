
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HackAndSlash
{
    public class ShieldItem : IItem
    {
        
        private Game1 game;
        private IPlayer player; //Player reference

        private ItemSprite shieldSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        public static bool inInventory = false;

        public Rectangle[] collidableTiles;
        public ItemCollisionHandler TriforceCollisionHandler;
        private const float UNDERLAYER = 0.4f;

        // Constructor
        public ShieldItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            this.player = this.game.Player; //Reference of player from Game1

            position = startPosition;
            shieldSprite = (ItemSprite)SpriteFactory.Instance.CreateShield();
            spriteWidth = shieldSprite.Texture.Width / shieldSprite.Columns;
            spriteHeight = shieldSprite.Texture.Height / shieldSprite.Rows;
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            TriforceCollisionHandler = new ItemCollisionHandler(this.player);
        }

        public void Update()
        {
            // check for collision collision -> collect Item
            if (TriforceCollisionHandler.CheckForPlayerCollision(collidableTiles))
            {
                CollectItem();
            }
            shieldSprite.Update();
        }

        public void Draw()
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, shieldSprite.Texture.Width, shieldSprite.Texture.Height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, shieldSprite.Texture.Width, shieldSprite.Texture.Height);

            spriteBatch.Draw(shieldSprite.Texture, destinationRectangle, sourceRectangle, Color.White,
                0f, Vector2.Zero, SpriteEffects.None, UNDERLAYER);
        }
        /// <summary>
        /// Increase the Shield Point.
        /// </summary>
        public void CollectItem()
        {
            if (RupyItem.numUses >= 100)
            {
                RupyItem.numUses -= 100;
                this.game.Player.ShieldUp();
                //Remove the item, make sure the player only collects once!
                ChangeToExpended();
                collidableTiles[0] = new Rectangle(0, 0, 0, 0);

                SoundFactory.Instance.TriforceObtainedEffect();
                SpriteFactory.Instance.SetZeldaGotShield();
                DrawPlayer.Instance.Item = true; //Adjust the player sprite's position
                this.game.Player = new UseItemPlayer(game.Player, game);
            }
        }

        public void UseItem(GlobalSettings.Direction currentPlayerDirection)
        {

        }

        public bool FogBreaker()
        {
            return inInventory;
        }
        public Vector2 GetPos()
        {
            return position;
        }

        // returns collidableTiles for enemy damage or player collection
        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            Rectangle[] RectanglesList = collidableTiles;
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

        }

        public void ChangeToUseable()
        {

        }

        public void ChangeToBeingUsed()
        {

        }

        public void ChangeToExpended()
        {
            position = new Vector2(-64, 64);
        }
    }
}