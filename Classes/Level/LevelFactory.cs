using System;
using System.Linq;
using System.Collections.Generic;

namespace RogueSimulator.Classes.Level
{
    //I'm also trying to limit the use of if-else and switch as branching strategies.
    //https://medium.com/swlh/factory-pattern-without-switch-this-is-how-it-should-be-done-cd895e356f44
    public class LevelFactory
    {
        private readonly Dictionary<string, Func<BaseLevel>> _levels;

        public LevelFactory()
        {
            _levels = new Dictionary<string, Func<BaseLevel>>();
        }
        public BaseLevel this[string levelType] => CreateLevel(levelType);
        public BaseLevel CreateLevel(string levelType) => _levels[levelType]();
        public string[] RegisteredTypes => _levels.Keys.ToArray();
        public void RegisterLevel(string levelType, Func<BaseLevel> factoryMethod)
        {
            if (string.IsNullOrEmpty(levelType)) return;
            if (factoryMethod is null) return;

            _levels[levelType] = factoryMethod;
        }
    }
}
