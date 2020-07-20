using Microsoft.Xna.Framework;

namespace RogueSimulator.Classes.Mechanics
{
    public class CollisionBlock : ICollidable
    {
        public Rectangle CollisionRectangle { get; }

        public CollisionBlock(Vector2 position, Rectangle animationFrame)
        {
            CollisionRectangle = new Rectangle(
                x: (int)position.X,
                y: (int)position.Y,
                width: animationFrame.Width,
                height: animationFrame.Height
            );
        }
    }
}