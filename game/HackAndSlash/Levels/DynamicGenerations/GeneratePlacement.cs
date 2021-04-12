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

        public int[] startUpLocation { get; set; }

        public GeneratePlacement()
        {
            
        }



        public bool[,] GetPlacement(int Row, int Col)
        {
            RoomGraph graph = new RoomGraph(Row, Col);
            graph.SetStartUp(Row - 1, Col / 2);


            return graph.GetArrangement();
        }

    }
}
