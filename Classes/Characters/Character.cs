using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Classes.Characters
{

    public class Character
    {
        private Movement _movement;
        private Texture2D _texture;

        private Dictionary<CharacterAction, Animation> _actionAnimations = new Dictionary<CharacterAction, Animation>
        {
            {CharacterAction.IDLE, new Animation(87, 1035, 58, 87, 231, 6)},
            {CharacterAction.RUN, new Animation(75, 1432, 78, 85, 231, 8)},
        };

        public Character(Texture2D texture, Vector2 pos)
        {
            _texture = texture;
            _movement = new Movement(pos);
        }

        public virtual void Update(GameTime gameTime)
        {
            getCurrentAnimation().Update(gameTime);
            _movement.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                new Vector2(_movement.Position.X, _movement.Position.Y),
                getCurrentAnimation().getAnimationFrame(),
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(1, 1),
                _movement.Direction == CharacterDirection.LEFT ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }

        private Animation getCurrentAnimation()
        {
            return _actionAnimations.Single(action => action.Key == _movement.Action).Value;
        }
    }
}