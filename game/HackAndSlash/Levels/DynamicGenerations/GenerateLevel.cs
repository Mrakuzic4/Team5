using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;



/// <summary>
/// Generate or modify a level consists of many rooms 
/// </summary>
namespace HackAndSlash
{
    class GenerateLevel
    {

        public Map[,] levelSet; 

        public int levelSetRow { set; get; }
        public int levelSetCol { set; get; }

        public GenerateLevel()
        {

        }

        public Map[,] GenerateLevelSet(int Row, int Col)
        {
            levelSetRow = Row;
            levelSetCol = Col;

            init();
            RegulateDoors();


            return levelSet;
        }

        // Initilize the Map matrix with placeholders 
        private void init()
        {
            levelSet = new Map[levelSetRow, levelSetCol];

            bool[,] Placement = new bool[levelSetRow, levelSetCol];

            Placement = new GeneratePlacement(levelSetRow, levelSetCol).GetPlacement();

            // Fill the rooms with placeholders first 
            for (int i = 0; i < levelSet.GetLength(0); i++)
                for (int j = 0; j < levelSet.GetLength(1); j++)
                    if (Placement[i, j])
                        levelSet[i, j] = new GenerateRoom().InitRoom();
        }

        // Change doors depending on the inter-room relationship 
        private void RegulateDoors()
        {
            int[] iter = new int[] { 0, 1, 2, 3 };
            for (int i = 0; i < levelSet.GetLength(0); i++) {
                for (int j = 0; j < levelSet.GetLength(1); j++) {
                    foreach (int Dir in iter) {
                        if (HasNextRoom(new int[] {i, j }, Dir) && levelSet[i, j] != null) {
                            AddOpenDoors(new int[] { i, j }, Dir);
                        }
                    }
                }
            }
                
        }

        /// <summary>
        /// The contract is there must have been a room on that direction.  
        /// </summary>
        /// <param name="CurrentPos"></param>
        /// <param name="Direction"></param>
        private void AddOpenDoors(int[] CurrentPos, int Direction)
        {
            Vector2 Offset = new Vector2(0, 0);
            int nextRoomDoorDir = 0;

            levelSet[CurrentPos[0], CurrentPos[1]].OpenDoors[Direction] = true;

            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    Offset.Y = -1;
                    nextRoomDoorDir = (int)GlobalSettings.Direction.Down; 
                    break;

                case (int)GlobalSettings.Direction.Down:
                    Offset.Y = 1;
                    nextRoomDoorDir = (int)GlobalSettings.Direction.Up;
                    break;

                case (int)GlobalSettings.Direction.Left:
                    Offset.X = -1;
                    nextRoomDoorDir = (int)GlobalSettings.Direction.Right;
                    break;

                case (int)GlobalSettings.Direction.Right:
                    Offset.X = 1;
                    nextRoomDoorDir = (int)GlobalSettings.Direction.Left;
                    break;

                default:
                    break;
            }

            levelSet[CurrentPos[0] + (int)Offset.Y, CurrentPos[1] + (int)Offset.X].OpenDoors[nextRoomDoorDir] = true;
        }

        public bool HasNextRoom(int[] CurrentPos, int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    if (CurrentPos[0] <= 0) return false;
                    return (levelSet[CurrentPos[0] - 1, CurrentPos[1]] != null);

                case (int)GlobalSettings.Direction.Down:
                    if (CurrentPos[0] >= levelSet.GetLength(0) - 1) return false;
                    return (levelSet[CurrentPos[0] + 1, CurrentPos[1]] != null);

                case (int)GlobalSettings.Direction.Left:
                    if (CurrentPos[1] <= 0) return false;
                    return (levelSet[CurrentPos[0], CurrentPos[1] - 1] != null);

                case (int)GlobalSettings.Direction.Right:
                    if (CurrentPos[1] >= levelSet.GetLength(1) - 1) return false;
                    return (levelSet[CurrentPos[0], CurrentPos[1] + 1] != null);

                default:
                    return false;
            }
        }
        public int[] StartUpMap()
        {
            int Row;
            int Col;
            while (true)
            {
                Row = GlobalSettings.RND.Next(levelSet.GetLength(0) - 1);
                Col = GlobalSettings.RND.Next(levelSet.GetLength(1) - 1);

                if (levelSet[Row, Col] != null)
                    break; 
            }
            return new int[] { Row, Col };
        }

    }
}
