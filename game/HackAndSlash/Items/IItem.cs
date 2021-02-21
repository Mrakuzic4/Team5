using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    interface IItem
    {
        void LoadContent();

        void Update();

        void Draw();

        void CollectItem(IPlayer player);

        void UseItem(int currentPlayerDirection, Vector2 currentPlayerPosition);

        void ChangeToCollectable();

        void ChangeToUseable();

        void ChangeToBeingUsed();

        void ChangeToExpended();
    }
}