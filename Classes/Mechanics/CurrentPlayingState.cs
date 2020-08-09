using System.Collections.Generic;
using Microsoft.Xna.Framework;

using RogueSimulator.Classes.Entity;
using RogueSimulator;

namespace RogueSimulator.Classes.Mechanics
{
    public class CurrentPlayingState
    {
        public LevelType SelectedLevel { get; set; } = LevelType.LEVEL1;
        public Movement Movement { get; set; } = new Movement(new Input(), new Vector2() { X = 150, Y = 150 });
        public List<Character> Characters { get; set; } = new List<Character>();

        public void ResetMovement()
        {
            Movement = new Movement(new Input(), new Vector2() { X = 150, Y = 150 });
        }
    }
}
