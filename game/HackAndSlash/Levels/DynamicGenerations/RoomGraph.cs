using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// For quick access of the relationship between rooms in a level set
/// </summary>
namespace HackAndSlash
{
    class RoomNode
    {

    }

    class RoomGraph
    {
        public int[] startUpLocation { get; set; }
        public int[] currentLocationIndex { get; set; }

        private bool[,] arrangement;

        public RoomGraph(int levelSetRow, int levelSetCol)
        {
            arrangement = new bool[levelSetRow, levelSetCol];

            for (int i = 0; i < arrangement.GetLength(0); i++)
                for (int j = 0; j < arrangement.GetLength(1); j++)
                    arrangement[i, j] = false;

            startUpLocation = new int[] { levelSetRow - 1, levelSetCol / 2 };
        }

        public void SetStartUp(int Row, int Col)
        {
            startUpLocation = new int[] {Row, Col };
            arrangement[Row, Col] = true; 
        }

        public bool IsEmpty(int Direction)
        {
            switch (Direction)
            {
                case (int)GlobalSettings.Direction.Up:
                    if (currentLocationIndex[0] <= 0) return false;
                    return (arrangement[currentLocationIndex[0] - 1, currentLocationIndex[1]] == false);

                case (int)GlobalSettings.Direction.Down:
                    if (currentLocationIndex[0] >= arrangement.GetLength(0) - 1) return false;
                    return (arrangement[currentLocationIndex[0] + 1, currentLocationIndex[1]] == false);

                case (int)GlobalSettings.Direction.Left:
                    if (currentLocationIndex[1] <= 0) return false;
                    return (arrangement[currentLocationIndex[0], currentLocationIndex[1] - 1] == false);

                case (int)GlobalSettings.Direction.Right:
                    if (currentLocationIndex[1] >= arrangement.GetLength(1) - 1) return false;
                    return (arrangement[currentLocationIndex[0], currentLocationIndex[1] + 1] == false);

                default:
                    return false;
            }
        }

        public bool[,] GetArrangement()
        {
            return arrangement;
        }
    }
}
