using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Mechanics.Menu;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class LevelSelectorState : IState
    {
        private LevelMenu _levelMenu;
        private MouseState _prevMouseState;
        private Game1 _game;

        public LevelSelectorState(Game1 game)
        {
            _game = game;
        }
        public void LoadContent()
        {
            if (_levelMenu != null) return;

            _levelMenu = new LevelMenu(
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
            _levelMenu.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void mouseClicked(int x, int y)
        {
            Rectangle mouseClickRectangle = new Rectangle(x, y, 10, 10);

            //index represents the number of the level that will be selected
            int index = 1;
            foreach (Button button in _levelMenu.GetButtons())
            {
                if (mouseClickRectangle.Intersects(button.CollisionRectangle))
                {
                    switch (index)
                    {
                        case 1:
                            _game.SelectedLevel = "level1";
                            break;
                        case 2:
                            _game.SelectedLevel = "level2";
                            break;
                        default:
                            _game.SelectedLevel = "level1";
                            break;
                    }
                    _game.ChangeGameState(GameState.PLAYING);
                    break;
                }
                index++;
            }
        }
    }
}
