using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Bullet : Interfaces.IDrawable, IUpdatable, ICollidable
    {
        private Texture2D _texture;
        private Vector2 _position;
        private double _prevElapsedMs;
        public Bullet(double initialFireTime, Texture2D bulletTexture, Vector2 from, Vector2 to)
        {
            _prevElapsedMs = initialFireTime;
            _texture = bulletTexture;
            _position = from;
        }
        public Rectangle CollisionRectangle { get; set; }

        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
