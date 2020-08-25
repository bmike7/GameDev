using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Entity
{
    public abstract class Character : ICollidable, Interfaces.IDrawable
    {
        protected Movement _movement;
        protected Texture2D _texture;
        protected Rectangle _collisionRectangle;
        protected float _scale = 1;
        protected Dictionary<MovementAction, Animation> _actionAnimations = new Dictionary<MovementAction, Animation>();

        protected Character(IInput input, Texture2D texture, Vector2 position, Rectangle collisionRectangle, int health = 100, int maxHealth = 100)
        {
            _texture = texture;
            _movement = new Movement(input, position);
            _collisionRectangle = collisionRectangle;
            Health = health;
            MaxHealth = maxHealth;
        }

        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public Rectangle CollisionRectangle { get { return _collisionRectangle; } }

        public virtual void Update(GameTime gameTime, BaseLevel level)
        {
            Animation currentAnimation = getCurrentAnimation();
            currentAnimation.Update(gameTime);

            _movement.Update(this, gameTime, level);
            _collisionRectangle.X = (int)_movement.Position.X;
            _collisionRectangle.Y = (int)_movement.Position.Y;

            if (Health < 1 && CanChangeAnimation())
                level.AddToRemove(this);
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
                scale: _scale,
                effects: _movement.Direction == MovementDirection.LEFT ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layerDepth: 0
            );
        }
        public Vector2 GetPosition() => new Vector2(_movement.Position.X, _movement.Position.Y);
        public Movement GetMovement() => _movement;
        public void GetsAttacked(int damage)
        {
            Health -= damage;
            _movement.Action = Health < 1 ? MovementAction.DIE : MovementAction.HIT;
        }
        public bool CanChangeAnimation() => getCurrentAnimation().CanChangeAnimation();
        private Animation getCurrentAnimation() => _actionAnimations.ContainsKey(_movement.Action)
            ? _actionAnimations[_movement.Action]
            : _actionAnimations[MovementAction.IDLE];
    }
}
