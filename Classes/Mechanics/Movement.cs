using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Movement
    {
        private const int NUMBER_OF_PIXELS_TO_TRAVEL_P_S = 300;
        private Input _input;
        private KeyboardState _prevKeyboardState;
        private double _tempElapsed;
        private Rectangle _tempOwnCollisionRectangle;
        private ICollidable[] _tempCollisionBlocks;
        private double _prevElapsed = 0;


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

        public void Update(GameTime gameTime, Rectangle ownCollisionRectangle, ICollidable[] collisionBlocks)
        {
            _input.Update();
            _tempElapsed = gameTime.TotalGameTime.TotalMilliseconds;
            _tempOwnCollisionRectangle = ownCollisionRectangle;
            _tempCollisionBlocks = collisionBlocks;

            updatePosition();
            updateDirection();
            updateAction();

            _prevKeyboardState = Keyboard.GetState();
            _prevElapsed = _tempElapsed;
        }

        private void updatePosition()
        {
            float x = getNewX();
            float y = getNewY();

            Position = new Vector2(x, y);
        }

        private float getNewX()
        {
            float xStep = numberOfHorizontalPixelsToTravel();

            bool goesRight = _input.IsRight && !isColliding(CollisionSide.RIGHT, (int)(Position.X + xStep));
            bool goesLeft = _input.IsLeft && !isColliding(CollisionSide.LEFT, (int)(Position.X - xStep));

            return goesRight
                ? Position.X + xStep
                : goesLeft
                    ? Position.X - xStep
                    : Position.X;
        }

        private float getNewY()
        {
            return _input.isDown
                ? Position.Y + numberOfVerticalPixelsToTravel()
                : _input.IsUp
                    ? Position.Y - numberOfVerticalPixelsToTravel()
                    : Position.Y;
        }

        private float numberOfHorizontalPixelsToTravel()
        {
            if (isNewKeyboardInput())
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

        private bool isNewKeyboardInput()
        {
            return Keyboard.GetState() != _prevKeyboardState;
        }

        private void updateDirection()
        {
            Direction = (int)Position.X > _tempOwnCollisionRectangle.X
                ? CharacterDirection.RIGHT
                : (int)Position.X < _tempOwnCollisionRectangle.X
                    ? CharacterDirection.LEFT
                    : Direction;
        }

        private void updateAction()
        {
            if ((int)Position.Y != _tempOwnCollisionRectangle.Y)
            {
                Action = Position.Y < _tempOwnCollisionRectangle.Y ? CharacterAction.JUMP : CharacterAction.FALL;
                return;
            }

            Action = (int)Position.X != _tempOwnCollisionRectangle.X ? CharacterAction.RUN : CharacterAction.IDLE;
        }
    }
}