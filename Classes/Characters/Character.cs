using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Classes.Characters
{

    public class Character : ICollidable
    {
        private Movement _movement;
        private Texture2D _texture;

        public Rectangle CollisionRectangle { get; private set; }

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
            CollisionRectangle = new Rectangle((int)pos.X + 50, (int)pos.Y, 50, 80);
        }

        public virtual void Update(GameTime gameTime, ICollidable[] collisionTiles)
        {
            Animation currentAnimation = getCurrentAnimation();

            currentAnimation.Update(gameTime);
            _movement.Update(gameTime, CollisionRectangle, collisionTiles);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: new Vector2(_movement.Position.X, _movement.Position.Y),
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