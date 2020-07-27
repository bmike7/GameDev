using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Input
    {
        public bool IsRight { get; set; } = false;
        public bool IsLeft { get; set; } = false;
        public bool IsSpace { get; set; } = false;
        public bool IsUp { get; set; } = false;
        public bool isDown { get; set; } = false;

        public void Update()
        {
            IsRight = Utility.IsKeyPressed(Keys.D);
            IsLeft = Utility.IsKeyPressed(Keys.A);
            IsSpace = Utility.IsKeyPressed(Keys.Space);
            IsUp = Utility.IsKeyPressed(Keys.W);
            isDown = Utility.IsKeyPressed(Keys.S);
        }
    }
}
