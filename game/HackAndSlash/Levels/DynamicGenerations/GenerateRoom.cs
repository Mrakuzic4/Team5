﻿using System;
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

        public GenerateRoom()
        {
            // Setup the template 
            defaultBlock = 0;
            dropKeys = 0;
            dropPotions = 0;
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

            
            template.HiddenDoors = new bool[] { false, false, false, false };
            template.LockedDoors = new bool[] { false, false, false, false };
            template.MysteryDoors = new bool[] { false, false, false, false };
            template.OpenDoors = new bool[] { false, false, false, false };
            template.Holes = new bool[] { false, false, false, false };
            

            return template;
        }

        
    }

}
