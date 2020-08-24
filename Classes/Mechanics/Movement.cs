using Microsoft.Xna.Framework;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Movement
    {
        public const int ADD_ON_GROUND_CHECKER = 7;
        private const int SECOND = 1000;
        private const float TIME_OF_JUMP_MS = 0.3f;
        private IInput _input;
        private IInput _prevInput;
        private double _tempElapsedMs;
        private double _prevElapsedMs = 0;
        private Character _tempChar;
        private ICollidable[] _tempCollisionBlocks;
        private double _startedJumpingTime = 0;


        public Movement(IInput input, Vector2 position, MovementAction action = MovementAction.IDLE, MovementDirection direction = MovementDirection.RIGHT)
        {
            _input = input;
            Position = position;
            Action = action;
            Direction = direction;
        }

        public MovementAction Action { get; set; }
        public MovementDirection Direction { get; set; }
        public Vector2 Position { get; set; }
        public int HorizontalVelocity { get; set; } = 300;
        public int VerticalVelocity { get; private set; } = 420;

        public void Update(Character selfChar, GameTime gameTime, BaseLevel level)
        {
            _tempChar = selfChar;
            _tempElapsedMs = gameTime.TotalGameTime.TotalMilliseconds;
            _tempCollisionBlocks = level.GetNearCollidableBlocks(Position);

            if (_input is Input) ((Input)_input).Update();
            if (_input is AI) ((AI)_input).Update(selfChar, level, _tempElapsedMs, _prevElapsedMs);

            if (_input != _prevInput || _tempElapsedMs - SECOND > _prevElapsedMs)
                _prevElapsedMs = _tempElapsedMs;

            updatePosition(level);
            updateDirection();
            updateAction();

            _prevInput = _input;
            _prevElapsedMs = _tempElapsedMs;
        }
        private void updatePosition(BaseLevel level)
        {
            float x = getNewX();
            if (x < 0)
                x = 0;
            else if (x > level.Size - _tempChar.CollisionRectangle.Width)
                x = level.Size - _tempChar.CollisionRectangle.Width;

            Position = new Vector2(x, getNewY());
        }
        private float getNewX()
        {
            float xStep = pixelsToTravel(HorizontalVelocity);
            float moveRight = Position.X + xStep;
            float moveLeft = Position.X - xStep;

            bool goesRight = _input.IsRight && !willCollide(CollisionSide.RIGHT, (int)moveRight);
            bool goesLeft = _input.IsLeft && !willCollide(CollisionSide.LEFT, (int)moveLeft);

            return goesRight
                ? moveRight
                : goesLeft
                    ? moveLeft
                    : Position.X;
        }
        private float getNewY()
        {
            // For now I'm okay with the fact that the Player will jump and fall linear
            // and will not use acceleration of any kind
            updateJump();
            return isJumping()
                ? Position.Y - pixelsToTravel(VerticalVelocity)
                : !isOnGround()
                    ? Position.Y + pixelsToTravel(VerticalVelocity)
                    : Position.Y;
        }
        private float pixelsToTravel(int velocity)
            => Utility.PixelsToTravel(_prevElapsedMs, _tempElapsedMs, velocity);
        private void updateJump()
        {
            _startedJumpingTime = (_input.IsStartedJumping && (_tempElapsedMs > _startedJumpingTime + (TIME_OF_JUMP_MS * 1000)) && isOnGround())
                ? _tempElapsedMs
                : _startedJumpingTime;
        }
        private bool isJumping() => _tempElapsedMs < _startedJumpingTime + (TIME_OF_JUMP_MS * 1000) && _startedJumpingTime != 0;
        private bool isOnGround() => willCollide(CollisionSide.BOTTOM, (int)Position.Y + ADD_ON_GROUND_CHECKER);
        private bool willCollide(CollisionSide cs, int newCoordinate)
            => Utility.WillCollideWithOneOf(
                ownCollisionRectangle: Utility.NewOwnCollisionRectangle(_tempChar, cs, newCoordinate),
                collisionBlocks: _tempCollisionBlocks
            );
        private void updateDirection()
        {
            Direction = _input.IsRight
                ? MovementDirection.RIGHT
                : _input.IsLeft
                    ? MovementDirection.LEFT
                    : Direction;
        }
        private void updateAction()
        {
            if (!_tempChar.CanChangeAnimation())
                return;

            if (isJumping() || !isOnGround())
            {
                Action = isJumping() ? MovementAction.JUMP : MovementAction.FALL;
                return;
            }

            Action = _input.IsRight || _input.IsLeft ? MovementAction.RUN : MovementAction.IDLE;
        }
    }
}
