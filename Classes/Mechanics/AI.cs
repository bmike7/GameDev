using Microsoft.Xna.Framework;

using RogueSimulator.Interfaces;

// using RogueSimulator;
using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Level;

namespace RogueSimulator.Classes.Mechanics
{
    public class AI : IInput
    {
        public bool IsRight { get; set; } = false;
        public bool IsLeft { get; set; } = false;
        public bool IsStartedJumping { get; set; } = false;

        public void Update(Character self, BaseLevel level, double tempElapsedMs, double prevElapsedMs)
        {
            // if takes the step => !onGround || colliding ==> turn around
            Movement movement = self.GetMovement();
            MovementDirection currentDir = movement.Direction;
            ICollidable[] blocks = level.GetNearCollidableBlocks(self.GetPosition());

            float pixelsToTravel = Utility.PixelsToTravel(prevElapsedMs, tempElapsedMs, movement.HorizontalVelocity);
            float step = currentDir == MovementDirection.RIGHT ? pixelsToTravel : -pixelsToTravel;

            // does it collide or will it fall? If so turn around.
            bool onGround = Utility.WillCollideWithOneOf(
                ownCollisionRectangle: new Rectangle(
                    x: (int)(movement.Position.X),
                    y: (int)(movement.Position.Y + Movement.ADD_ON_GROUND_CHECKER),
                    width: self.CollisionRectangle.Width,
                    height: self.CollisionRectangle.Height
                ),
                collisionBlocks: blocks
            );
            if (!onGround)
                return;

            bool willFall = Utility.WillCollideWithOneOf(
                ownCollisionRectangle: new Rectangle(
                    x: (int)(movement.Position.X + step),
                    y: (int)(movement.Position.Y + Movement.ADD_ON_GROUND_CHECKER),
                    width: self.CollisionRectangle.Width,
                    height: self.CollisionRectangle.Height
                ),
                collisionBlocks: blocks
            );
            Rectangle newOwnCollisionRectangle = new Rectangle(
                x: (int)(movement.Position.X + step),
                y: (int)movement.Position.Y,
                width: self.CollisionRectangle.Width,
                height: self.CollisionRectangle.Height
            );
            bool collides = Utility.WillCollideWithOneOf(newOwnCollisionRectangle, blocks)
                || newOwnCollisionRectangle.X < 0
                || newOwnCollisionRectangle.X + newOwnCollisionRectangle.Width > level.Size;


            if (!willFall || collides)
                movement.Direction = currentDir == MovementDirection.RIGHT ? MovementDirection.LEFT : MovementDirection.RIGHT;

            currentDir = movement.Direction;
            IsRight = currentDir == MovementDirection.RIGHT;
            IsLeft = currentDir == MovementDirection.LEFT;
        }
    }
}
