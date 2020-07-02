using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes
{
    public class Movement
    {
        private const int NUMBER_OF_PIXELS_TO_TRAVEL_P_S = 250;
        private double _prevElapsed;
        public Movement(Vector2 pos, CharacterAction action = CharacterAction.IDLE, CharacterDirection direction = CharacterDirection.RIGHT)
        {
            Position = pos;
            Action = action;
            Direction = direction;
            _prevElapsed = 0;
        }

        public CharacterAction Action { get; private set; }
        public CharacterDirection Direction { get; private set; }
        public Vector2 Position { get; private set; }

        public void UpdateMovement(GameTime gameTime, KeyboardState keyboardState)
        {
            Vector2 newPos = getNewPosition(gameTime, keyboardState);

            UpdateDirection(newPos);
            UpdateAction(newPos);
            UpdatePosition(newPos);
        }

        private Vector2 getNewPosition(GameTime gt, KeyboardState kbs)
        {
            bool isRight = kbs.IsKeyDown(Keys.D);
            bool isLeft = kbs.IsKeyDown(Keys.A);

            float x = isRight
                ? Position.X + numberOfPixelsToTravel(gt, CharacterDirection.RIGHT, CharacterAction.RUN)
                : isLeft
                    ? Position.X - numberOfPixelsToTravel(gt, CharacterDirection.LEFT, CharacterAction.RUN)
                    : Position.X;

            return new Vector2(x, Position.Y);
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

        private void UpdatePosition(Vector2 newPos)
        {
            Position = newPos;
        }



        private float numberOfPixelsToTravel(GameTime gt, CharacterDirection charDir, CharacterAction CharAct)
        {
            double elapsed = gt.TotalGameTime.TotalMilliseconds;

            // if other movement as prev, reset _prevElapsed to check against.
            if (charDir != Direction || CharAct != Action)
                _prevElapsed = elapsed;

            float toTravel = (float)(NUMBER_OF_PIXELS_TO_TRAVEL_P_S * (elapsed - _prevElapsed) / 1000);

            _prevElapsed = elapsed;

            // Because the momement is chosen dependent on the new location and the previous,
            // the character needs to replace at least a little bit. In this case 1px.
            return toTravel != 0 ? toTravel : 1;
        }
    }
}