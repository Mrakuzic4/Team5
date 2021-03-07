

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace HackAndSlash
{
    public interface IPlayer
    {
        void SetPos(Vector2 Pos);
        Vector2 GetPos();
        void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);
        void Update();
        void Move();
        void UseItem();
        void Attack();
        void Damaged();
        GlobalSettings.Direction GetDir();
        void ChangeDirection(GlobalSettings.Direction dir);

    }
}
