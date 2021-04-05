using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class PauseOverlay
    {
        private Game1 Game;
        private Texture2D Overlay;
        private Texture2D SwordSelector;
        private Texture2D ItemSelector;
        private Texture2D InventoryText;
        private SpriteBatch spriteBatch;
        private Vector2 SelectorContinueLoc = new Vector2(GlobalSettings.PAUSE_CONTINUE_X, GlobalSettings.PAUSE_CONTINUE_Y);
        private Vector2 SelectorQuitLoc = new Vector2(GlobalSettings.PAUSE_QUIT_X, GlobalSettings.PAUSE_QUIT_Y);
        private Vector2 CurrentSelection;
        private int itemSelectorPos;
        private List<IItem> itemList;
        private int delayCounter;
        public PauseOverlay(Game1 game, Texture2D overlay, Texture2D swordSelector, Texture2D inventoryText, Texture2D itemSelector, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.Overlay = overlay;
            this.SwordSelector = swordSelector;
            this.spriteBatch = spriteBatch;
            this.InventoryText = inventoryText;
            this.ItemSelector = itemSelector;
            CurrentSelection = SelectorContinueLoc;
            itemSelectorPos = 0;
            itemList = game.useableItemList;
            delayCounter = 100;
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                CurrentSelection = SelectorQuitLoc;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || 
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
            {
                CurrentSelection = SelectorContinueLoc;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                if (CurrentSelection == SelectorContinueLoc)
                {
                    Game.elapsing = true;
                    Game.gamePaused = false;
                }
                else
                {
                    Game.Exit();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left) || 
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                if (delayCounter > GlobalSettings.DELAY_TIME)
                {
                    itemSelectorPos--;
                    delayCounter = 0;
                }
                if (itemSelectorPos < 0)
                {
                    itemSelectorPos = itemList.Count() - 1;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right) || 
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                if (delayCounter > GlobalSettings.DELAY_TIME)
                {
                    itemSelectorPos++;
                    delayCounter = 0;
                }
                if (itemSelectorPos > itemList.Count() - 1)
                {
                    itemSelectorPos = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1) && itemList.Count > 1 && delayCounter > GlobalSettings.DELAY_TIME)
            {
                IItem temp = itemList[itemSelectorPos];
                itemList[itemSelectorPos] = itemList[1];
                itemList[1] = itemList[0];
                itemList[0] = temp;
                delayCounter = 0;
            }
            foreach(IItem item in itemList)
            {
                item.Update();
            }
            delayCounter += 2;
        }

        public void Draw()
        {
            spriteBatch.Draw(Overlay, new Vector2(0, 0), Color.White);
            foreach (IItem item in Game.useableItemList)
            {
                item.SetToolbarPosition(itemList.IndexOf(item));
                item.Draw(); 
            }
            spriteBatch.Draw(InventoryText, new Vector2(4 * GlobalSettings.BASE_SCALAR, 76), Color.White);
            if (itemList.Count > 0) 
            {
                spriteBatch.Draw(ItemSelector, new Vector2((4 + itemSelectorPos) * GlobalSettings.BASE_SCALAR, 74), Color.White); 
            }
            spriteBatch.Draw(SwordSelector, CurrentSelection, Color.White);
        }
    }
}
