using Microsoft.Xna.Framework.Input;

namespace RogueSimulator
{
    public enum CharacterAction
    {
        IDLE,
        RUN,
    }

    public enum CharacterDirection
    {
        LEFT,
        RIGHT,
    }

    public static class Utility
    {
        public static bool IsKeyPressed(Keys key)
        {
            KeyboardState stateKey = Keyboard.GetState();
            return stateKey.IsKeyDown(key);
        }
    }
}