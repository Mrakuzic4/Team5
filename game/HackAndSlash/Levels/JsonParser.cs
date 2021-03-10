using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Content;

namespace HackAndSlash
{
    class JsonParser
    {
        
        private string targetLocation = @"Content/info/levelDemoM1.json";
        private string targetContent;
        private Map currentMapInfo;

        public JsonParser(string TargetFile)
        {
            targetLocation = TargetFile; 
            Parse();
        }

        private void ReadFile()
        {

            if (File.Exists(targetLocation))
            {
                targetContent = File.ReadAllText(targetLocation);
            }
        }

        public void Parse()
        {
            ReadFile();

            // If this line gives error, modify the properties of the .json file 
            currentMapInfo = JsonConvert.DeserializeObject<Map>(targetContent);

        }
        public Map getCurrentMapInfo()
        {
            return currentMapInfo;
        }
    }
}
