using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HackAndSlash.GlobalSettings;


namespace HackAndSlash
{
    class SwordEnemyCollisionHandler
    {
        public void HandleCollision(IPlayer player, IEnemy damagedEnemy) //Possibly need ICollision?? See the 2D collision slide.
        {
            //If enemy is not null, then it means the sword collides with an enemy and damagedEnemy is set.
            if (damagedEnemy != null)
            {
                damagedEnemy.changeToDie();
            }
        }
    }
}
