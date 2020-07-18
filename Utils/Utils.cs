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
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}