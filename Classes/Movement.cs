using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes
{
    public class Movement
    {
        private const int MOVING_SPEED = 4;
        public Movement(Vector2 pos, CharacterAction action = CharacterAction.IDLE, CharacterDirection direction = CharacterDirection.RIGHT)
        {
            Position = pos;
            Action = action;
            Direction = direction;
        }

        public CharacterAction Action { get; private set; }
        public CharacterDirection Direction { get; private set; }
        public Vector2 Position { get; private set; }

        public void UpdatePosition(KeyboardState keyboardState)
        {
            Vector2 newPos = getNewPosition(keyboardState);

            bool isLeft = newPos.X < Position.X;
            bool isRight = newPos.X > Position.X;

            Direction = isRight
                ? CharacterDirection.RIGHT
                : isLeft
                    ? CharacterDirection.LEFT
                    : Direction;

            Action = newPos.X != Position.X ? CharacterAction.RUN : CharacterAction.IDLE;

            Position = newPos;
        }

        private Vector2 getNewPosition(KeyboardState kbs)
        {
            bool isRight = kbs.IsKeyDown(Keys.D);
            bool isLeft = kbs.IsKeyDown(Keys.A);

            float x = isRight
                ? Position.X + MOVING_SPEED
                : isLeft
                    ? Position.X - MOVING_SPEED
                    : Position.X;

            return new Vector2(x, Position.Y);
        }
    }
}