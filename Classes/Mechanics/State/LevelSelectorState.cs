using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics.Menu;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class LevelSelectorState : IState
    {
        private readonly Game1 _game;
        private LevelMenu _levelMenu;
        private MouseState _prevMouseState;

        public LevelSelectorState(Game1 game)
        {
            _game = game;
        }
        public void LoadContent()
        {
            if (_levelMenu != null) return;

            _levelMenu = new LevelMenu(
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
            _levelMenu.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void mouseClicked(Rectangle mouseClickRectangle)
        {
            //index represents the number of the level that will be selected
            int index = 0;
            foreach (Button button in _levelMenu.GetButtons())
            {
                if (mouseClickRectangle.Intersects(button.CollisionRectangle))
                {
                    _game.CurrentPlayingState.SelectedLevel = (LevelType)index;
                    button.ExecuteOnClickAction();
                }
                index++;
            }
        }
    }
}
