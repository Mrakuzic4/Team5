using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static HackAndSlash.GlobalSettings;

namespace HackAndSlash.Collision
{
    class EnemyBlockCollision
    {
        public void HandleCollision(IEnemy enemy, CollisionType collisionType) //Possibly need ICollision?? See the 2D collision slide.
        {
            switch (collisionType)
            {
                case CollisionType.Right:

                    //if ((enemy.GetDirection() == GlobalSettings.Direction.Right))
                    //{
                        enemy.SetPos(new Microsoft.Xna.Framework.Vector2(enemy.GetPos().X - 1, enemy.GetPos().Y));
                    //}
                    enemy.changeToMoveLeft();
                    break;
                case CollisionType.Left:
                    //if((enemy.GetDirection() == GlobalSettings.Direction.Left))
                    //{
                        enemy.SetPos(new Microsoft.Xna.Framework.Vector2(enemy.GetPos().X + 1, enemy.GetPos().Y));
                    //}
                    enemy.changeToMoveRight();
                    break;
                case CollisionType.Top:
                    //if ((enemy.GetDirection() == GlobalSettings.Direction.Up))
                    //{
                        enemy.SetPos(new Microsoft.Xna.Framework.Vector2(enemy.GetPos().X, enemy.GetPos().Y + 1));
                    //}
                    enemy.changeToMoveDown();
                    break;
                case CollisionType.Bottom:

                    //if ((enemy.GetDirection() == GlobalSettings.Direction.Down))
                    //{
                        enemy.SetPos(new Microsoft.Xna.Framework.Vector2(enemy.GetPos().X, enemy.GetPos().Y - 1));
                    //}
                    enemy.changeToMoveUp();
                    break;
            }
        }
    }
}
