
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
    public class Price : IItem
    {
        private Game1 game;
        private IPlayer player; //Player reference

        private Texture2D shieldSprite;

        public Vector2 position;

        private SpriteBatch spriteBatch;

        public static bool inInventory = false;
        public Rectangle[] collidableTiles;

        private const float UNDERLAYER = 0.4f;
        private const int COUNT_WORD = 2;

        // Constructor
        public Price(Vector2 startPosition, SpriteBatch gameSpriteBatch, Game1 game)
        {
            this.game = game;
            this.player = this.game.Player; //Reference of player from Game1

            position = startPosition;
            shieldSprite = SpriteFactory.Instance.CreatePrice();
            spriteBatch = gameSpriteBatch;

        }

        public void Update()
        {
            
        }

        public void Draw()
        {

            Rectangle sourceRectangle = new Rectangle(0, 0, shieldSprite.Width, shieldSprite.Height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR* COUNT_WORD, GlobalSettings.BASE_SCALAR);

            spriteBatch.Draw(shieldSprite, destinationRectangle, sourceRectangle, Color.White,
                0f, Vector2.Zero, SpriteEffects.None, UNDERLAYER);
        }

        public void CollectItem()
        {

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
            collidableTiles[0] = new Rectangle(0, 0, 0, 0);
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
        }
    }
}