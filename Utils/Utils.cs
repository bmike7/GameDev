using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator
{
    public enum MovementAction
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
    }

    public enum MovementDirection
    {
        LEFT,
        RIGHT,
    }

    public enum CollisionSide
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT,
    }

    public enum LevelType
    {
        LEVEL1,
        LEVEL2,
    }

    public enum GameState
    {
        MAIN_MENU,
        LEVEL_SELECTOR,
        LOADING,
        PLAYING,
        PAUSED,
        QUIT,
        GAME_OVER,
    }

    public static class Utility
    {
        public static bool IsKeyPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static bool isMouseLeftButtonClicked(MouseState mouseState, MouseState prevMouseState)
        {
            return prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released;
        }

        public static Rectangle MouseClickRectangle(MouseState mouseState)
        {
            return new Rectangle(mouseState.X, mouseState.Y, 10, 10);
        }

        public static void ClickButtonIfMouseclickIntersects(MouseState mouseState, Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                if (Utility.MouseClickRectangle(mouseState).Intersects(button.CollisionRectangle))
                    button.ExecuteOnClickAction();
            }
        }

        public static Texture2D LoadTexture(Game game, string assetName) => game.Content.Load<Texture2D>(assetName);
    }
}
