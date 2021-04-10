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
    class UpgradesOverlay
    {
        private Game1 Game;
        private SpriteBatch spriteBatch;
        private Vector2 CurrentSelection;
        private int delayCounter;
        private int optionHeight = 64;
        private string[] options = { "ITEM RANGE", "BOMB RADIUS", "VISIBILITY RADIUS", "HEAL POWER", "MAXIMUM HEALTH", "RESET SAVE FILE", "EXIT" };
        private int[] positions;
        private bool[] upgraded = { false, false, false, false, false, false, false };
        ISprite swordSelector;
        GraphicsDevice graphics;
        TextSprite text;
        TextSprite bigText;

        public UpgradesOverlay(Game1 game, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            text = (TextSprite)SpriteFactory.Instance.CreateTextCharacters(3);
            bigText = (TextSprite)SpriteFactory.Instance.CreateTextCharacters(5);
            swordSelector = SpriteFactory.Instance.CreateThrowingKnife(GlobalSettings.Direction.Right);
            positions = new int[7];
            for( int i = 0; i <= 6; i++)
            {
                positions[i] = (2 + i) * 64;
            }
            CurrentSelection = new Vector2(0, positions[0]);
            delayCounter = 0;
        }
        public void Update()
        {
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown)))
            {
                CurrentSelection.Y += optionHeight;
                delayCounter = 0;
            }
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp)))
            {
                CurrentSelection.Y -= optionHeight;
                delayCounter = 0;
            }
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)))
            {
                //If chose to continue
                if (CurrentSelection.Y == positions[0] && !upgraded[0] && GlobalSettings.saveSets.MaxHealth < 16)
                {
                    GlobalSettings.saveSets.ItemRange++;
                    upgraded[0] = true;
                }
                else if (CurrentSelection.Y == positions[1] && !upgraded[1] && GlobalSettings.saveSets.ItemRange < 10)
                {
                    GlobalSettings.saveSets.BombRadius++;
                    upgraded[1] = true;
                }
                else if (CurrentSelection.Y == positions[2] && !upgraded[2] && GlobalSettings.saveSets.HealPower < 16)
                {
                    GlobalSettings.saveSets.VisibilityRadius++;
                    upgraded[2] = true;
                }
                else if (CurrentSelection.Y == positions[3] && !upgraded[3] && GlobalSettings.saveSets.VisibilityRadius < 10)
                {
                    GlobalSettings.saveSets.HealPower++;
                    upgraded[3] = true;
                }
                else if (CurrentSelection.Y == positions[4] && !upgraded[4] && GlobalSettings.saveSets.BombRadius < 6)
                {
                    GlobalSettings.saveSets.MaxHealth += 2;
                    upgraded[4] = true;
                }
                else if (CurrentSelection.Y == positions[5])
                {
                    GlobalSettings.saveSets.ItemRange = 3;
                    GlobalSettings.saveSets.BombRadius = 3;
                    GlobalSettings.saveSets.VisibilityRadius = 2;
                    GlobalSettings.saveSets.HealPower = 1;
                    GlobalSettings.saveSets.MaxHealth = 6;
                    upgraded[5] = true;
                }
                else
                {
                    // save data on game win (move to game state machines change to win method)
                    new JsonParser(SaveDatabase.saveFilePath, JsonParser.ParseMode.settingsMode).SaveToFile();
                    Game.Exit();
                }
                delayCounter = 0;

            }
            delayCounter++;

        }

        public void Draw()
        { 
            spriteBatch.Draw(GenerateTexture(GlobalSettings.WINDOW_WIDTH, GlobalSettings.WINDOW_HEIGHT, pixel => Color.Black), new Vector2(0, 0), Color.White);
            bigText.Draw(spriteBatch, "UPGRADES MENU", new Vector2(0, 0), Color.White);
            for (int i = 0; i <= 6; i++)
            {
                text.Draw(spriteBatch, options[i], new Vector2(64, positions[i]), upgraded[i] ? Color.Gray : Color.White);
            }
            
            swordSelector.Draw(spriteBatch, new Vector2(CurrentSelection.X, CurrentSelection.Y - 20), Color.White);
            
        }

        public Texture2D GenerateTexture(int width, int height, Func<int, Color> paint)
        {
            Texture2D texture = new Texture2D(graphics, width, height);

            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
                data[pixel] = paint(pixel);

            texture.SetData(data);
            return texture;
        }
    }
}
