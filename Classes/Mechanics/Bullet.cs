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
        private MovementDirection _direction;

        public Bullet(double initialFireTime, Texture2D bulletTexture, Vector2 from, MovementDirection direction, int velocity = DEFAULT_VELOCITY)
        {
            _prevElapsedMs = initialFireTime;
            _texture = bulletTexture;
            _direction = direction;
            CollisionRectangle = new Rectangle((int)from.X, (int)from.Y, 10, 10);
            Velocity = velocity;
        }
        public int Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }

        public void Update(GameTime gameTime)
        {
            double elapsed = gameTime.TotalGameTime.TotalMilliseconds;
            float pixelsToTravel = Utility.PixelsToTravel(_prevElapsedMs, elapsed, Velocity);
            float toTravel = _direction == MovementDirection.RIGHT
                ? pixelsToTravel
                : -pixelsToTravel;
            Rectangle updatedCollisionRectangle = new Rectangle(
                x: (int)(CollisionRectangle.X + toTravel),
                y: CollisionRectangle.Y,
                width: CollisionRectangle.Width,
                height: CollisionRectangle.Height
            );
            CollisionRectangle = updatedCollisionRectangle;
            _prevElapsedMs = elapsed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: new Vector2(CollisionRectangle.X, CollisionRectangle.Y),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: 1,
                effects: _direction == MovementDirection.RIGHT ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                layerDepth: 0
            );
        }
    }
}
