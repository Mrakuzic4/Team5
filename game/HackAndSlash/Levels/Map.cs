using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 9 Mar 2021: initial sketch of the class Map 

namespace HackAndSlash
{
    public class Map : IMap
    {

        public int[,] Arrangement { get; set; }

        public int DropPotions {  get; set;  }
        public int DropKeys { get; set; }


        public bool[] LockedDoors { get; set; }
        public bool[] OpenDoors { get; set; }
        public bool[] HiddenDoors { get; set; }

        public int DefaultBlock { get; set; } 
    }
}
