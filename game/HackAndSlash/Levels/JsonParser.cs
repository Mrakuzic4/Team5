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

        public class LevelMapSearch
        {
            public int[,] mapArrangement; 

        }

        public JsonParser()
        {
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

            JsonTextReader reader = new JsonTextReader(new StringReader(targetContent));

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                }
                else
                {
                    Console.WriteLine("Token: {0}", reader.TokenType);
                }
            }
        }
    }
}
