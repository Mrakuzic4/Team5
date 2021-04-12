using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Generate or modify a level consists of many rooms 
/// </summary>
namespace HackAndSlash
{
    class GenerateLevel
    {

        public Map[,] levelSet; 
        private RoomGraph graph; 

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



            return levelSet;
        }

        // Initilize the Map matrix with placeholders 
        private void init()
        {
            levelSet = new Map[levelSetRow, levelSetCol];

            bool[,] Placement = new bool[levelSetRow, levelSetCol];

            Placement = new GeneratePlacement().GetPlacement(levelSetRow, levelSetCol);

            // Fill the rooms with placeholders first 
            for (int i = 0; i < levelSet.GetLength(0); i++)
                for (int j = 0; j < levelSet.GetLength(1); j++)
                    if (Placement[i, j])
                        levelSet[i, j] = new GenerateRoom().InitRoom();
        }

        // Change doors depending on the inter-room relationship 
        private void RegulateDoors()
        {

        }

        public Map StartUpMap()
        {
            int Row = levelSet.GetLength(0) - 1;
            int Col = levelSet.GetLength(1) / 2;
            return levelSet[Row, Col];
        }

    }
}
