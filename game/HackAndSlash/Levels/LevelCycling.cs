using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{


    public class LevelCycling
    {

        private const int LEVEL_SIZE = 6;

        // Field created jjust to fullfill sprint 3 scycling feature 
        public List<Map> S3EagleCycle; // this is only used for sprint 3 
        public Map[,] levelEagle;
        public Map[,] levelCrescent; // For future use 

        public Map[,] currentMapSet;
        public int[] currentLocationIndex = GlobalSettings.STRAT_UP_INDEX;

        public LevelCycling()
        {
            S3EagleCycle = new List<Map>()
            {
                new JsonParser(MapDatabase.eagleM1).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM2).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM3).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM4).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM5).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM6).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM7).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM8).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM9).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM10).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM11).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM12).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM13).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM14).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM15).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM16).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM17).getCurrentMapInfo()
            };

            string[,] EaglePaths = MapDatabase.eagle;
            levelEagle = new Map[LEVEL_SIZE, LEVEL_SIZE]; 
            for(int i = 0; i < LEVEL_SIZE; i++)
            {
                for (int j = 0; j < LEVEL_SIZE; j++)
                {
                    if (EaglePaths[i, j] != null)
                        levelEagle[i, j] = new JsonParser(EaglePaths[i, j]).getCurrentMapInfo();
                    else
                        levelEagle[i, j] = null;
                }
            }

            currentMapSet = levelEagle; // For initilization, set eagle as the default level 

        }
        
        public Map StartUpLevel()
        {
            return levelEagle[5, 2]; // Only works for level eagle 
        }

        // If that direction has a room 
        public bool HasNextRoom(int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    if (currentLocationIndex[0] <= 0) return false;
                    return (currentMapSet[currentLocationIndex[0] - 1, currentLocationIndex[1]] != null);

                case (int)GlobalSettings.Direction.Down:
                    if (currentLocationIndex[0] >= currentMapSet.GetLength(0) - 1) return false;
                    return (currentMapSet[currentLocationIndex[0] + 1, currentLocationIndex[1]] != null);

                case (int)GlobalSettings.Direction.Left:
                    if (currentLocationIndex[1] <= 0) return false;
                    return (currentMapSet[currentLocationIndex[0] - 1, currentLocationIndex[1]] != null);

                case (int)GlobalSettings.Direction.Right:
                    if (currentLocationIndex[1] >= currentMapSet.GetLength(1) - 1) return false;
                    return (currentMapSet[currentLocationIndex[1] + 1, currentLocationIndex[1]] != null);

                default:
                    return false;
            }
        }
        public Map GetNextRoom(int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    if (currentLocationIndex[0] <= 0) return null;
                    return (currentMapSet[currentLocationIndex[0] - 1, currentLocationIndex[1]]);

                case (int)GlobalSettings.Direction.Down:
                    if (currentLocationIndex[0] >= currentMapSet.GetLength(0) - 1) return null;
                    return (currentMapSet[currentLocationIndex[0] + 1, currentLocationIndex[1]]);

                case (int)GlobalSettings.Direction.Left:
                    if (currentLocationIndex[1] <= 0) return null;
                    return (currentMapSet[currentLocationIndex[0], currentLocationIndex[1] - 1]);

                case (int)GlobalSettings.Direction.Right:
                    if (currentLocationIndex[1] >= currentMapSet.GetLength(1) - 1) return null;
                    return (currentMapSet[currentLocationIndex[0], currentLocationIndex[1] + 1]);

                default:
                    return null;
            }
        }
        public Map NextMap(int Dir)
        {
            // Placeholder method 
            return S3EagleCycle[0];
        }

    }

}
