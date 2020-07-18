using Microsoft.Xna.Framework.Input;

namespace RogueSimulator
{
    public enum CharacterAction
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
    }

    public enum CharacterDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    public static class Utility
    {
        public static bool IsKeyPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}