using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics.Menu;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class MainMenuState : IState
    {
        private MainMenu _mainMenu;
        private MouseState _prevMouseState;
        private Game1 _game;

        public MainMenuState(Game1 game)
        {
            _game = game;
        }

        public void LoadContent()
        {
            if (_mainMenu != null) return;

            _mainMenu = new MainMenu(
                viewport: _game.GraphicsDevice.Viewport,
                background: _game.Content.Load<Texture2D>("SpriteSheets/Background/finalNight"),
                buttonsTexture: _game.Content.Load<Texture2D>("SpriteSheets/Buttons/Buttons")
            );
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (_prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                mouseClicked(mouseState.X, mouseState.Y);

            _prevMouseState = mouseState;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _mainMenu.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void mouseClicked(int x, int y)
        {
            Rectangle mouseClickRectangle = new Rectangle(x, y, 10, 10);

            foreach (Button button in _mainMenu.GetButtons())
            {
                if (mouseClickRectangle.Intersects(button.CollisionRectangle))
                {
                    switch (button.ButtonAction)
                    {
                        case ButtonAction.START:
                            _game.ChangeGameState(GameState.PLAYING);
                            break;
                        case ButtonAction.QUIT:
                            _game.Exit();
                            break;
                    }
                }
            }
        }
    }
}
