using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 9 Mar 2021: initial sketch of the class Map 

namespace HackAndSlash
{
    class Map :IMap
    {
        private int[,] mapArrangement; 

        private IEnemy[] enemies;

        private IItem[] items;

        private bool[] doorOpen;
        private bool[] doorLocked;
        private bool[] doorCracked; 


    }
}
