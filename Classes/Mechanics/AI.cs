using RogueSimulator.Interfaces;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Level;

namespace RogueSimulator.Classes.Mechanics
{
    public class AI : IInput
    {
        public bool IsRight { get; set; } = false;
        public bool IsLeft { get; set; } = false;
        public bool IsStartedJumping { get; set; } = false;

        public void Update(BaseLevel level, Character self) { }
    }
}
