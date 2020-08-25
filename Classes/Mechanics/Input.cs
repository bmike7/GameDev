using Microsoft.Xna.Framework.Input;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Input : IInput
    {
        private bool _isRightPressed = false;
        private bool _isLeftPressed = false;
        private bool _isSpacePressed = false;

        public bool IsRight { get => _isRightPressed; }
        public bool IsLeft { get => _isLeftPressed; }
        public bool IsStartedJumping { get => _isSpacePressed; }

        public void Update()
        {
            _isRightPressed = Utility.IsKeyPressed(Keys.D);
            _isLeftPressed = Utility.IsKeyPressed(Keys.A);
            _isSpacePressed = Utility.IsKeyPressed(Keys.Space);
        }
    }
}
