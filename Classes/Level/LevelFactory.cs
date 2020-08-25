using System;
using System.Linq;
using System.Collections.Generic;

namespace RogueSimulator.Classes.Level
{
    //I'm also trying to limit the use of if-else and switch as branching strategies.
    //https://medium.com/swlh/factory-pattern-without-switch-this-is-how-it-should-be-done-cd895e356f44
    public class LevelFactory
    {
        private readonly Dictionary<LevelType, Func<BaseLevel>> _levels;

        public LevelFactory()
        {
            _levels = new Dictionary<LevelType, Func<BaseLevel>>();
        }
        public BaseLevel LoadLevel(LevelType levelType) => _levels[levelType]();
        public LevelType[] RegisteredTypes() { return _levels.Keys.ToArray(); }
        public void RegisterLevel(LevelType levelType, Func<BaseLevel> factoryMethod)
        {
            if (factoryMethod is null) return;

            _levels[levelType] = factoryMethod;
        }
    }
}
