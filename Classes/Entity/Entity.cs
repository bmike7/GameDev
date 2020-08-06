using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Entity
{
    public abstract class Entity : ICollidable, Interfaces.IDrawable
    {
        protected Movement _movement;
        protected Texture2D _texture;
        protected Rectangle _collisionRectangle;
        protected Dictionary<MovementAction, Animation> _actionAnimations = new Dictionary<MovementAction, Animation>();

        public Entity(IInput input, Texture2D texture, Vector2 position, Rectangle collisionRectangle)
        {
            _texture = texture;
            _movement = new Movement(input, position);
            _collisionRectangle = collisionRectangle;
        }

        public Rectangle CollisionRectangle { get { return _collisionRectangle; } }

        public virtual void Update(GameTime gameTime, BaseLevel level)
        {
            Animation currentAnimation = getCurrentAnimation();
            currentAnimation.Update(gameTime);

            _movement.Update(gameTime, level, CollisionRectangle);
            _collisionRectangle.X = (int)_movement.Position.X;
            _collisionRectangle.Y = (int)_movement.Position.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: GetPosition(),
                sourceRectangle: getCurrentAnimation().getAnimationFrameRectangle(),
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: new Vector2(1, 1),
                effects: _movement.Direction == MovementDirection.LEFT ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layerDepth: 0
            );
        }
        public Vector2 GetPosition() => new Vector2(_movement.Position.X, _movement.Position.Y);
        public Movement GetMovement() => _movement;
        private Animation getCurrentAnimation() => _actionAnimations[_movement.Action];
    }
}
