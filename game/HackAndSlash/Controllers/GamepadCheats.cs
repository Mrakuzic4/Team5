using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    class GamepadCheats
    {
        private Game1 Game;
        private Dictionary<List<Buttons>, ICommand> cheatMappings;
        private List<Buttons> ButtonBuffer = new List<Buttons>();
        private Stopwatch InputDelay = new Stopwatch();
        private Stopwatch Timeout = new Stopwatch();
        public GamepadCheats (Game1 game)
        {
            this.Game = game;
            cheatMappings = new Dictionary<List<Buttons>, ICommand>()
            {
                {new List<Buttons>() { Buttons.DPadUp, Buttons.DPadUp, Buttons.DPadDown, Buttons.DPadDown, Buttons.DPadLeft, 
                    Buttons.DPadRight, Buttons.DPadLeft, Buttons.DPadRight, Buttons.B, Buttons.A }, new GodModeCommand(game) }
            };
            InputDelay.Restart();
            Timeout.Restart();
        }
        public void Update(List<Buttons> pressedButtons)
        {
            //only add new button to the buffer if a certain time has elapsed to prevent the same button being added from multiple frames
            if (InputDelay.ElapsedMilliseconds > GlobalSettings.DELAY_KEYBOARD)
            {
                if (pressedButtons.Count > 0)
                {
                    ButtonBuffer.Add(pressedButtons[0]);
                    InputDelay.Restart();
                    Timeout.Restart();
                }
  
            }
            //clear the buffer after a certain timeout interval has elapsed
            if (Timeout.ElapsedMilliseconds > GlobalSettings.CHEAT_INPUT_TIMEOUT)
            {
                ButtonBuffer.Clear();
            }
            //if the cheatMappings dictionary contains the sequence of keys, execute the cheat code
            foreach(List<Buttons> b in cheatMappings.Keys)
            {
                if (b.SequenceEqual(ButtonBuffer)) 
                {
                    cheatMappings[b].execute();
                }
            }
        }
    }
}
