using Microsoft.Xna.Framework.Input;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Input : IInput
    {
        public bool IsRight { get; set; } = false;
        public bool IsLeft { get; set; } = false;
        public bool IsStartedJumping { get; set; } = false;

        public void Update()
        {
            IsRight = Utility.IsKeyPressed(Keys.D);
            IsLeft = Utility.IsKeyPressed(Keys.A);
            IsStartedJumping = Utility.IsKeyPressed(Keys.Space);
        }
    }
}
