using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    class MapGenerator
    {
        Map mapInfo; 

        public MapGenerator(Map MapInfo)
        {
            this.mapInfo = MapInfo;
        }

        public List<IBlock> GetBlockList()
        {
            List<IBlock> BlockList = new List<IBlock>(); 

            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    int Index = mapInfo.Arrangement[r, c];
                    /*
                     If Index is bigger or equal to 32, add a static block into the list
                     */
                }
            }

            return BlockList;
        }

        public List<IEnemy> GetEnemyList()
        {
             List<IEnemy> EnemyList = new List<IEnemy>();

            for (int r = 0; r < GlobalSettings.TILE_ROW; r++)
            {
                for (int c = 0; c < GlobalSettings.TILE_COLUMN; c++)
                {
                    int Index = mapInfo.Arrangement[r, c];
                    /*
                     If Index is between -1 to -256, add an enemy into the list
                     */
                    Vector2 position = new Vector2(r * GlobalSettings.BASE_SCALAR, c * GlobalSettings.BASE_SCALAR);
                }
            }

            return EnemyList;
        }

        public List<IItem> GetItemList()
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
                    Vector2 position = new Vector2(r * GlobalSettings.BASE_SCALAR, c * GlobalSettings.BASE_SCALAR);

                }
            }

            return ItemList;
        }

    }
}
