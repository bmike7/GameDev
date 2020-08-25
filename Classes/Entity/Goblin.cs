using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Classes.Entity
{
    public class Goblin : Character
    {
        public const string ASSET_NAME = "SpriteSheets/Enemies/Goblin/AllGoblinActions";
        public Goblin(Texture2D texture, Vector2 position)
            : base(
                input: new AI(),
                texture: texture,
                position: position,
                collisionRectangle: new Rectangle((int)position.X, (int)position.Y, 50, 60)
            )
        {
            _scale = 1.8f;
            _movement.HorizontalVelocity = 150;
            _actionAnimations.Add(MovementAction.IDLE, new Animation(63, 379, 34, 37, 150, 4));
            _actionAnimations.Add(MovementAction.RUN, new Animation(61, 532, 36, 40, 150, 8));
            _actionAnimations.Add(MovementAction.HIT, new Animation(63, 690, 34, 37, 150, 4, true));
            _actionAnimations.Add(MovementAction.DIE, new Animation(63, 222, 43, 39, 150, 4, true));
        }
    }
}
