

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace HackAndSlash
{
    public interface IPlayer
    {
        void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);
        void Update();
        void Move();
        void Attack(); //Get a class for attack!
        void Damaged();
        int GetDir();
        void ChangeDirection(int dir);
    }
}
