﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{
    class MapGenerator
    {
        Map mapInfo;
        GlobalSettings mapSettings;

        public MapGenerator(Map MapInfo)
        {
            this.mapInfo = MapInfo;
            mapSettings = new GlobalSettings();
        }

        public Level getLevel(GraphicsDevice GD, SpriteBatch spriteBatch)
        {
            Level level = new Level(GD, spriteBatch);

            return level;
        }

        public List<IBlock> GetBlockList(SpriteBatch spriteBatch, SpriteFactory spriteFactory, Map MapInfo)
        {
            List<IBlock> BlockList = new List<IBlock>();

            int TopPosition = GlobalSettings.HEADSUP_DISPLAY + GlobalSettings.BASE_SCALAR;
            int ButtPosition = GlobalSettings.WINDOW_HEIGHT - 2 * GlobalSettings.BASE_SCALAR;
            int LeftPosition = GlobalSettings.BASE_SCALAR;
            int RightPosition = GlobalSettings.WINDOW_WIDTH - 2 * GlobalSettings.BASE_SCALAR;
            int HorizontalPos, VerticalPos = 0;

            Vector2 LeftDoorBlocking = new Vector2(1 * GlobalSettings.BASE_SCALAR, 
                GlobalSettings.HEADSUP_DISPLAY + 5 * GlobalSettings.BASE_SCALAR);
            Vector2 RightDoorBlocking = new Vector2(GlobalSettings.WINDOW_WIDTH - 2 * GlobalSettings.BASE_SCALAR, 
                GlobalSettings.HEADSUP_DISPLAY + 5 * GlobalSettings.BASE_SCALAR);
            Vector2 TopDoorBlocking = new Vector2((float)7.5 * GlobalSettings.BASE_SCALAR,
                3 * GlobalSettings.BASE_SCALAR);
            Vector2 BottomDoorBlocking = new Vector2((float)7.5 * GlobalSettings.BASE_SCALAR,
                GlobalSettings.WINDOW_HEIGHT - 2 * GlobalSettings.BASE_SCALAR);

            // Stuck a block if that direction has no entry  
            for (int i = 0; i < MapInfo.OpenDoors.Length; i++) {
                bool CouldPass = (MapInfo.HiddenDoors[i] || MapInfo.LockedDoors[i] || MapInfo.OpenDoors[i]);

                if (!CouldPass) {
                    switch (i)
                    {
                        case (int)GlobalSettings.Direction.Up:
                            BlockList.Add(new BlockInvis(TopDoorBlocking, spriteBatch));
                            break;
                        case (int)GlobalSettings.Direction.Down:
                            BlockList.Add(new BlockInvis(BottomDoorBlocking, spriteBatch));
                            break;
                        case (int)GlobalSettings.Direction.Right:
                            BlockList.Add(new BlockInvis(RightDoorBlocking, spriteBatch));
                            break;
                        case (int)GlobalSettings.Direction.Left:
                            BlockList.Add(new BlockInvis(LeftDoorBlocking, spriteBatch));
                            break;
                        default:
                            break; 
                    }
                } 
            }

            // The following 2 for loops are for the creation of walls (in lieu of boundary check)
            for (int i = 0; i < GlobalSettings.TILE_COLUMN; i++)
            {   
                // All resulting magic numbers are to avoid player
                // from stuck in between and cannot plass through doors 
                if (i < 6)
                    HorizontalPos = (int)((i + 1.25) * GlobalSettings.BASE_SCALAR);  
                else
                    HorizontalPos = (int)((i + 2.75) * GlobalSettings.BASE_SCALAR);
                
                BlockList.Add(new BlockInvis(new Vector2(HorizontalPos, TopPosition), spriteBatch));
                BlockList.Add(new BlockInvis(new Vector2(HorizontalPos, ButtPosition), spriteBatch));
            }
            for (int i = 0; i < GlobalSettings.TILE_ROW; i++)
            {
                if (i < 3)
                    VerticalPos = GlobalSettings.HEADSUP_DISPLAY + (int)((i +2.25) * GlobalSettings.BASE_SCALAR);
                if (i > 3)
                    VerticalPos = GlobalSettings.HEADSUP_DISPLAY + (int)((i + 2.15) * GlobalSettings.BASE_SCALAR);
                
                BlockList.Add(new BlockInvis(new Vector2(LeftPosition, VerticalPos), spriteBatch));
                BlockList.Add(new BlockInvis(new Vector2(RightPosition, VerticalPos), spriteBatch));
            }   


            // The following loop is for blocks in the main gameplay aera 
            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    int Index = mapInfo.Arrangement[r, c];
                    
                    if (Index == 40)
                    {
                        BlockList.Add(spriteFactory.CreateBlockMovableVertical(spriteBatch, r, c));
                    }
                    else if (Index == 41)
                    {
                        BlockList.Add(spriteFactory.CreateBlockMovableHorizontal(spriteBatch, r, c));
                    }
                    if (Index >= 32 && (Index!=40 && Index != 41))
                    {
                        BlockList.Add(new BlockInvis(mapSettings.PlayAreaPosition(c, r), spriteBatch));
                    }
                }
            }

            return BlockList;
        }

        public List<IEnemy> GetEnemyList(SpriteBatch spriteBatch, GraphicsDevice graphics, Game1 game)
        {
             List<IEnemy> EnemyList = new List<IEnemy>();

            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    /*
                     If Index is in between -1 and -255, add an enemy into the list
                     */
                    int Index = mapInfo.Arrangement[r, c];
                    Vector2 position = mapSettings.PlayAreaPosition(c, r);
                    switch (Index)
                    {
                        case GlobalSettings.SNAKE_ENEMY:
                            EnemyList.Add(new SnakeEnemy(position, graphics, spriteBatch, game));
                            break;
                        case GlobalSettings.BUG_ENEMY:
                            EnemyList.Add(new BugEnemy(position, graphics, spriteBatch, game));
                            break;
                        case GlobalSettings.MOBLIN_ENEMY:
                            EnemyList.Add(new MoblinEnemy(position, graphics, spriteBatch, game));
                            break;
                        default:
                            break; 
                    }
                }
            }

            return EnemyList;
        }

        public List<IItem> GetItemList(SpriteBatch spriteBatch, Game1 game)
        {
            List<IItem> ItemList = new List<IItem>();
            int itemNum = 0;
            bool hasFirewall = false;
            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    int Index = mapInfo.Arrangement[r, c];
                    /*
                     If Index is smaller than -256, add an item into the list
                     */
                    Vector2 position = mapSettings.PlayAreaPosition(c, r);

                    switch (Index)
                    {
                        case GlobalSettings.FIREWALL_ITEM:
                            ItemList.Add(new FirewallItem(position, spriteBatch, game));
                            itemNum++;
                            break;
                        case GlobalSettings.BOMB_ITEM:
                            ItemList.Add(new BombItem(position, spriteBatch, game));
                            itemNum++;
                            break;
                        case GlobalSettings.THROWING_KNIFE_ITEM:
                            ItemList.Add(new ThrowingKnifeItem(position, spriteBatch, game));
                            itemNum++;
                            break;
                        case GlobalSettings.FOOD_ITEM:
                            ItemList.Add(new FoodItem(position, spriteBatch, game));
                            itemNum++;
                            break;
                        case GlobalSettings.TRIFORCE_ITEM:
                            ItemList.Add(new TriforceItem(position, spriteBatch, game));
                            break;
                        default:
                            break;
                    }
                }
            }

            return ItemList;
        }

    }
}
