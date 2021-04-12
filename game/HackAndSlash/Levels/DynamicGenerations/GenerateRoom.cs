using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Generate or modify a single room 
/// </summary>
namespace HackAndSlash
{
    class GenerateRoom
    {

        public int defaultBlock { get; set; }
        public int dropPotions { get; set; }
        public int dropKeys { get; set; }
        public bool[] doorState { get; set; }

        public GenerateRoom()
        {
            defaultBlock = 0;
            dropKeys = 0;
            dropPotions = 0;
            doorState = new bool[] {false, false, false, false }; 
        }

        public Map InitRoom()
        {
            Map template = new Map();

            template.DefaultBlock = defaultBlock;

            template.Arrangement = new int[GlobalSettings.TILE_ROW, GlobalSettings.TILE_COLUMN];
            for (int i = 0; i < template.Arrangement.GetLength(0); i++)
                for (int j = 0; j < template.Arrangement.GetLength(1); j++)
                    template.Arrangement[i, j] = defaultBlock;

            template.DropKeys = dropKeys;
            template.DropPotions = dropPotions;

            
            template.HiddenDoors = doorState;
            template.LockedDoors = doorState;
            template.MysteryDoors = doorState;
            template.OpenDoors = doorState;
            template.Holes = doorState;
            

            return template;
        }

    }

}
