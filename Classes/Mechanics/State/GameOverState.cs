using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics.Menu;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class GameOverState : IState
    {
        private GameOverMenu _gameOverMenu;
        private MouseState _prevMouseState;
        private Game1 _game;
        public GameOverState(Game1 game)
        {
            _game = game;
        }

        public void LoadContent()
        {
            if (_gameOverMenu != null) return;

            _gameOverMenu = new GameOverMenu(
                game: _game,
                background: _game.Content.Load<Texture2D>("SpriteSheets/Background/GameOverPaper"),
                buttonsTexture: _game.Content.Load<Texture2D>("SpriteSheets/Buttons/GameOverMenuButtons")
            );
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (Utility.isMouseLeftButtonClicked(mouseState, _prevMouseState))
                Utility.ClickButtonIfMouseclickIntersects(mouseState, _gameOverMenu.GetButtons().ToArray());

            _prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _gameOverMenu.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
