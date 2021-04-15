using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Generate or modify a single room 
/// </summary>
namespace HackAndSlash
{
    class GenerateRoom
    {
        public Map room { get; set; }
        public double enemyCount = 4;

        private const int ENEMY_SPAWN_BIAS = 3; 

        public int defaultBlock { get; set; }
        public int dropPotions { get; set; }
        public int dropKeys { get; set; }

        public int[] enemyList { get; set; }
        public int[] walkableBlockList { get; set; }
        public int[] solidBlockLIst { get; set; }
        public int[] itemList { get; set; }

        public int bossIndex { get; set; }

        private static bool[,] canPlace = new bool[,] {
            { true, true, true, true, true, false, false, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { false, true, true, true, true, true, true, true, true, true, true, false},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, false, false, true, true, true, true, true}
        };

        private static bool[,] cross = new bool[,] {
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false}
        };

        private static bool[,] allFalse = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
        };

        public GenerateRoom()
        {
            // Setup the template 
            defaultBlock = 0;
            dropKeys = 0;
            dropPotions = 0;

            UseStyleOne();
        }

        /// <summary>
        /// Fill the map info with placeholders, all disabled by default; 
        /// </summary>
        /// <returns></returns>
        public Map InitRoom()
        {
            Map template = new Map();

            template.DefaultBlock = defaultBlock;

            template.Arrangement = new int[GlobalSettings.TILE_ROW, GlobalSettings.TILE_COLUMN];
            for (int i = 0; i < template.Arrangement.GetLength(0); i++)
                for (int j = 0; j < template.Arrangement.GetLength(1); j++)
                    template.Arrangement[i, j] = defaultBlock;

            template.DropKeys = dropKeys;
            template.DropPotions = dropPotions;

            
            template.HiddenDoors = new bool[] { false, false, false, false };
            template.LockedDoors = new bool[] { false, false, false, false };
            template.MysteryDoors = new bool[] { false, false, false, false };
            template.OpenDoors = new bool[] { false, false, false, false };
            template.Holes = new bool[] { false, false, false, false };

            room = template; 

            return template;
        }

        public void PopulateEnemy()
        {
            int Count = 0;
            int Threshold = (int)(enemyCount / GlobalSettings.TILE_ROW * GlobalSettings.TILE_COLUMN) + ENEMY_SPAWN_BIAS; 

            for (int i = 0; i < room.Arrangement.GetLength(0); i++) {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++) {
                    if(GlobalSettings.RND.Next(100) < Threshold && !IsBlock(i, j) && Count < enemyCount) {
                        room.Arrangement[i, j] = enemyList[GlobalSettings.RND.Next(enemyList.Length)];
                        Count++; 
                    }
                }
            }

        }

        public void PopulateBlock()
        {
            // Experimental feature 
            bool[,] Rand = RandScatter(0.4);
            MaskOffDoorways(Rand);
            PopulatePattern(Rand, 2);


            Rand = RandScatter(0.2);
            MaskOffDoorways(Rand);
            PopulatePattern(Rand, solidBlockLIst[GlobalSettings.RND.Next(solidBlockLIst.Length)]);
        }

        public void PopulateItem()
        {

        }

        private bool IsBlock(int row, int col)
        {
            return (room.Arrangement[row, col] > 0); 
        }

        private void PopulatePattern(bool[,] Pattern, int Index)
        {
            for (int i = 0; i < room.Arrangement.GetLength(0); i++){
                for (int j = 0; j < room.Arrangement.GetLength(1); j++) {
                    if (canPlace[i, j] && Pattern[i, j]) {
                        room.Arrangement[i, j] = Index;
                    }
                }
            }
        }

        private void MaskOffDoorways(bool [,] Target)
        {
            for (int i = 0; i < room.Arrangement.GetLength(0); i++) {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++) {
                    Target[i, j] = canPlace[i, j] && Target[i, j];
                }
            }
        }

        private bool[,] RandScatter(double Density)
        {
            int Threshold = (int)(GlobalSettings.TILE_COLUMN * GlobalSettings.TILE_ROW * Density) * 100;
            Threshold /= (GlobalSettings.TILE_COLUMN * GlobalSettings.TILE_ROW); 
            bool[,] scatter = new bool[room.Arrangement.GetLength(0), room.Arrangement.GetLength(1)];

            for (int i = 0; i < room.Arrangement.GetLength(0); i++) {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++) {
                    if (GlobalSettings.RND.Next(100) < Threshold) {
                        scatter[i, j] = true;
                    }
                    else {
                        scatter[i, j] = false;
                    }
                }
            }

            return scatter; 
        }

        public void UseStyleOne()
        {
            enemyList = new int[]{
                GlobalSettings.SNAKE_ENEMY,
                GlobalSettings.BUG_ENEMY,
                GlobalSettings.MOBLIN_ENEMY
            };
            walkableBlockList = new int[]{0, 1, 2};
            solidBlockLIst = new int[] {32, 33, 35, 36, 37, 38 };

            bossIndex = GlobalSettings.BOSS_ENEMY; 


        }

        
    }

}
