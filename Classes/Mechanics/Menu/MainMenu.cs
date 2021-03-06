using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class MainMenu : Menu
    {
        public MainMenu(Game1 game, Texture2D background, Texture2D buttonsTexture) : base(game.GraphicsDevice.Viewport, background)
        {
            Vector2 middlepointScreen = new Vector2(_viewport.Width / 2, _viewport.Height / 2);
            float x = middlepointScreen.X - 75;

            Vector2 startButtonPosition = new Vector2(x, middlepointScreen.Y);
            Vector2 quitButtonPosition = new Vector2(x, middlepointScreen.Y + 55);

            _buttonList.Add(new Button(
                onClickAction: () => game.ChangeGameState(GameState.LEVEL_SELECTOR),
                buttonTexture: buttonsTexture,
                position: startButtonPosition,
                buttonSpriteRectangle: new Rectangle(6, 4, 52, 16))
            );
            _buttonList.Add(new Button(
                onClickAction: () => game.ChangeGameState(GameState.QUIT),
                buttonTexture: buttonsTexture,
                position: quitButtonPosition,
                buttonSpriteRectangle: new Rectangle(6, 23, 52, 16))
            );
        }
    }
}
