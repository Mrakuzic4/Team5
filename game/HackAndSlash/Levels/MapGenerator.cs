using System;
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

        public MapGenerator(Map MapInfo)
        {
            this.mapInfo = MapInfo;
        }

        // Not fully implemented 
        public List<ILevel> getLevelList(GraphicsDevice GD, SpriteBatch spriteBatch, Map map)
        {
            List <ILevel> levelList = new List<ILevel>();

            levelList.Add(new Level(GD, spriteBatch, map.Arrangement, map.DefaultBlock,
                map.OpenDoors, map.HiddenDoors, map.LockedDoors));

            return levelList;
        }

        public List<IBlock> GetBlockList(SpriteBatch spriteBatch, SpriteFactory spriteFactory)
        {
            List<IBlock> BlockList = new List<IBlock>(); 

            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    int Index = mapInfo.Arrangement[r, c];
                    if (Index == 32 || Index == 33)
                    {
                        BlockList.Add(new BlockInvis(new Vector2((c * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET),
                                    (r * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET + GlobalSettings.HEADSUP_DISPLAY)), spriteBatch));
                    }
                    else if (Index == 40)
                    {
                        BlockList.Add(spriteFactory.CreateBlockMovableVertical(spriteBatch, r, c));
                    }
                    else if (Index == 41)
                    {
                        BlockList.Add(spriteFactory.CreateBlockMovableHorizontal(spriteBatch, r, c));
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
                    int Index = mapInfo.Arrangement[r, c];
                    Vector2 position = new Vector2((c * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET),
                                    (r * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET));
                    switch (Index)
                    {
                        case -1:
                            EnemyList.Add(new SnakeEnemy(position, graphics, spriteBatch, game));
                            break;
                        case -2:
                            EnemyList.Add(new SnakeEnemy(position, graphics, spriteBatch, game));
                            break;
                        case -3:
                            EnemyList.Add(new SnakeEnemy(position, graphics, spriteBatch, game));
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

            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    int Index = mapInfo.Arrangement[r, c];
                    /*
                     If Index is smaller than -256, add an item into the list
                     */
                    Vector2 position = new Vector2((c * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET),
                            (r * GlobalSettings.BASE_SCALAR + GlobalSettings.BORDER_OFFSET));

                    switch (Index)
                    {
                        case -257:
                            ItemList.Add(new FirewallItem(position, spriteBatch, game));
                            break;
                        case -258:
                            ItemList.Add(new BombItem(position, spriteBatch, game));
                            break;
                        case -259:
                            ItemList.Add(new ThrowingKnifeItem(position, spriteBatch, game));
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
