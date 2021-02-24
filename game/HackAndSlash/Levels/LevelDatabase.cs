using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{


    public class LevelDatabase
    {

        public Dictionary<int, Texture2D> DemoLevelStyle;
        public int[,] DemoM1;

        private static LevelDatabase instance = new LevelDatabase();
        public static LevelDatabase Instance
        {
            get
            {
                return instance;
            }
        }

        public LevelDatabase()
        {
            Initilize();
        }


        private void Initilize()
        {
            DemoLevelStyle = new Dictionary<int, Texture2D>(){
                {0, SpriteFactory.Instance.GetBlockBlank1()},
                {1, SpriteFactory.Instance.GetBlockDemo()}};
            DemoM1 = new int[,] {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0},
                {0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            };
        }
    }

}
