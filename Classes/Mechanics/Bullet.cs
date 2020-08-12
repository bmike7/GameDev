using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Bullet : Interfaces.IDrawable, IUpdatable, ICollidable
    {
        private const int DEFAULT_VELOCITY = 550;
        private Texture2D _texture;
        private double _prevElapsedMs;
        public Bullet(double initialFireTime, Texture2D bulletTexture, Vector2 from, Vector2 to, int velocity = DEFAULT_VELOCITY)
        {
            _prevElapsedMs = initialFireTime;
            _texture = bulletTexture;
            CollisionRectangle = new Rectangle((int)from.X, (int)from.Y, 10, 10);
            Velocity = velocity;
        }
        public int Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }

        public void Update(GameTime gameTime)
        {
            double elapsed = gameTime.TotalGameTime.TotalMilliseconds;
            Rectangle updatedCollisionRectangle = new Rectangle(
                x: (int)(CollisionRectangle.X + Utility.PixelsToTravel(_prevElapsedMs, elapsed, Velocity)),
                y: CollisionRectangle.Y,
                width: CollisionRectangle.Width,
                height: CollisionRectangle.Height
            );
            CollisionRectangle = updatedCollisionRectangle;
            _prevElapsedMs = elapsed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(CollisionRectangle.X, CollisionRectangle.Y), Color.White);
        }
    }
}
