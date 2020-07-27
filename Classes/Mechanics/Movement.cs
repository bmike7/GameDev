using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Movement
    {
        private const int HORIZONTAL_VELOCITY = 300;
        private const int VERTICAL_VELOCITY = 420;
        private const float TIME_OF_JUMP_MS = 0.3f;
        private const int ADD_ON_GROUND_CHECKER = 7;
        private Input _input;
        private KeyboardState _prevKeyboardState;
        private double _tempElapsedMs;
        private double _prevElapsedMs = 0;
        private Rectangle _tempOwnCollisionRectangle;
        private ICollidable[] _tempCollisionBlocks;
        private double _startedJumpingTime = 0;


        public Movement(Vector2 position, CharacterAction action = CharacterAction.IDLE, CharacterDirection direction = CharacterDirection.RIGHT)
        {
            _input = new Input();
            Position = position;
            Action = action;
            Direction = direction;
        }

        public CharacterAction Action { get; private set; }
        public CharacterDirection Direction { get; private set; }
        public Vector2 Position { get; private set; }

        public void Update(GameTime gameTime, BaseLevel level, Rectangle ownCollisionRectangle)
        {
            _input.Update();
            _tempElapsedMs = gameTime.TotalGameTime.TotalMilliseconds;
            _tempOwnCollisionRectangle = ownCollisionRectangle;
            _tempCollisionBlocks = level.GetNearCollidableBlocks(Position);
            if (_tempElapsedMs - 1000 > _prevElapsedMs)
                _prevElapsedMs = _tempElapsedMs;

            updatePosition(level);
            updateDirection();
            updateAction();

            _prevKeyboardState = Keyboard.GetState();
            _prevElapsedMs = _tempElapsedMs;
        }
        private void updatePosition(BaseLevel level)
        {
            float x = getNewX();
            if (x < 0)
                x = 0;
            else if (x > level.GetSize() - _tempOwnCollisionRectangle.Width)
                x = level.GetSize() - _tempOwnCollisionRectangle.Width;

            Position = new Vector2(x, getNewY());
        }
        private float getNewX()
        {
            float xStep = numberOfHorizontalPixelsToTravel();
            float moveRight = Position.X + xStep;
            float moveLeft = Position.X - xStep;

            bool goesRight = _input.IsRight && !isColliding(CollisionSide.RIGHT, (int)moveRight);
            bool goesLeft = _input.IsLeft && !isColliding(CollisionSide.LEFT, (int)moveLeft);

            return goesRight
                ? moveRight
                : goesLeft
                    ? moveLeft
                    : Position.X;
        }
        private float getNewY()
        {
            updateJump();
            return isJumping()
                ? Position.Y - numberOfVerticalPixelsToTravel()
                : !isOnGround()
                    ? Position.Y + numberOfVerticalPixelsToTravel()
                    : Position.Y;
        }
        private float numberOfHorizontalPixelsToTravel()
        {
            if (Keyboard.GetState() != _prevKeyboardState)
                _prevElapsedMs = _tempElapsedMs;

            return (float)(HORIZONTAL_VELOCITY * (_tempElapsedMs - _prevElapsedMs) / 1000);
        }
        //For now I'm okay with the fact that the character will jump and fall linear
        //and will not use acceleration of any kind
        private float numberOfVerticalPixelsToTravel()
            => (float)(VERTICAL_VELOCITY * (_tempElapsedMs - _prevElapsedMs) / 1000);
        public void updateJump()
        {
            _startedJumpingTime = (_input.IsSpace && (_tempElapsedMs > _startedJumpingTime + (TIME_OF_JUMP_MS * 1000)) && isOnGround())
                ? _tempElapsedMs
                : _startedJumpingTime;
        }
        private bool isJumping() => _tempElapsedMs < _startedJumpingTime + (TIME_OF_JUMP_MS * 1000) && _startedJumpingTime != 0;
        private bool isOnGround() => isColliding(CollisionSide.BOTTOM, (int)Position.Y + ADD_ON_GROUND_CHECKER);
        private bool isColliding(CollisionSide cs, int newCoordinate)
        {
            Rectangle ownCollisionRectangle = new Rectangle(
                x: (cs == CollisionSide.LEFT || cs == CollisionSide.RIGHT) ? newCoordinate : (int)Position.X,
                y: (cs == CollisionSide.BOTTOM || cs == CollisionSide.TOP) ? newCoordinate : (int)Position.Y,
                width: _tempOwnCollisionRectangle.Width,
                height: _tempOwnCollisionRectangle.Height
            );

            foreach (ICollidable block in _tempCollisionBlocks)
            {
                if (ownCollisionRectangle.Intersects(block.CollisionRectangle))
                    return true;
            }

            return false;
        }
        private void updateDirection()
        {
            Direction = _input.IsRight
                ? CharacterDirection.RIGHT
                : _input.IsLeft
                    ? CharacterDirection.LEFT
                    : Direction;
        }
        private void updateAction()
        {
            if (isJumping() || !isOnGround())
            {
                Action = isJumping() ? CharacterAction.JUMP : CharacterAction.FALL;
                return;
            }

            Action = _input.IsRight || _input.IsLeft ? CharacterAction.RUN : CharacterAction.IDLE;
        }
    }
}
