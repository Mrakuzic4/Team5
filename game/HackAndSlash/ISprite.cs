
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace HackAndSlash
{
    public interface ISprite
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
