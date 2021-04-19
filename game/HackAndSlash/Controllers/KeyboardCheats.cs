using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class KeyboardCheats
    {
        private Game1 Game;
        private Dictionary<List<Keys>, ICommand> cheatMappings;
        private List<Keys> KeyBuffer = new List<Keys>();
        private Stopwatch InputDelay = new Stopwatch();
        private Stopwatch Timeout = new Stopwatch();
        public KeyboardCheats (Game1 game)
        {
            this.Game = game;
            //dictionary of all key combinations mapped to their respective cheats
            cheatMappings = new Dictionary<List<Keys>, ICommand>()
            {
                {new List<Keys>() { Keys.W, Keys.W, Keys.S, Keys.S, Keys.A, Keys.D, Keys.A, Keys.D, Keys.Z, Keys.N }, new GodModeCommand(game) },
                {new List<Keys>() { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, 
                    Keys.Z, Keys.N}, new GodModeCommand(game) }
            };
            InputDelay.Restart();
            Timeout.Restart();
        }
        public void Update (Keys[] inputKeys)
        {
            //only add new key to the buffer if a certain time has elapsed to prevent the same key being added from multiple frames
            if (InputDelay.ElapsedMilliseconds > GlobalSettings.DELAY_KEYBOARD)
            {
                if (inputKeys.Length > 0)
                {
                    KeyBuffer.Add(inputKeys[0]);
                    InputDelay.Restart();
                    Timeout.Restart();
                }
            }
            //clear the buffer after a certain timeout interval has elapsed
            if (Timeout.ElapsedMilliseconds > GlobalSettings.CHEAT_INPUT_TIMEOUT)
            {
                KeyBuffer.Clear();
            }
            //if the cheatMappings dictionary contains the sequence of keys, execute the cheat code
            foreach(List<Keys> k in cheatMappings.Keys)
            {
                if (k.SequenceEqual(KeyBuffer)) 
                {
                    cheatMappings[k].execute();
                }
            }
        }
    }
}
