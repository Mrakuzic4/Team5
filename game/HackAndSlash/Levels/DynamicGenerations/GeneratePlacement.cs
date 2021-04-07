using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Generate the placement of all rooms in a level set 
/// </summary>
namespace HackAndSlash
{
    class GeneratePlacement
    {

        public GeneratePlacement()
        {

        }

        public bool[,] GetPlacement(int Row, int Col)
        {
            bool[,] Placement = new bool[Row, Col];

            for (int i = 0; i < Placement.GetLength(0); i++)
                for (int j = 0; j < Placement.GetLength(1); j++)
                    Placement[i, j] = false;



            return Placement;
        }

    }
}
