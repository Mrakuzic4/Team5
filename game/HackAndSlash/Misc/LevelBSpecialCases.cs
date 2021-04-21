using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HackAndSlash
{
    /// <summary>
    /// Dealing with the special cases in level eagle 
    /// </summary>
    class LevelBSpecialCases : ISpecialCases
    {
        private Game1 game; 
        
        private int[] CurrentRoomIndex = new int[] { 0, 0};
        private int[] LastRoomIndex = new int[] { 0, 0 };

        private List<int> merchantList = new List<int>() {
            GlobalSettings.NPC_OLD_MAN,
            GlobalSettings.NPC_OLD_WOMAN
        };
        private List<int> bossList = new List<int>() { 
            GlobalSettings.BOSS_ENEMY
        };

        private enum roomTypes {normal, merchant, boss };
        private int currentRoomType; 

        public LevelBSpecialCases()
        {

        }

        public void Update(Game1 Game)
        {


        }


    }
}

    

