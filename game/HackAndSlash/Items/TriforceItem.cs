
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
    public class TriforceItem : IItem
    {
        
        private Game1 game;
        private IPlayer player; //Player reference

        private ItemSprite triforceSprite;
        public int spriteWidth, spriteHeight;
        public Vector2 position;

        private SpriteBatch spriteBatch;

        public static bool inInventory = false;

        public Rectangle[] collidableTiles;
        public ItemCollisionHandler TriforceCollisionHandler;

        // Constructor
        public TriforceItem(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            this.player = this.game.Player; //Reference of player from Game1

            position = startPosition;
            triforceSprite = (ItemSprite)SpriteFactory.Instance.CreateTriforce();
            spriteWidth = triforceSprite.Texture.Width / triforceSprite.Columns;
            spriteHeight = triforceSprite.Texture.Height / triforceSprite.Rows;
            spriteBatch = gameSpriteBatch;
            collidableTiles = new Rectangle[1];
            collidableTiles[0] = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            TriforceCollisionHandler = new ItemCollisionHandler(this.player);
        }

        public void Update()
        {
            // check for collision collision -> collect Item
            // if numUses != 0
            if (TriforceCollisionHandler.CheckForPlayerCollision(collidableTiles))
            {
                CollectItem();
            }
            triforceSprite.Update();
        }

        public void Draw()
        {
            // Draw on ground
            triforceSprite.Draw(spriteBatch, position, Color.White);
        }

        public void CollectItem()
        {
            // Game Win State

            SoundFactory.Instance.TriforceObtainedEffect();
            ChangeToExpended();
            game.GameState = GlobalSettings.GameStates.GameWon;
            game.currentLevel.setGameWon();
            GlobalSettings.NIGHTMAREMODE = false;
            this.game.gameWon = true;
            this.game.inGameWonAnimation = true;
            //this.game.gameOver = true;
            //this.game.inGameOverAnimation = true;
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