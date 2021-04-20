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
        private int[] costs;
        private int[] settingLimits = { 16, 10, 16, 10, 16 };
        private bool[] upgraded = { false, false, false, false, false, false, false };
        private SavedSettings settings = GlobalSettings.saveSets;
        private int[] settingValues;
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
            swordSelector = SpriteFactory.Instance.CreateSword(GlobalSettings.Direction.Right);
            positions = new int[7];
            for( int i = 0; i <= 6; i++)
            {
                positions[i] = (2 + i) * 64;
            }
            int[] Values = { settings.ItemRange, settings.BombRadius, settings.VisibilityRadius, settings.HealPower, settings.MaxHealth };
            settingValues = Values;
            costs = new int[5];
            costs[0] = GlobalSettings.saveSets.ItemRange * 5;
            costs[1] = GlobalSettings.saveSets.BombRadius * 10;
            costs[2] = GlobalSettings.saveSets.VisibilityRadius * 5;
            costs[3] = GlobalSettings.saveSets.HealPower * 10;
            costs[4] = GlobalSettings.saveSets.MaxHealth * 10;
            CurrentSelection = new Vector2(0, positions[0]);
            delayCounter = 0;
        }
        public void Update()
        {
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown)))
            {
                CurrentSelection.Y += optionHeight;
                if(CurrentSelection.Y > positions[6])
                {
                    CurrentSelection.Y = positions[0];
                }
                delayCounter = 0;
            }
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp)))
            {
                CurrentSelection.Y -= optionHeight;
                if (CurrentSelection.Y < positions[0])
                {
                    CurrentSelection.Y = positions[6];
                }
                delayCounter = 0;
            }
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)))
            {
                //If chose to continue
                for(int i = 0; i < 5; i++)
                {
                    if (CurrentSelection.Y == positions[i] && !upgraded[i] && settingValues[i] < settingLimits[i] && RupyItem.numUses > costs[i])
                    {
                        settingValues[i]++;
                        // Max health upgrades by 2 each time
                        if(i == 4)
                        {
                            settingValues[i]++;
                        }
                        RupyItem.numUses -= costs[i];
                        upgraded[i] = true;
                    }
                }
                if (CurrentSelection.Y == positions[5])
                {
                    // set to default values
                    settingValues[0] = 3;
                    settingValues[1] = 3;
                    settingValues[2] = 2;
                    settingValues[3] = 1;
                    settingValues[4] = 6;
                    // reset cost value based on new settings values
                    costs[0] = GlobalSettings.saveSets.ItemRange * 5;
                    costs[1] = GlobalSettings.saveSets.BombRadius * 10;
                    costs[2] = GlobalSettings.saveSets.VisibilityRadius * 5;
                    costs[3] = GlobalSettings.saveSets.HealPower * 10;
                    costs[4] = GlobalSettings.saveSets.MaxHealth * 5;
                    upgraded[5] = true;
                    Game.reset(4);
                }
                else if (CurrentSelection.Y == positions[6])
                {
                    // save data on game win (move to game state machines change to win method)
                    settings.ItemRange = settingValues[0];
                    settings.BombRadius = settingValues[1];
                    settings.VisibilityRadius = settingValues[2];
                    settings.HealPower = settingValues[3];
                    settings.MaxHealth = settingValues[4];

                    GlobalSettings.saveSets = settings;
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
            for (int i = 0; i <= 4; i++)
            {
                string printString = options[i] + " " + (settingValues[i] + 1) + " X " + costs[i];
                if (i == 4)
                {
                    printString = options[i] + " " + (settingValues[i] + 2) + " X " + costs[i];
                }
                text.Draw(spriteBatch, printString, new Vector2(64, positions[i]), upgraded[i] ? Color.Gray : Color.White);
            }
            for (int i = 5; i <= 6; i++)
            {
                text.Draw(spriteBatch, options[i], new Vector2(64, positions[i]), upgraded[i] ? Color.Gray : Color.White);
            }
            swordSelector.Draw(spriteBatch, new Vector2(CurrentSelection.X, CurrentSelection.Y - 20), Color.White);
            Game.mainRupy.Draw();
            
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
