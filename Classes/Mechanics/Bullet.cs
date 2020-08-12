using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Bullet : Interfaces.IDrawable, IUpdatable, ICollidable
    {
        private const int DEFAULT_VELOCITY = 550;
        private Texture2D _texture;
        private Vector2 _position;
        private double _prevElapsedMs;
        public Bullet(double initialFireTime, Texture2D bulletTexture, Vector2 from, Vector2 to, int velocity = DEFAULT_VELOCITY)
        {
            _prevElapsedMs = initialFireTime;
            _texture = bulletTexture;
            _position = from;
            Velocity = velocity;
        }
        public int Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }

        public void Update(GameTime gameTime)
        {
            double elapsed = gameTime.TotalGameTime.TotalMilliseconds;
            _position.X += Utility.PixelsToTravel(_prevElapsedMs, elapsed, Velocity);
            _prevElapsedMs = elapsed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
