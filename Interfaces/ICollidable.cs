using Microsoft.Xna.Framework;

namespace RogueSimulator.Interfaces
{
    public interface ICollidable
    {
        Rectangle CollisionRectangle { get; }

        Vector2 GetPosition()
        {
            return new Vector2(CollisionRectangle.X, CollisionRectangle.Y);
        }
    }
}
