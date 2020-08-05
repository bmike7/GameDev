using Microsoft.Xna.Framework;

using RogueSimulator;

namespace RogueSimulator.Classes.Mechanics
{
    public class CurrentPlayingState
    {
        public LevelType SelectedLevel { get; set; } = LevelType.LEVEL1;
        public Movement Movement { get; set; } = new Movement(new Input(), new Vector2() { X = 150, Y = 150 });

        public void ResetMovement()
        {
            Movement = new Movement(new Input(), new Vector2() { X = 150, Y = 150 });
        }
    }
}
