

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace HackAndSlash
{
    public interface IPlayer
    {
        void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);
        void Update();
    }
}
