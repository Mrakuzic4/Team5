using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{
    interface ISprite
    {
        void Update();

        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }


    interface IController
    {
        // I had trouble givng them an interface
        // Mouse and Keyboard do share some methods with same functionality 
        // But the parameters are different 
        // So I don't really know hoe to make it and interface 
    }

}
