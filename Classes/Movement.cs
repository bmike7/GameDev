using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes
{
    public class Movement
    {
        public Movement(Vector2 pos, CharacterAction action = CharacterAction.IDLE, CharacterDirection direction = CharacterDirection.RIGHT)
        {
            Position = new Position(pos.X, pos.Y);
            Action = action;
            Direction = direction;
        }

        public CharacterAction Action { get; private set; }
        public CharacterDirection Direction { get; private set; }
        public Position Position { get; private set; }

        public void UpdateMovement(GameTime gameTime, KeyboardState keyboardState)
        {
            Vector2 nextPos = Position.GetNextPosition(gameTime, keyboardState, Direction, Action);

            // The Direction and Action needs to be updated before the position
            // Because they depend on the current and the next one.
            UpdateDirection(nextPos);
            UpdateAction(nextPos);

            Position.Update(nextPos);
        }

        private void UpdateDirection(Vector2 newPos)
        {
            bool isLeft = newPos.X < Position.X;
            bool isRight = newPos.X > Position.X;

            Direction = isRight
                ? CharacterDirection.RIGHT
                : isLeft
                    ? CharacterDirection.LEFT
                    : Direction;
        }

        private void UpdateAction(Vector2 newPos)
        {
            Action = newPos.X != Position.X ? CharacterAction.RUN : CharacterAction.IDLE;
        }
    }
}