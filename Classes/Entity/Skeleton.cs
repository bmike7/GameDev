using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Entity
{
    public class Skeleton : Character, IAttacker
    {
        private const int ATTACK_DAMAGE = 15;
        public const string ASSET_NAME = "SpriteSheets/Enemies/Skeleton/AllSkelletonActions";
        public Skeleton(Texture2D texture, Vector2 position)
            : base(
                input: new AI(),
                texture: texture,
                position: position,
                collisionRectangle: new Rectangle((int)position.X, (int)position.Y, 50, 75),
                health: 150
            )
        {
            _scale = 1.6f;
            _movement.HorizontalVelocity = 100;
            _actionAnimations.Add(MovementAction.IDLE, new Animation(65, 365, 45, 51, 151, 4));
            _actionAnimations.Add(MovementAction.RUN, new Animation(65, 830, 46, 52, 151, 4));
            _actionAnimations.Add(MovementAction.HIT, new Animation(65, 675, 45, 51, 151, 4, true));
            _actionAnimations.Add(MovementAction.DIE, new Animation(65, 210, 46, 53, 151, 4, true));
            _actionAnimations.Add(MovementAction.ATTACK, new Animation(59, 48, 94, 59, 150, 8, true));
        }

        public void Attack(Character characterToAttack)
        {
            if (!CanChangeAnimation()) return;

            _movement.Action = MovementAction.ATTACK;
            characterToAttack.GetsAttacked(ATTACK_DAMAGE);
        }
    }
}
