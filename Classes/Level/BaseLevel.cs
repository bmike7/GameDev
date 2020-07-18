using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Classes.Level
{
    public abstract class BaseLevel
    {
        protected Texture2D _texture;
        protected Viewport _viewport;
        public abstract void Create();

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract CollisionBlock[] GetCollisionBlocks(Vector2 characterPosition);
    }
}