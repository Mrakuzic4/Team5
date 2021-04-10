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
        private SavedSettings currentSavedInfo;
        public enum ParseMode { mapMode, settingsMode };
        private ParseMode mode;

        public JsonParser(string TargetFile, ParseMode mode)
        {
            targetLocation = TargetFile;
            this.mode = mode;
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
            if (mode == ParseMode.mapMode)
            {
                // If this line gives error, modify the properties of the .json file 
                currentMapInfo = JsonConvert.DeserializeObject<Map>(targetContent);
            }
            else if (targetContent != null) {
                currentSavedInfo = JsonConvert.DeserializeObject<SavedSettings>(targetContent);
            }
        }
        public Map getCurrentMapInfo()
        {
            return currentMapInfo;
        }

        public SavedSettings getCurrentSavedSettings()
        {
            return currentSavedInfo;
        }
        public void SaveToFile()
        {
            string saveData = JsonConvert.SerializeObject(GlobalSettings.saveSets);
            File.WriteAllText(targetLocation, saveData);
            ReadFile();
        }
    }
}
