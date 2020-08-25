using Microsoft.Xna.Framework;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class AI : IInput
    {
        private const int NEARBY_DISTANCE = 100;
        private MovementDirection _currentDir = MovementDirection.RIGHT;
        private Character _tempSelf;
        private Movement _tempMovement;
        private int _levelSize;
        private ICollidable[] _tempBlocks;

        public bool IsRight { get => _currentDir == MovementDirection.RIGHT && _tempMovement.Action != MovementAction.ATTACK; }
        public bool IsLeft { get => _currentDir == MovementDirection.LEFT && _tempMovement.Action != MovementAction.ATTACK; }
        public bool IsStartedJumping { get; set; } = false;

        public void Update(Character self, BaseLevel level, double tempElapsedMs, double prevElapsedMs)
        {
            _tempSelf = self;
            _tempMovement = self.GetMovement();
            _levelSize = level.Size;
            _tempBlocks = level.GetNearCollidableBlocks(self.GetPosition());

            if (!isOnGround()) return;

            Player player = level.Player;
            if (self is IAttacker && playerIsNearby(self, player))
            {
                attack(self as IAttacker, player);
                return;
            }

            updateDirection(tempElapsedMs, prevElapsedMs);
        }

        private void updateDirection(double tempElapsedMs, double prevElapsedMs)
        {
            float pixelsToTravel = Utility.PixelsToTravel(prevElapsedMs, tempElapsedMs, _tempMovement.HorizontalVelocity);
            float step = _tempMovement.Direction == MovementDirection.RIGHT ? pixelsToTravel : -pixelsToTravel;
            Rectangle possibleNextCollisionRectangle = newOwnCollisionRectangle(step);

            if (!willFall(step) || willCollide(possibleNextCollisionRectangle))
                _tempMovement.Direction = _tempMovement.Direction == MovementDirection.RIGHT ? MovementDirection.LEFT : MovementDirection.RIGHT;

            _currentDir = _tempMovement.Direction;
        }
        private bool isOnGround() =>
            Utility.WillCollideWithOneOf(
                ownCollisionRectangle: new Rectangle(
                    x: (int)(_tempMovement.Position.X),
                    y: (int)(_tempMovement.Position.Y + Movement.ADD_ON_GROUND_CHECKER),
                    width: _tempSelf.CollisionRectangle.Width,
                    height: _tempSelf.CollisionRectangle.Height
                ),
                collisionBlocks: _tempBlocks
            );
        private bool willFall(float step) =>
            Utility.WillCollideWithOneOf(
                ownCollisionRectangle: new Rectangle(
                    x: (int)(_tempMovement.Position.X + step),
                    y: (int)(_tempMovement.Position.Y + Movement.ADD_ON_GROUND_CHECKER),
                    width: _tempSelf.CollisionRectangle.Width,
                    height: _tempSelf.CollisionRectangle.Height
                ),
                collisionBlocks: _tempBlocks
            );
        private Rectangle newOwnCollisionRectangle(float step) =>
            new Rectangle(
                x: (int)(_tempMovement.Position.X + step),
                y: (int)_tempMovement.Position.Y,
                width: _tempSelf.CollisionRectangle.Width,
                height: _tempSelf.CollisionRectangle.Height
            );
        private bool willCollide(Rectangle possibleNextColRec)
            => Utility.WillCollideWithOneOf(possibleNextColRec, _tempBlocks)
                || possibleNextColRec.X < 0
                || possibleNextColRec.X + possibleNextColRec.Width > _levelSize;
        private bool playerIsNearby(Character self, Player player) => Vector2.Distance(self.GetPosition(), player.GetPosition()) < NEARBY_DISTANCE;
        private void attack(IAttacker attacker, Character characterToAttack) => attacker.Attack(characterToAttack);
    }
}
