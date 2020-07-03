using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes
{
    public class Position
    {
        private const int NUMBER_OF_PIXELS_TO_TRAVEL_P_S = 250;
        private double _prevElapsed = 0;

        /* Normally you would think a Position wouldn't need a Direction and an Action,
         * but these variables are saved here as a private field
         * because then they can get stored and I wouldn't need to pass it through al
         * the methods manually and methods wouldn't have like a zillion parameters.
         * So it was for readability reasons
         */
        private CharacterDirection _prevCharDir = CharacterDirection.RIGHT;
        private CharacterAction _prevCharAct = CharacterAction.IDLE;
        private GameTime _tempGameTime;


        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; private set; }
        public float Y { get; private set; }

        public void Update(Vector2 newPosition)
        {
            X = newPosition.X;
            Y = newPosition.Y;
        }

        public Vector2 GetNextPosition(
            GameTime gameTime,
            KeyboardState keyboardState,
            CharacterDirection prevCharDir,
            CharacterAction prevCharAct
        )
        {
            _tempGameTime = gameTime;
            _prevCharDir = prevCharDir;
            _prevCharAct = prevCharAct;

            bool isRight = keyboardState.IsKeyDown(Keys.D);
            bool isLeft = keyboardState.IsKeyDown(Keys.A);

            float x = getNewX(isRight, isLeft);

            return new Vector2(x, Y);
        }

        private float getNewX(bool isRight, bool isLeft)
        {
            return isRight
                ? X + numberOfHorizontalPixelsToTravel(CharacterDirection.RIGHT, CharacterAction.RUN)
                : isLeft
                    ? X - numberOfHorizontalPixelsToTravel(CharacterDirection.LEFT, CharacterAction.RUN)
                    : X;
        }

        private float numberOfHorizontalPixelsToTravel(CharacterDirection newCharDir, CharacterAction newCharAct)
        {
            double elapsed = _tempGameTime.TotalGameTime.TotalMilliseconds;

            // if other movement as prev, reset _prevElapsed to check against.
            // Otherwise the char could move like 250px in ove event loop cycle.
            if (newCharDir != _prevCharDir || newCharAct != _prevCharAct)
                _prevElapsed = elapsed;

            float toTravel = (float)(NUMBER_OF_PIXELS_TO_TRAVEL_P_S * (elapsed - _prevElapsed) / 1000);

            _prevElapsed = elapsed;

            // Because the movement is chosen dependent on the new location and the previous one,
            // the character needs to move at least a little bit instead of nothing at all. I've chosen for 1px.
            return toTravel != 0 ? toTravel : 1;
        }
    }
}