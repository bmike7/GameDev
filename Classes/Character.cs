using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes
{
    using Position = Microsoft.Xna.Framework.Vector2;

    enum CharacterActions
    {
        IDLE,
        RUN,
        JUMP,
        ATTACK,
    }

    enum CharacterDirection
    {
        LEFT,
        RIGHT,
    }

    public class Character
    {
        private Position _position;
        private Texture2D _texture;
        private CharacterActions _currentAction;
        private CharacterDirection _direction;

        private Dictionary<CharacterActions, Action> _actions = new Dictionary<CharacterActions, Action>
        {
            {CharacterActions.IDLE, new Action(87, 1035, 58, 87, 231, 6)},
            {CharacterActions.RUN, new Action(75, 1432, 78, 85, 231, 8)},
        };

        public Character(Texture2D texture, Position pos)
        {
            _texture = texture;
            _position = pos;
            _currentAction = CharacterActions.IDLE;
            _direction = CharacterDirection.RIGHT;
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            CharacterDirection isLeftDirection = keyboardState.IsKeyDown(Keys.A)
                ? CharacterDirection.LEFT
                : _direction;

            _direction = keyboardState.IsKeyDown(Keys.D)
                ? CharacterDirection.RIGHT
                : isLeftDirection;

            _currentAction = keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.A)
                ? CharacterActions.RUN
                : CharacterActions.IDLE;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                _actions.Single(action => action.Key == _currentAction).Value.getActionFrame(gameTime),
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(1, 1),
                _direction == CharacterDirection.LEFT ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0
            );
        }
    }
}