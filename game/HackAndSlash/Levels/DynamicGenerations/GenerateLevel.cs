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
        private bool dev = false;

        public Map[,] levelSet;

        public int levelSetRow { set; get; }
        public int levelSetCol { set; get; }

        public int startUpCol { set; get; }

        public int startUpRow { set; get; }

        private int levelRowCount;
        private int levelColCount;

        private const int HIDDEN_DOOR_THRESHOLD = 6;
        private const int MERCHANT_ROOM_COUNT = 2;
        private const int BOSS_ROOM_COUNT = 1;
        private const int BOSS_ROOM_DIST = 6; 

        public GenerateLevel()
        {

        }

        public Map[,] GenerateLevelSet(int Row, int Col)
        {
            levelSetRow = Row;
            levelSetCol = Col;

            init();
            PickStartUpRoom();
            PopulateRooms();

            SetBossRooms();
            RegulateDoors();

            SetMerchantRooms();

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

            levelRowCount = levelSet.GetLength(0);
            levelColCount = levelSet.GetLength(1);

        }

        // Change doors depending on the inter-room relationship 
        private void RegulateDoors()
        {
            int[] iter = new int[] { 0, 1, 2, 3 };
            for (int i = 0; i < levelSet.GetLength(0); i++)
            {
                for (int j = 0; j < levelSet.GetLength(1); j++)
                {
                    foreach (int Dir in iter)
                    {
                        if (HasNextRoom(new int[] { i, j }, Dir) && levelSet[i, j] != null)
                        {
                            if (GlobalSettings.RND.Next(100) < HIDDEN_DOOR_THRESHOLD)
                            {
                                AddHiddenDoors(new int[] { i, j }, Dir);
                                AddItemInRoom(new int[] { i, j }, GlobalSettings.BOMB_ITEM, 1);
                            }
                            else
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

        /// <summary>
        /// Mark other doors as none and can only be opened by bomb. 
        /// </summary>
        /// <param name="CurrentPos"></param>
        /// <param name="Direction"></param>
        private void AddHiddenDoors(int[] CurrentPos, int Direction)
        {
            Vector2 Offset = new Vector2(0, 0);
            int nextRoomDoorDir = 0;

            levelSet[CurrentPos[0], CurrentPos[1]].OpenDoors[Direction] = false;
            levelSet[CurrentPos[0], CurrentPos[1]].HiddenDoors[Direction] = true;

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

            levelSet[CurrentPos[0] + (int)Offset.Y, CurrentPos[1] + (int)Offset.X].OpenDoors[nextRoomDoorDir] = false;
            levelSet[CurrentPos[0] + (int)Offset.Y, CurrentPos[1] + (int)Offset.X].HiddenDoors[nextRoomDoorDir] = true;
        }

        /// <summary>
        /// Add mystery door at that direction. 
        /// </summary>
        /// <param name="CurrentPos"></param>
        /// <param name="Direction"></param>
        private void AddMysDoors(int[] CurrentPos, int Direction)
        {
            Vector2 Offset = new Vector2(0, 0);
            int nextRoomDoorDir = 0;

            levelSet[CurrentPos[0], CurrentPos[1]].OpenDoors[Direction] = false;
            levelSet[CurrentPos[0], CurrentPos[1]].MysteryDoors[Direction] = true;

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

            levelSet[CurrentPos[0] + (int)Offset.Y, CurrentPos[1] + (int)Offset.X].OpenDoors[nextRoomDoorDir] = false;
            levelSet[CurrentPos[0] + (int)Offset.Y, CurrentPos[1] + (int)Offset.X].MysteryDoors[nextRoomDoorDir] = true;
        }

        /// <summary>
        /// Place n items of certain kind randomly in that room. 
        /// </summary>
        /// <param name="CurrentPos"></param>
        /// <param name="ItemIndex"></param>
        /// <param name="Number"></param>
        private void AddItemInRoom(int[] CurrentPos, int ItemIndex, int Number)
        {
            GenerateRoom RoomGen = new GenerateRoom();
            RoomGen.room = levelSet[CurrentPos[0], CurrentPos[1]];
            int Count = 0;

            while (Count > Number)
            {
                int pos1 = GlobalSettings.RND.Next(GlobalSettings.TILE_ROW);
                int pos2 = GlobalSettings.RND.Next(GlobalSettings.TILE_COLUMN);

                if (RoomGen.AddSoftIndex(CurrentPos, ItemIndex))
                {
                    Count++;
                }
            }
        }


        private void PickStartUpRoom()
        {
            // Randomly pick a startup Room 
            while (true)
            {
                startUpRow = GlobalSettings.RND.Next(levelSet.GetLength(0) - 1);
                startUpCol = GlobalSettings.RND.Next(levelSet.GetLength(1) - 1);

                if (levelSet[startUpRow, startUpCol] != null)
                    break;
            }
        }

        private int L1DistanceFromStart(int row, int col)
        {
            return Math.Abs(row - startUpRow) + Math.Abs(col - startUpCol);
        }

        /// <summary>
        /// Iterate through the rooms and add items, blocks, and enemies in them.
        /// </summary>
        private void PopulateRooms()
        {
            int L1Dist;
            double RowProgression = 0, ColProgression = 0;


            for (int i = 0; i < levelSet.GetLength(0); i++)
            {
                for (int j = 0; j < levelSet.GetLength(1); j++)
                {
                    L1Dist = L1DistanceFromStart(i, j);
                    ColProgression = j / levelColCount;
                    RowProgression = i / levelRowCount;

                    if (levelSet[i, j] != null)
                    {
                        GenerateRoom RoomGen = new GenerateRoom();
                        RoomGen.InitRoom();
                        RoomGen.SetPara(L1Dist, RowProgression, ColProgression);

                        if (!IsStartUpRoom(i, j))
                        { 
                            RoomGen.PopulateBlock();
                            RoomGen.PopulateEnemy();
                        }
                        RoomGen.PopulateItem();

                        levelSet[i, j] = RoomGen.room;
                    }
                }
            }
        }

        /// <summary>
        /// Randomly select n rooms and make them as merchant room.
        /// merchant rooms are locked by money. 
        /// </summary>
        private void SetMerchantRooms()
        {

            int count = 0;
            int[] iter = new int[] { 0, 1, 2, 3 };

            while (count < MERCHANT_ROOM_COUNT)
            {
                int row = GlobalSettings.RND.Next(levelSet.GetLength(0));
                int col = GlobalSettings.RND.Next(levelSet.GetLength(1));

                if (!IsStartUpRoom(row, col))
                {
                    GenerateRoom RoomGen = new GenerateRoom();
                    RoomGen.InitRoom();
                    RoomGen.SetAsMerchantRoom();

                    levelSet[row, col] = RoomGen.room;

                    foreach (int Dir in iter)
                    {
                        if (HasNextRoom(new int[] { row, col }, Dir))
                        {
                            AddMysDoors(new int[] { row, col }, Dir);
                        }
                    }

                    count++;
                }

            }
        }

        /// <summary>
        /// Randomly picking rooms, it it's far enough from startup room,
        /// set it as boss room. 
        /// </summary>
        /// <returns></returns>
        private int SetBossRooms()
        {

            int count = 0;
            int[] iter = new int[] { 0, 1, 2, 3 };
            GenerateRoom RoomGen = new GenerateRoom();
            RoomGen.InitRoom();

            if (dev) { 
                if(HasNextRoom(new int[] { startUpRow,  startUpCol}, (int)GlobalSettings.Direction.Up)) {
                    RoomGen.SetAsBossRoom();
                    levelSet[startUpRow - 1, startUpCol] = RoomGen.room;
                }
                else if (HasNextRoom(new int[] { startUpRow, startUpCol }, (int)GlobalSettings.Direction.Down)) {
                    RoomGen.SetAsBossRoom();
                    levelSet[startUpRow + 1, startUpCol] = RoomGen.room;
                }
                return 0; 
            }

            while (count < BOSS_ROOM_COUNT)
            {
                
                int row = GlobalSettings.RND.Next(levelSet.GetLength(0));
                int col = GlobalSettings.RND.Next(levelSet.GetLength(1));

                if (!IsStartUpRoom(row, col) && L1DistanceFromStart(row, col) > BOSS_ROOM_DIST)
                {
                    RoomGen.SetAsBossRoom();
                    levelSet[row, col] = RoomGen.room;
                    count++;
                }
            }

            return 0; 
        }

        /// <summary>
        /// Whether or not a given room is the startup room. 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool IsStartUpRoom(int row, int col)
        {
            return (row == startUpRow && col == startUpCol);
        }

        /// <summary>
        /// If that direction of that room has a neighbor. 
        /// True sight included.
        /// </summary>
        /// <param name="CurrentPos"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
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
            return new int[] { startUpRow, startUpCol };
        }

    }
}