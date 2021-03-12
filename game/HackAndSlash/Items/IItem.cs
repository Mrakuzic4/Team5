using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public interface IItem
    {
        void Update();

        void Draw();

        void CollectItem();

        void UseItem(GlobalSettings.Direction currentPlayerDirection);

        void ChangeToCollectable();

        void ChangeToUseable();

        void ChangeToBeingUsed();

        void ChangeToExpended();

        Rectangle[] getCollidableTiles(bool isEnemy);
    }
}