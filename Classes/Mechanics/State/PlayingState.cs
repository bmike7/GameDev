using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Characters;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class PlayingState : IState
    {
        private Character _player;
        private BaseLevel _currentLevel;
        private Camera2D _camera;
        private Game1 _game;

        public PlayingState(Game1 game)
        {
            _game = game;
        }

        public void LoadContent()
        {
            _player = new Character(_game.Content.Load<Texture2D>("SpriteSheets/Wizard/allActions"), new Vector2 { X = 150, Y = 150 });
            _currentLevel = LevelFactory.CreateLevel(_game);
            _camera = new Camera2D(_game.GraphicsDevice.Viewport);

            _currentLevel.Create();
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime, _currentLevel);
            _camera.UpdatePosition(_player.GetPosition(), _currentLevel);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            _currentLevel.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
