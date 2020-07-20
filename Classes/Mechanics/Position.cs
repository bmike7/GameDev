using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Position
    {
        private const int NUMBER_OF_PIXELS_TO_TRAVEL_P_S = 300;
        private double _tempElapsed;
        private CollisionBlock _ownCollisionBlock;
        private ICollidable[] _tempCollisionRectangles;
        private double _prevElapsed = 0;
        private KeyboardState _prevKeyboardState;
        private Input _input;


        public Position(float x, float y)
        {
            X = x;
            Y = y;
            _input = new Input
            {
                IsUp = false,
                IsRight = false,
                isDown = false,
                IsLeft = false,
            };
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
            Rectangle actionFrameRectangle,
            ICollidable[] collisionBlocks)
        {
            _tempElapsed = gameTime.TotalGameTime.TotalMilliseconds;
            _tempCollisionRectangles = collisionBlocks;
            _ownCollisionBlock = new CollisionBlock(new Vector2(X, Y), actionFrameRectangle);

            _input.Update();

            float x = getNewX();
            float y = getNewY();

            _prevKeyboardState = Keyboard.GetState();
            _prevElapsed = _tempElapsed;
            return new Vector2(x, y);
        }

        private float getNewX()
        {
            // X will depent on input (L&R) and left or right collision
            return _input.IsRight
                ? X + numberOfHorizontalPixelsToTravel()
                : _input.IsLeft
                    ? X - numberOfHorizontalPixelsToTravel()
                    : X;
        }

        private float getNewY()
        {
            // Y will depent on a jumping state and will fall if not stand on anything
            return _input.isDown
                ? Y + numberOfVerticalPixelsToTravel()
                : _input.IsUp
                    ? Y - numberOfVerticalPixelsToTravel()
                    : Y;
        }

        private float numberOfHorizontalPixelsToTravel()
        {
            if (Keyboard.GetState() != _prevKeyboardState)
                _prevElapsed = _tempElapsed;

            float toTravel = (float)(NUMBER_OF_PIXELS_TO_TRAVEL_P_S * (_tempElapsed - _prevElapsed) / 1000);

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