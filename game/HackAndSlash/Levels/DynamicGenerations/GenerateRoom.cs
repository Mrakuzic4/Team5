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

        private const int ENEMY_MAX = 8;
        private const int ENEMY_SPAWN_BIAS = 3;
        private const int PATTERNED_BIAS = -10;
        private const int ITEM_TOLERANCE = 6;
        private const int ITEM_MAX = 4;
        private const double WALKABLE_SPREAD_MAX = 0.6;
        private const double SOLID_SPREAD_MAX = 0.2;

        private double walkableSpread;
        private double solidSpread;
        private int distFromStartup;
        private double rowProgression;
        private double colProgression;

        // In regards to the Map class
        public int defaultBlock { get; set; }
        public int dropPotions { get; set; }
        public int dropKeys { get; set; }

        // Related to the style and possible things that appears in the rooms
        public int blackRoomInedx { get; set; }
        public int[] enemyList { get; set; }
        public int[] walkableBlockList { get; set; }
        public int[] solidBlockLIst { get; set; }
        public int[] itemList { get; set; }
        public int[] merchantItems { get; set; }
        public int[] merchantCharaList { get; set; }
        public int bossIndex { get; set; }


        // Room matrices 
        private static bool[,] canPlace = new bool[,] {
            { true, true, true, true, true, false, false, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { false, true, true, true, true, true, true, true, true, true, true, false},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, false, false, true, true, true, true, true}
        };

        private static bool[,] corners = new bool[,] {
            { true, false, false, false, false, false, false, false, false, false, false, true},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { true, false, false, false, false, false, false, false, false, false, false, true}
        };

        private static bool[,] midOval = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, true, true, true, true, false, false, false, false},
            { false, false, false, false, true, true, true, true, false, false, false, false},
            { false, false, false, false, true, true, true, true, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
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

        private static bool[,] cornerDust = new bool[,] {
            { true, true, true, false, false, false, false, false, false, true, true, true},
            { true, true, false, false, false, false, false, false, false, false, true, true},
            { true, false, false, false, false, false, false, false, false, false, false, true},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { true, false, false, false, false, false, false, false, false, false, false, true},
            { true, true, false, false, false, false, false, false, false, false, true, true},
            { true, true, true, false, false, false, false, false, false, true, true, true}
        };

        private static bool[,] grid = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, false, true, false, true, true, false, true, false, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, false, true, false, true, true, false, true, false, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, false, true, false, true, true, false, true, false, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
        };

        private static bool[,] square = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, true, true, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
        };

        private static bool[,] doubleRectangles = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, true, false, false, false, false, false, false, true, true, false},
            { false, true, true, false, false, false, false, false, false, true, true, false},
            { false, true, true, false, false, false, false, false, false, true, true, false},
            { false, true, true, false, false, false, false, false, false, true, true, false},
            { false, true, true, false, false, false, false, false, false, true, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
        };

        private static bool[,] maze = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true , false, true , true , true , false, true , true , true , true , false},
            { false, true , false, false, false, true , false, true , false, false, false, false},
            { false, true , false, true , false, true , true , true , false, true , true , false},
            { false, true , false, true , false, true , false, true , false, false, false, false},
            { false, true , true , true , false, true , false, true , true , true , true , false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
        };

        private static bool[,] pipe = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, true, true, true, true, true, true, true, true, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, true, true, true, true, true, true, true, true, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, true, true, true, true, true, true, true, true, true, true, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
        };

        private static bool[,] merchantRoom = new bool[,] {
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, true , false, false, true , false, false, true , false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false},
            { false, false, false, false, false, false, false, false, false, false, false, false}
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

            UseStyleOne(); // Maybe later moved into other parts 
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

        public void SetPara(int dist, double rowProg, double colProg)
        {
            distFromStartup = dist;
            rowProgression = rowProg;
            colProgression = colProg;
        }

        public void PopulateEnemy()
        {
            // Amount of enemy in this room 
            int enemyCount = (int)(ENEMY_MAX * (
                (double)distFromStartup / Math.Pow(room.Arrangement.GetLength(0), 1)));

            int Count = 0;
            int Threshold = (int)(enemyCount / GlobalSettings.TILE_ROW * GlobalSettings.TILE_COLUMN) + ENEMY_SPAWN_BIAS;

            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    if (GlobalSettings.RND.Next(100) < Threshold && !IsBlock(i, j)
                        && Count < enemyCount && canPlace[i, j])
                    {
                        room.Arrangement[i, j] = enemyList[GlobalSettings.RND.Next(enemyList.Length)];
                        Count++;
                    }
                }
            }

        }

        public void PopulateBlock()
        {
            int walkablePatternedRate;
            int solidPatternedRate;
            bool[,] Rand;

            List<bool[,]> WalkableList = new List<bool[,]>();
            List<bool[,]> SolidList = new List<bool[,]>();
            WalkableList.Add(cross);
            WalkableList.Add(corners);
            WalkableList.Add(midOval);
            SolidList.Add(grid);
            SolidList.Add(corners);
            SolidList.Add(doubleRectangles);
            SolidList.Add(square);
            SolidList.Add(cornerDust);
            SolidList.Add(pipe);
            SolidList.Add(maze);

            walkableSpread = Math.Min(
                (double)distFromStartup / (double)Math.Pow(room.Arrangement.GetLength(0), 2),
                WALKABLE_SPREAD_MAX);
            solidSpread = Math.Min(
                (double)distFromStartup / (double)Math.Pow(room.Arrangement.GetLength(0), 2),
                SOLID_SPREAD_MAX);

            walkablePatternedRate = (int)(100 * (1 - walkableSpread / WALKABLE_SPREAD_MAX)) - PATTERNED_BIAS;
            solidPatternedRate = (int)(100 * (1 - solidSpread / SOLID_SPREAD_MAX)) - PATTERNED_BIAS;


            if (GlobalSettings.RND.Next(100) < walkablePatternedRate)
            {
                Rand = WalkableList[GlobalSettings.RND.Next(WalkableList.Count)];
            }
            else
            {
                Rand = RandScatter(walkableSpread);
                MaskOffDoorways(Rand);
            }
            PopulatePattern(Rand, walkableBlockList[GlobalSettings.RND.Next(walkableBlockList.Length)]);


            if (GlobalSettings.RND.Next(100) < solidPatternedRate)
            {
                Rand = SolidList[GlobalSettings.RND.Next(SolidList.Count)];
            }
            else
            {
                Rand = RandScatter(solidSpread);
                MaskOffDoorways(Rand);
            }
            PopulatePattern(Rand, solidBlockLIst[GlobalSettings.RND.Next(solidBlockLIst.Length)]);
        }

        public void PopulateItem()
        {
            int TotalitemNow = Math.Max(ITEM_MAX - distFromStartup, 0);
            int Threshold = (int)(100 * ((double)TotalitemNow / room.Arrangement.Length));
            int ItemCount = 0;

            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    if (distFromStartup == 0)
                    {
                        if (corners[i, j])
                            room.Arrangement[i, j] = itemList[GlobalSettings.RND.Next(itemList.Length)];
                    }
                    else if (GlobalSettings.RND.Next(100) < Threshold
                        && ItemCount < ITEM_MAX && !IsBlock(i, j))
                    {
                        room.Arrangement[i, j] = itemList[GlobalSettings.RND.Next(itemList.Length)];
                        ItemCount += 1;
                    }
                }
            }
        }

        public void SetAsMerchantRoom()
        {
            room.DefaultBlock = blackRoomInedx;

            int RowMid = 2;
            int ColMid = 6;

            FloodMap(blackRoomInedx);

            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    if (merchantRoom[i, j])
                    {
                        room.Arrangement[i, j] = merchantItems[GlobalSettings.RND.Next(merchantItems.Length)];
                    }
                }
            }

            room.Arrangement[RowMid, ColMid] = merchantCharaList[
                GlobalSettings.RND.Next(merchantCharaList.Length)];
        }

        public bool AddSoftIndex(int[] Position, int Index)
        {
            if (IsBlock(Position[0], Position[1]))
                return false;
            else
            {
                room.Arrangement[Position[0], Position[1]] = Index;
                return true;
            }
        }

        private bool IsBlock(int row, int col)
        {
            return (room.Arrangement[row, col] > 0);
        }

        private bool IsItem(int row, int col)
        {
            return (room.Arrangement[row, col] < -256);
        }

        private void PopulatePattern(bool[,] Pattern, int Index)
        {
            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    if (Pattern[i, j])
                    {
                        room.Arrangement[i, j] = Index;
                    }
                }
            }
        }

        private void MaskOffDoorways(bool[,] Target)
        {
            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    Target[i, j] = canPlace[i, j] && Target[i, j];
                }
            }
        }

        private bool[,] RandScatter(double Density)
        {
            int Threshold = (int)(GlobalSettings.TILE_COLUMN * GlobalSettings.TILE_ROW * Density) * 100;
            Threshold /= (GlobalSettings.TILE_COLUMN * GlobalSettings.TILE_ROW);
            bool[,] scatter = new bool[room.Arrangement.GetLength(0), room.Arrangement.GetLength(1)];

            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    if (GlobalSettings.RND.Next(100) < Threshold)
                    {
                        scatter[i, j] = true;
                    }
                    else
                    {
                        scatter[i, j] = false;
                    }
                }
            }

            return scatter;
        }

        private void FloodMap(int Index)
        {
            for (int i = 0; i < room.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < room.Arrangement.GetLength(1); j++)
                {
                    room.Arrangement[i, j] = Index;
                }
            }
        }

        public void UseStyleOne()
        {
            enemyList = new int[]{
                GlobalSettings.SNAKE_ENEMY,
                GlobalSettings.BUG_ENEMY,
                GlobalSettings.MOBLIN_ENEMY
            };
            walkableBlockList = new int[] { 2 };
            solidBlockLIst = new int[] { 32, 33, 35, 36, 37, 38 };
            blackRoomInedx = 1;
            itemList = new int[] {
                GlobalSettings.FIREWALL_ITEM,
                GlobalSettings.BOMB_ITEM,
                GlobalSettings.THROWING_KNIFE_ITEM,
                GlobalSettings.FOOD_ITEM,
            };
            merchantItems = new int[] {
                GlobalSettings.REFILL_ITEM,
                GlobalSettings.HEART_ITEM,
                GlobalSettings.SHIELD_ITEM
            };
            merchantCharaList = new int[] {
                GlobalSettings.NPC_OLD_MAN,
                GlobalSettings.NPC_OLD_WOMAN
            };


            bossIndex = GlobalSettings.BOSS_ENEMY;


        }


    }

}