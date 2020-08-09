using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Entity
{

    public class Player : Character, IShooter
    {
        public static string ASSET_NAME = "SpriteSheets/Wizard/allActions";
        public Player(Texture2D texture, Vector2 position)
            : base(
                input: new Input(),
                texture: texture,
                position: position,
                collisionRectangle: new Rectangle((int)position.X, (int)position.Y, 60, 70)
            )
        {
            _actionAnimations.Add(MovementAction.IDLE, new Animation(87, 1035, 58, 87, 231, 6));
            _actionAnimations.Add(MovementAction.RUN, new Animation(75, 1432, 78, 85, 231, 8));
            _actionAnimations.Add(MovementAction.JUMP, new Animation(68, 1229, 65, 87, 231, 2));
            _actionAnimations.Add(MovementAction.FALL, new Animation(74, 621, 59, 100, 231, 2));
        }

        public Gun Gun { get; set; }

        public void Shoot()
        {
            Gun.FireBullet();
        }
    }
}
