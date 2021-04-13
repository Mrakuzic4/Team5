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
        private const double THRESHOLD = 0.5;
        private const double OFFSET = 0.5; 
        private const int UP = (int)GlobalSettings.Direction.Up;
        private const int DOWN = (int)GlobalSettings.Direction.Up;
        private const int LEFT = (int)GlobalSettings.Direction.Up;
        private const int RIGHT = (int)GlobalSettings.Direction.Up;

        public double _Density = 0.5;
        public int _RandWeight = 10;
        public bool _StartMiddle = false; // Middle or bottom 

        public int[] startUpLocation { get; set; }
        private int row;
        private int col; 
        

        public GeneratePlacement(int Row, int Col)
        {
            row = Row;
            col = Col; 
        }



        public bool[,] GetPlacement()
        {
            RoomGraph graph = new RoomGraph(row, col);
            List<RoomNode> exploreLeads = new List<RoomNode>();
            startUpLocation = _StartMiddle ? new int[] { row / 2, col/ 2} 
                : new int[] { row - 1, col / 2 }; 

            graph.SetStartUp(startUpLocation[0], startUpLocation[1]);
            exploreLeads.Add(new RoomNode(startUpLocation, UP)); 

            while (exploreLeads.Count > 0)
            {
                foreach (RoomNode node in exploreLeads.ToArray())
                {
                    if (graph.ReachingCorner(node.index) || graph.ReachingDeadend(node.index))
                    {
                        exploreLeads.Remove(node);
                        continue;
                    }
                        

                    foreach (int Direction in node.PossibleExpandDir())
                    {
                        int emptyCount = graph.EmptyCount(node.index, Direction);
                        double possbility = GlobalSettings.RND.NextDouble() - OFFSET;
                        if (Direction == UP || Direction == DOWN)
                            possbility += emptyCount / (col - 1.0);
                        else
                            possbility += emptyCount / (row - 1.0);

                        if (graph.IsEmpty(node.index, Direction) && possbility > THRESHOLD)
                        {
                            graph.AddRoom(node.index, Direction);
                            RoomNode newNode = new RoomNode(node.NeighborPos(Direction), Direction);
                            newNode.maxCombo = (Direction == node.expansionDir) ? node.maxCombo + 1 : 0;

                            exploreLeads.Add(newNode);
                        }
                    }
                    exploreLeads.Remove(node);
                }
            }


            return graph.GetArrangement();
        }
    }

}
