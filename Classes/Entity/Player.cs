using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RogueSimulator.Classes.Level;
using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Entity
{

    public class Player : Character, IShooter
    {
        private MouseState _prevMouseState;
        public static string ASSET_NAME = "SpriteSheets/Wizard/allActions";
        public Player(Texture2D texture, Vector2 position, Texture2D bulletTexture)
            : base(
                input: new Input(),
                texture: texture,
                position: position,
                collisionRectangle: new Rectangle((int)position.X, (int)position.Y, 60, 70)
            )
        {
            Gun = new Gun(bulletTexture);
            _actionAnimations.Add(MovementAction.IDLE, new Animation(87, 1035, 58, 87, 231, 6));
            _actionAnimations.Add(MovementAction.RUN, new Animation(75, 1432, 78, 85, 231, 8));
            _actionAnimations.Add(MovementAction.JUMP, new Animation(68, 1229, 65, 87, 231, 2));
            _actionAnimations.Add(MovementAction.FALL, new Animation(74, 621, 59, 100, 231, 2));
            _actionAnimations.Add(MovementAction.SHOOT, new Animation(91, 258, 135, 84, 231, 8, true));
        }

        public Gun Gun { get; set; }

        public override void Update(GameTime gameTime, BaseLevel level)
        {
            base.Update(gameTime, level);

            MouseState mouseState = Mouse.GetState();
            if (Utility.isMouseLeftButtonClicked(mouseState, _prevMouseState) && CanChangeAnimation())
            {
                Bullet firedBullet = shoot(
                    elapsedMs: gameTime.TotalGameTime.TotalMilliseconds,
                    from: new Vector2(CollisionRectangle.X + CollisionRectangle.Width, CollisionRectangle.Y),
                    direction: _movement.Direction
                );

                if (firedBullet != null)
                    _movement.Action = MovementAction.SHOOT;

                level.AddFiredShot(firedBullet);
            }

            _prevMouseState = mouseState;
        }

        private Bullet shoot(double elapsedMs, Vector2 from, MovementDirection direction)
            => Gun?.FireBullet(elapsedMs, from, direction);
    }
}
