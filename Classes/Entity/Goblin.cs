using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Classes.Entity
{
    public class Goblin : Character
    {
        public static string ASSET_NAME = "SpriteSheets/Enemies/Goblin/AllGoblinActions";
        public Goblin(Texture2D texture, Vector2 position)
            : base(
                input: new AI(),
                texture: texture,
                position: position,
                collisionRectangle: new Rectangle((int)position.X, (int)position.Y, 34, 50)
            )
        {
            _scale = 1.8f;
            _actionAnimations.Add(MovementAction.IDLE, new Animation(63, 379, 34, 37, 150, 4));
            _actionAnimations.Add(MovementAction.FALL, new Animation(63, 379, 34, 37, 150, 4));
        }
    }
}
