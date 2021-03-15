using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{


    public class LevelCycling
    {

        public int[,] eagleComp = new int[,] {
            { }
        };

        // Field created jjust to fullfill sprint 3 scycling feature 
        public List<Map> S3EagleCycle;

        public LevelCycling()
        {
            S3EagleCycle = new List<Map>()
            {
                new JsonParser(MapDatabase.eagleM1).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM2).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM3).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM4).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM5).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM6).getCurrentMapInfo()
            };
        }
        
    }

}
