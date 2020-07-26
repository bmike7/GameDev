using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public abstract class BaseLevel
    {
        protected Texture2D _texture;
        protected Texture2D _background;
        protected Viewport _viewport;
        public abstract void Create();

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract ICollidable[] GetNearCollidableBlocks(Vector2 characterPosition);
        public abstract int GetSize();
    }
}
