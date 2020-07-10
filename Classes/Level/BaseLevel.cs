using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public abstract class BaseLevel
    {
        protected Texture2D _texture;
        protected Viewport _viewport;
        public abstract void Create();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}