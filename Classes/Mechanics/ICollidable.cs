using Microsoft.Xna.Framework;

namespace RogueSimulator.Classes.Mechanics
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