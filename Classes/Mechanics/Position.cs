using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Position
    {
        private const int NUMBER_OF_PIXELS_TO_TRAVEL_P_S = 300;
        private double _prevElapsed = 0;

        /* Normally you would think a Position wouldn't need a Direction and an Action,
         * but these variables are saved here as private fields
         * because then they can get stored and I wouldn't need to pass it through al
         * the methods manually and methods wouldn't have like a zillion parameters.
         * So it was for readability reasons
         */
        private CharacterDirection _prevCharDir = CharacterDirection.RIGHT;
        private CharacterAction _prevCharAct = CharacterAction.IDLE;
        private GameTime _tempGameTime;
        private CollisionBlock[] _tempCollisionBlocks;


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
            CharacterDirection prevCharDir,
            CharacterAction prevCharAct,
            CollisionBlock[] collisionBlocks
        )
        {
            _tempGameTime = gameTime;
            _tempCollisionBlocks = collisionBlocks;
            _prevCharDir = prevCharDir;
            _prevCharAct = prevCharAct;

            bool isMovingRight = Utility.IsKeyPressed(Keys.D);
            bool isMovingLeft = Utility.IsKeyPressed(Keys.A);
            bool isMovingUp = Utility.IsKeyPressed(Keys.W);
            bool isMovingDown = Utility.IsKeyPressed(Keys.S);

            float x = getNewX(isMovingRight, isMovingLeft);
            float y = getNewY(isMovingDown, isMovingUp);

            return new Vector2(x, y);
        }

        private float getNewX(bool isRight, bool isLeft)
        {
            return isRight
                ? X + numberOfHorizontalPixelsToTravel(CharacterDirection.RIGHT, CharacterAction.RUN)
                : isLeft
                    ? X - numberOfHorizontalPixelsToTravel(CharacterDirection.LEFT, CharacterAction.RUN)
                    : X;
        }

        private float getNewY(bool isDown, bool isUp)
        {
            return isDown
                ? Y + numberOfVerticalPixelsToTravel()
                : isUp
                    ? Y - numberOfVerticalPixelsToTravel()
                    : Y;
        }

        private float numberOfHorizontalPixelsToTravel(CharacterDirection newCharDir, CharacterAction newCharAct)
        {
            double elapsed = _tempGameTime.TotalGameTime.TotalMilliseconds;

            // if other movement as prev, reset _prevElapsed to check against.
            // Otherwise the char could move like 250px in one event loop cycle.
            if (newCharDir != _prevCharDir || newCharAct != _prevCharAct)
                _prevElapsed = elapsed;

            float toTravel = (float)(NUMBER_OF_PIXELS_TO_TRAVEL_P_S * (elapsed - _prevElapsed) / 1000);

            _prevElapsed = elapsed;

            // Because the movement is chosen dependent on the new location and the previous one,
            // the character needs to move at least a little bit instead of nothing at all. I've chosen for 1px.
            return toTravel != 0 ? toTravel : 1;
        }

        private float numberOfVerticalPixelsToTravel()
        {
            return 3;
        }
    }
}