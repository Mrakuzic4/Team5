using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HackAndSlash.GlobalSettings;

namespace HackAndSlash
{
    class PlayerEnemyCollisionHandler
    {
        public void HandleCollision(IPlayer player, CollisionType collisionType) //Possibly need ICollision?? See the 2D collision slide.
        {
            if (collisionType != CollisionType.None)
            {
                player.Damaged();
            }
            
        }
    }
}
