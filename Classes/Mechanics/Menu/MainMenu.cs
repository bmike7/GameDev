using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class MainMenu : Menu
    {
        public MainMenu(Viewport viewport, Texture2D background, Texture2D buttonsTexture) : base(viewport, background)
        {
            Vector2 middlepointScreen = new Vector2(_viewport.Width / 2, _viewport.Height / 2);
            float x = middlepointScreen.X - Button.WIDTH / 2;

            Vector2 startButtonPosition = new Vector2(x, middlepointScreen.Y);
            Vector2 quitButtonPosition = new Vector2(x, middlepointScreen.Y + 55);

            _buttonList.Add(new Button(ButtonAction.START, buttonsTexture, startButtonPosition, new Rectangle(6, 4, 52, 16)));
            _buttonList.Add(new Button(ButtonAction.QUIT, buttonsTexture, quitButtonPosition, new Rectangle(6, 23, 52, 16)));
        }
    }
}
