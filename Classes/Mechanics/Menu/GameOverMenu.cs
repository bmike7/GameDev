using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class GameOverMenu : Menu
    {
        public GameOverMenu(Game1 game, Texture2D background, Texture2D buttonsTexture) : base(game.GraphicsDevice.Viewport, background)
        {
            Vector2 middlepointScreen = new Vector2(_viewport.Width / 2, _viewport.Height / 2);
            float x = middlepointScreen.X - 75;

            Vector2 restartButtonPosition = new Vector2(x, middlepointScreen.Y);
            Vector2 quitButtonPosition = new Vector2(x, middlepointScreen.Y + 55);

            _buttonList.Add(new Button(
                onClickAction: () => game.ChangeGameState(GameState.PLAYING),
                buttonTexture: buttonsTexture,
                position: restartButtonPosition,
                buttonSpriteRectangle: new Rectangle(6, 4, 52, 16))
            );
            _buttonList.Add(new Button(
                onClickAction: () => game.ChangeGameState(GameState.MAIN_MENU),
                buttonTexture: buttonsTexture,
                position: quitButtonPosition,
                buttonSpriteRectangle: new Rectangle(6, 23, 52, 16))
            );
        }
    }
}
