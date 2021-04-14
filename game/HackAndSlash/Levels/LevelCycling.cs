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
        public bool useS4 = true; // May alterd in game1.cs

        private const int LEVEL_EAGLE_SIZE = 6;

        // Field created jjust to fullfill sprint 3 scycling feature 
        public Map[,] levelEagle;
        public Map[,] levelCrescent; // For future use 

        private GenerateLevel levelRNG; 

        public Map[,] currentMapSet;
        public int[] currentLocationIndex;

        public LevelCycling(bool UseS4)
        {
            useS4 = UseS4; 

            currentLocationIndex = new int[] { GlobalSettings.STRAT_UP_INDEX[0], GlobalSettings.STRAT_UP_INDEX[1] };

            string[,] EaglePaths = MapDatabase.eagle;
            levelEagle = new Map[LEVEL_EAGLE_SIZE, LEVEL_EAGLE_SIZE]; 
            for(int i = 0; i < LEVEL_EAGLE_SIZE; i++)
            {
                for (int j = 0; j < LEVEL_EAGLE_SIZE; j++)
                {
                    if (EaglePaths[i, j] != null)
                        levelEagle[i, j] = new JsonParser(EaglePaths[i, j], JsonParser.ParseMode.mapMode).getCurrentMapInfo();
                    else
                        levelEagle[i, j] = null;
                }
            }

            if (useS4)
                currentMapSet = levelEagle; // For initilization, set eagle as the default level 
            else
            {
                levelRNG = new GenerateLevel();
                currentMapSet = levelRNG.GenerateLevelSet(8, 8);
            }
        }
        

        public Map StartUpLevel()
        {
            if (useS4)
                return levelEagle[GlobalSettings.STRAT_UP_INDEX[0], GlobalSettings.STRAT_UP_INDEX[1]]; // Only works for level eagle 
            else
            {
                currentLocationIndex = levelRNG.StartUpMap();
                return currentMapSet[currentLocationIndex[0], currentLocationIndex[1]]; 
            }
                
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
                    return (currentMapSet[currentLocationIndex[0], currentLocationIndex[1] - 1] != null);

                case (int)GlobalSettings.Direction.Right:
                    if (currentLocationIndex[1] >= currentMapSet.GetLength(1) - 1) return false;
                    return (currentMapSet[currentLocationIndex[0], currentLocationIndex[1] + 1] != null);

                default:
                    return false;
            }
        }

        // Return the room in that direction, assume there is one 
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

        /// <summary>
        /// Remove the item/enemy from current room, so that the player cannot exploit on it. 
        /// </summary>
        /// <param name="Index">Index of the item/enemy.</param>
        public void RemoveOneIndex(int Index)
        {
            bool OneItemRemoved = false; 
            Map ActiveMap = currentMapSet[currentLocationIndex[0], currentLocationIndex[1]];

            for (int i = 0; i < GlobalSettings.TILE_ROW; i++)
            {
                for (int j = 0; j < GlobalSettings.TILE_COLUMN; j++)
                {
                    if(ActiveMap.Arrangement[i, j] == Index)
                    {
                        ActiveMap.Arrangement[i, j] = ActiveMap.DefaultBlock;
                        OneItemRemoved = true; 
                    } 
                    if (OneItemRemoved)
                        break; 
                }
            }
        }

        // Connect 2 rooms via a hole 
        public void HoleBridgeRooms(int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    currentMapSet[currentLocationIndex[0] - 1, currentLocationIndex[1]].Holes[
                        (int)GlobalSettings.Direction.Down] = true;
                    break; 
                case (int)GlobalSettings.Direction.Down:
                    currentMapSet[currentLocationIndex[0] + 1, currentLocationIndex[1]].Holes[
                        (int)GlobalSettings.Direction.Up] = true;
                    break;

                case (int)GlobalSettings.Direction.Left:
                    currentMapSet[currentLocationIndex[0], currentLocationIndex[1] - 1].Holes[
                        (int)GlobalSettings.Direction.Right] = true;
                    break;

                case (int)GlobalSettings.Direction.Right:
                    currentMapSet[currentLocationIndex[0] - 1, currentLocationIndex[1] + 1].Holes[
                        (int)GlobalSettings.Direction.Left] = true;
                    break;

                default:
                    break;
            }
        }

    }

}
