using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Input
    {
        public bool IsRight { get; set; }
        public bool IsLeft { get; set; }
        public bool IsUp { get; set; }
        public bool isDown { get; set; }

        public void Update()
        {
            IsRight = Utility.IsKeyPressed(Keys.D);
            IsLeft = Utility.IsKeyPressed(Keys.A);
            IsUp = Utility.IsKeyPressed(Keys.W);
            isDown = Utility.IsKeyPressed(Keys.S);
        }
    }
}