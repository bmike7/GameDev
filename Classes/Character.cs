using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes
{

    public class Character
    {
        private Movement _movement;
        private Texture2D _texture;

        private Dictionary<CharacterAction, Action> _actions = new Dictionary<CharacterAction, Action>
        {
            {CharacterAction.IDLE, new Action(87, 1035, 58, 87, 231, 6)},
            {CharacterAction.RUN, new Action(75, 1432, 78, 85, 231, 8)},
        };

        public Character(Texture2D texture, Vector2 pos)
        {
            _texture = texture;
            _movement = new Movement(pos);
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            _movement.UpdateMovement(gameTime, keyboardState);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                new Vector2(_movement.Position.X, _movement.Position.Y),
                _actions.Single(action => action.Key == _movement.Action).Value.getActionFrame(gameTime),
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(1, 1),
                _movement.Direction == CharacterDirection.LEFT ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }
    }
}