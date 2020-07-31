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
                game: _game,
                background: _game.Content.Load<Texture2D>("SpriteSheets/Background/finalNight"),
                buttonsTexture: _game.Content.Load<Texture2D>("SpriteSheets/Buttons/Buttons")
            );
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (Utility.isMouseLeftButtonClicked(mouseState, _prevMouseState))
                mouseClicked(Utility.MouseClickRectangle(mouseState));

            _prevMouseState = mouseState;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _mainMenu.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void mouseClicked(Rectangle mouseClickRectangle)
        {
            foreach (Button button in _mainMenu.GetButtons())
            {
                if (mouseClickRectangle.Intersects(button.CollisionRectangle))
                    button.ExecuteOnClickAction();
            }
        }
    }
}
