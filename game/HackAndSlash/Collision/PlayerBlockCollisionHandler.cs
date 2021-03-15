using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HackAndSlash.GlobalSettings;

namespace HackAndSlash
{
    class PlayerBlockCollisionHandler
    {
        /// <summary>
        /// Adjusts player position according to each collision type
        /// </summary>
        public void HandleCollision(IPlayer player, CollisionType collisionType) 
        {
            switch (collisionType)
            {
                case CollisionType.Right:
                    player.SetPos(new Microsoft.Xna.Framework.Vector2(player.GetPos().X - GlobalSettings.STEP_SIZE_X, player.GetPos().Y));
                    break;
                case CollisionType.Left:
                    player.SetPos(new Microsoft.Xna.Framework.Vector2(player.GetPos().X + GlobalSettings.STEP_SIZE_X, player.GetPos().Y));
                    break;
                case CollisionType.Top:
                    player.SetPos(new Microsoft.Xna.Framework.Vector2(player.GetPos().X, player.GetPos().Y - GlobalSettings.STEP_SIZE_Y));
                    break;
                case CollisionType.Bottom:
                    player.SetPos(new Microsoft.Xna.Framework.Vector2(player.GetPos().X, player.GetPos().Y + GlobalSettings.STEP_SIZE_Y));
                    break;
            }
        }
    }
}
