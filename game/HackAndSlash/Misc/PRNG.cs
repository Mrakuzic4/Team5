using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class PRNG
    {
        private int[] directionIter = new int[] { 0, 1, 2, 3 }; 

        public PRNG()
        {

        }

        public int Directional(int[] PreviousDirections)
        {
            int Max = PreviousDirections.Max();
            int[] Possibilities = new int[] { 0, 0, 0, 0 };
            int BestDirection = 0;

            foreach (int Dirs in directionIter)
            {
                Possibilities[Dirs] = (PreviousDirections[Dirs] - Max + 1) * GlobalSettings.RND.Next();
                if (Possibilities[Dirs] > Possibilities[BestDirection])
                    BestDirection = Dirs; 
            }

            return BestDirection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PreviousDirections">
        /// List of ints if how many times this direction has been used. e.g. [4, 1, 0, 3]
        /// </param>
        /// <param name="MaskedDirections">
        /// true if that direction is masked and cannot go. e.g. [0, 1, 1, 0]
        /// </param>
        /// <returns></returns>
        public int DirectionMask(int[] PreviousDirections, bool[] MaskedDirections)
        {
            int Max = PreviousDirections.Max();
            int[] Possibilities = new int[] { 0, 0, 0, 0 };
            int BestDirection = 0;

            foreach (int Dirs in directionIter)
            {
                Possibilities[Dirs] = (Max - PreviousDirections[Dirs]) * GlobalSettings.RND.Next();
                if (Possibilities[Dirs] > Possibilities[BestDirection]
                    && !MaskedDirections[Dirs])
                    BestDirection = Dirs;
            }

            return BestDirection;
        }


        public int DropRate(int TotalChance, int RemainingChance, int TotalDrop, int Dropped)
        {
            int DropRate = 0;

            double chance = (TotalChance - RemainingChance + TotalDrop - Dropped) / TotalChance;
            DropRate = (int)(chance * 100);

            return DropRate;
        }

        /// <summary>
        /// Decides if this time drops or not 
        /// </summary>
        /// <param name="TotalChance">
        /// e.g. Total amount of enemies designated in this room 
        /// </param>
        /// <param name="RemainingChance">
        /// e.g. Number of enemies remaining in this room 
        /// </param>
        /// <param name="TotalDrop">
        /// e.g. Designated amount of certain item to be dropped in this room  
        /// </param>
        /// <param name="Dropped">
        /// e.g. Number of that item that has already dropped 
        /// </param>
        /// <returns>
        /// True or false of whether or not to drop 
        /// </returns>
        public bool Drop(int TotalChance, int RemainingChance, int TotalDrop, int Dropped)
        {
            if (Dropped >= TotalDrop) return false;
            else
            {
                return (GlobalSettings.RND.Next(100) <
                    DropRate(TotalChance, RemainingChance, TotalDrop, Dropped));
            }
        }
    }
}
