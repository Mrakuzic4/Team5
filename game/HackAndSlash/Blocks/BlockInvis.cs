
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{
    class BlockInvis : IBlock
    {
        private Vector2 location { get; set; }
        private SpriteBatch spriteBatch;
        public Rectangle rectangle { get; }

        public BlockInvis(Vector2 location, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.location = location;
            rectangle = new Rectangle((int)location.X, (int)location.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);
        }

        public void Update()
        {
            //Do nothing
        }
        public void Draw()
        {
            // Do nothing 
        }

        public void ChangeToMoving(GlobalSettings.CollisionType type)
        {
            //Do nothing
        }
    }
}
