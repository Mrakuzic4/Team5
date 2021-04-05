
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class BurningFire : IItem
    {
        private SpriteBatch spriteBatch;

        private ItemSprite fireSprite;
        public Vector2 position;

        private Color defaultTint = Color.White; 

        public BurningFire(Vector2 startPosition, SpriteBatch gameSpriteBatch)
        {
            position = startPosition;
            spriteBatch = gameSpriteBatch;
            fireSprite = (ItemSprite)SpriteFactory.Instance.CreateFirewall();
        }

        public void Update()
        {
            fireSprite.Update();
        }

        public void Draw()
        {
            fireSprite.Draw(spriteBatch, position, defaultTint); 
        }


        public void CollectItem()
        {

        }

        public void UseItem(GlobalSettings.Direction currentPlayerDirection)
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

        public void SetToolbarPosition(int index)
        {

        }

        public Rectangle[] getCollidableTiles(bool isEnemy)
        {
            return new Rectangle[] { new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR) };
        }

    }
}