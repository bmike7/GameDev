using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics.Menu;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class PausedState : IState
    {
        private PausedMenu _pauseMenu;
        private MouseState _prevMouseState;
        private Game1 _game;

        public PausedState(Game1 game)
        {
            _game = game;
        }

        public void LoadContent()
        {
            if (_pauseMenu != null) return;

            _pauseMenu = new PausedMenu(
                game: _game,
                background: _game.Content.Load<Texture2D>("SpriteSheets/Background/finalNight"),
                buttonsTexture: _game.Content.Load<Texture2D>("SpriteSheets/Buttons/PauseMenuButtons")
            );
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (Utility.isMouseLeftButtonClicked(mouseState, _prevMouseState))
                Utility.MouseClicked(mouseState, _pauseMenu.GetButtons().ToArray());

            _prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _pauseMenu.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
