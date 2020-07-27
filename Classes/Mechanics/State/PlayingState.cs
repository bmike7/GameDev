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

        public PlayingState(Character player, BaseLevel level, Viewport viewport)
        {
            _player = player;
            _currentLevel = level;
            _camera = new Camera2D(viewport);

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
