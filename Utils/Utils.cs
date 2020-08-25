using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Interfaces;

namespace RogueSimulator
{
    public enum MovementAction
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
        ATTACK,
        SHOOT,
        HIT,
        DIE,
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

        public static float PixelsToTravel(double prevElapsedMs, double elapsedMs, int velocity)
            => (float)(velocity * (elapsedMs - prevElapsedMs) / 1000);

        public static bool WillCollideWithOneOf(Rectangle ownCollisionRectangle, ICollidable[] collisionBlocks)
        {
            foreach (ICollidable block in collisionBlocks)
            {
                if (ownCollisionRectangle.Intersects(block.CollisionRectangle))
                    return true;
            }

            return false;
        }

        public static Rectangle NewOwnCollisionRectangle(Character character, CollisionSide collisionSide, int newCoordinate)
            => new Rectangle(
                x: (collisionSide == CollisionSide.LEFT || collisionSide == CollisionSide.RIGHT) ? newCoordinate : (int)character.GetMovement().Position.X,
                y: (collisionSide == CollisionSide.BOTTOM || collisionSide == CollisionSide.TOP) ? newCoordinate : (int)character.GetMovement().Position.Y,
                width: character.CollisionRectangle.Width,
                height: character.CollisionRectangle.Height
            );
    }
}
