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

        private SettingsOption[] optionsList = { new SettingsOption("ITEM RANGE", GlobalSettings.saveSets.ItemRange, 10, 5),
                    new SettingsOption("BOMB RADIUS", GlobalSettings.saveSets.BombRadius, 7, 10),
                    new SettingsOption("VISIBILITY RADUIS", GlobalSettings.saveSets.VisibilityRadius, 16, 5),
                    new SettingsOption("HEAL POWER", GlobalSettings.saveSets.HealPower, 10, 10),
                    new SettingsOption("MAX HEALTH", GlobalSettings.saveSets.MaxHealth, 16, 10) };

        private int[] positions;
        private SavedSettings settings = GlobalSettings.saveSets;
        private ISprite swordSelector;
        private GraphicsDevice graphics;
        private TextSprite text;
        private TextSprite bigText;
        private Texture2D bg = SpriteFactory.Instance.getGameWonScreen();

        public UpgradesOverlay(Game1 game, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            this.Game = game;
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            text = (TextSprite)SpriteFactory.Instance.CreateTextCharacters(3);
            bigText = (TextSprite)SpriteFactory.Instance.CreateTextCharacters(5);
            swordSelector = SpriteFactory.Instance.CreateSword(GlobalSettings.Direction.Right);
            positions = new int[8];
            for (int i = 0; i <= 7; i++)
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
                if (CurrentSelection.Y > positions[7])
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
                    CurrentSelection.Y = positions[7];
                }
                delayCounter = 0;
            }
            if (delayCounter >= 30 && (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)))
            {
                //If chose to continue
                for (int i = 0; i < 5; i++)
                {
                    if (CurrentSelection.Y == positions[i] && optionsList[i].CanBeUpgraded())
                    {
                        optionsList[i].Increment();
                        RupyItem.numUses -= optionsList[i].cost;
                    }
                }
                if (CurrentSelection.Y == positions[5])
                {
                    // set to default values
                    settings = new JsonParser(@"Content/info/NewSaveFile.json", JsonParser.ParseMode.settingsMode).getCurrentSavedSettings();
                    optionsList[0].settingValue = settings.ItemRange;
                    optionsList[1].settingValue = settings.BombRadius;
                    optionsList[2].settingValue = settings.VisibilityRadius;
                    optionsList[3].settingValue = settings.HealPower;
                    optionsList[4].settingValue = settings.MaxHealth;
                    // reset cost value based on new settings values
                    foreach (SettingsOption option in optionsList)
                    {
                        option.CalculateCost();
                        option.upgraded = false;
                        option.updateColor();
                    }
                }
                else
                {
                    // save data on game win (move to game state machines change to win method)
                    settings.ItemRange = optionsList[0].settingValue;
                    settings.BombRadius = optionsList[1].settingValue;
                    settings.VisibilityRadius = optionsList[2].settingValue;
                    settings.HealPower = optionsList[3].settingValue;
                    settings.MaxHealth = optionsList[4].settingValue;

                    GlobalSettings.saveSets = settings;
                    new JsonParser(SaveDatabase.saveFilePath, JsonParser.ParseMode.settingsMode).SaveToFile();
                    if (CurrentSelection.Y == positions[6])
                    {
                        Game.Exit();
                    }
                    
                    else if (CurrentSelection.Y == positions[7])
                    {

                        Game.reset(4);
                    }
                }

                    delayCounter = 0;

            }
            delayCounter++;

        }

        public void Draw()
        {
            //spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            bigText.Draw(spriteBatch, "UPGRADES MENU", new Vector2(0, 0), Color.White);
            for (int i = 0; i <= 4; i++)
            {
                text.Draw(spriteBatch, optionsList[i].PrintString(), new Vector2(64, positions[i]), optionsList[i].drawColor);
            }
            text.Draw(spriteBatch, "RESET SAVE FILE", new Vector2(64, positions[5]), Color.White);
            text.Draw(spriteBatch, "SAVE AND EXIT", new Vector2(64, positions[6]), Color.White);
            text.Draw(spriteBatch, "PLAY AGAIN", new Vector2(64, positions[7]), Color.White);
            swordSelector.Draw(spriteBatch, new Vector2(CurrentSelection.X, CurrentSelection.Y - 20), Color.White);
            Game.mainRupy.Draw();
        }
    }

    class SettingsOption
    {

        public int cost;
        public int settingLimit;
        public bool upgraded;
        public string name;
        public int settingValue;
        private int incAmount = 1;
        private int costMod;
        public Color drawColor = Color.White;

        public SettingsOption(string name, int value, int limit, int costMod)
        {
            this.name = name;
            upgraded = false;
            settingLimit = limit;
            settingValue = value;
            this.costMod = costMod;
            CalculateCost();
            updateColor();
            if (name == "MAX HEALTH")
            {
                incAmount = 2;
            }
        }

        public bool CanBeUpgraded()
        {
            if (!upgraded && settingValue < settingLimit && RupyItem.numUses >= cost)
            {
                upgraded = true;
                updateColor();
                return true;
            }
            return false;
        }

        public string PrintString()
        {
            return name + " " + (settingValue + incAmount) + " X " + cost;
        }

        public void Increment()
        {
            settingValue += incAmount;
            updateColor();
            CalculateCost();
        }

        public void CalculateCost()
        {
            cost = settingValue * costMod;
        }

        public void updateColor()
        {
            if (settingValue >= settingLimit)
            {
                drawColor = Color.Red;
            }
            else if (upgraded)
            {
                drawColor = Color.Gray;
            }
            else
            {
                drawColor = Color.White;
            }
        }
    }
}