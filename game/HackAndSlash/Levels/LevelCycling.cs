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

        private const int LEVEL_SIZE = 6;

        // Field created jjust to fullfill sprint 3 scycling feature 
        public List<Map> S3EagleCycle;
        public Map[,] levelEagle; 

        public LevelCycling()
        {
            S3EagleCycle = new List<Map>()
            {
                new JsonParser(MapDatabase.eagleM1).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM2).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM3).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM4).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM5).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM6).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM7).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM8).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM9).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM10).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM11).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM12).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM13).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM14).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM15).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM16).getCurrentMapInfo(),
                new JsonParser(MapDatabase.eagleM17).getCurrentMapInfo()
            };

            string[,] EaglePaths = MapDatabase.eagle;
            levelEagle = new Map[LEVEL_SIZE, LEVEL_SIZE]; 
            for(int i = 0; i < LEVEL_SIZE; i++)
            {
                for (int j = 0; j < LEVEL_SIZE; j++)
                {
                    if (EaglePaths[i, j] != null)
                        levelEagle[i, j] = new JsonParser(EaglePaths[i, j]).getCurrentMapInfo();
                    else
                        levelEagle[i, j] = null;
                }
            }

        }
        
        public Map StartUpLevel()
        {
            return levelEagle[5, 2];
        }

        public Map NextMap(int Dir)
        {
            // Placeholder method 
            return S3EagleCycle[0];
        }

    }

}
