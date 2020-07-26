using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Characters
{

    public class Character : ICollidable, Interfaces.IDrawable
    {
        private Movement _movement;
        private Texture2D _texture;
        private Rectangle _collisionRectangle;
        public Rectangle CollisionRectangle { get { return _collisionRectangle; } }

        private Dictionary<CharacterAction, Animation> _actionAnimations = new Dictionary<CharacterAction, Animation>
        {
            {CharacterAction.IDLE, new Animation(87, 1035, 58, 87, 231, 6)},
            {CharacterAction.RUN, new Animation(75, 1432, 78, 85, 231, 8)},
            {CharacterAction.JUMP, new Animation(68, 1229, 65, 87, 231, 2)},
            {CharacterAction.FALL, new Animation(74, 621, 59, 100, 231, 2)},
        };

        public Character(Texture2D texture, Vector2 pos)
        {
            _texture = texture;
            _movement = new Movement(pos);
            _collisionRectangle = new Rectangle((int)pos.X, (int)pos.Y, 60, 70);
        }

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
                effects: _movement.Direction == CharacterDirection.LEFT ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                layerDepth: 0
            );
        }

        public Vector2 GetPosition()
        {
            return new Vector2(_movement.Position.X, _movement.Position.Y);
        }

        private Animation getCurrentAnimation()
        {
            return _actionAnimations.Single(action => action.Key == _movement.Action).Value;
        }
    }
}
