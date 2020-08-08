using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class PlayingState : IState
    {
        private const int PAUSE_BUTTON_HEIGHT = 20;
        private const int PAUSE_BUTTON_OFFSET = 40;
        private Player _player;
        private BaseLevel _currentLevel;
        private Camera2D _camera;
        private Game1 _game;
        private LevelFactory _levelFactory;
        private Button _pauseButton;
        private MouseState _prevMouseState;

        public PlayingState(Game1 game)
        {
            _game = game;

            _levelFactory = new LevelFactory();
            _levelFactory.RegisterLevel(LevelType.LEVEL1, () => new Level1(game));
            _levelFactory.RegisterLevel(LevelType.LEVEL2, () => new Level2(game));
        }

        public void LoadContent()
        {
            _player = new Player(_game.Content.Load<Texture2D>(Player.ASSET_NAME), _game.CurrentPlayingState.Movement.Position);
            _currentLevel = _levelFactory.LoadLevel(_game.CurrentPlayingState.SelectedLevel);
            _camera = new Camera2D(_game.GraphicsDevice.Viewport);
            _pauseButton = new Button(
                onClickAction: () =>
                {
                    _game.CurrentPlayingState.Movement = _player.GetMovement();
                    _game.ChangeGameState(GameState.PAUSED);
                },
                buttonTexture: Utility.LoadTexture(_game, "SpriteSheets/Buttons/PauseButton"),
                position: new Vector2(_game.GraphicsDevice.Viewport.Width - PAUSE_BUTTON_OFFSET, PAUSE_BUTTON_OFFSET - PAUSE_BUTTON_HEIGHT),
                buttonSpriteRectangle: new Rectangle(3, 2, 10, 10),
                height: 20
            );

            _game.CurrentPlayingState.ResetMovement();
            _currentLevel.Create();
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (Utility.isMouseLeftButtonClicked(mouseState, _prevMouseState))
                Utility.ClickButtonIfMouseclickIntersects(mouseState, new Button[] { _pauseButton });

            _player.Update(gameTime, _currentLevel);
            if (_currentLevel.FinisherPortal != null && _player.CollisionRectangle.Intersects(_currentLevel.FinisherPortal.CollisionRectangle))
                _game.ChangeGameState(GameState.LEVEL_SELECTOR);
            if (_player.GetPosition().Y > _game.GraphicsDevice.Viewport.Height)
                _game.ChangeGameState(GameState.GAME_OVER);

            foreach (Character character in _currentLevel.Characters)
                character.Update(gameTime, _currentLevel);

            _camera.UpdatePosition(_player.GetPosition(), _currentLevel);
            _pauseButton.UpdatePosition(getNewPauseButtonPos());

            _prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            _currentLevel.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            _pauseButton.Draw(spriteBatch);
            spriteBatch.End();
        }

        private Vector2 getNewPauseButtonPos() =>
            new Vector2(
                x: _camera.Position.X + _game.GraphicsDevice.Viewport.Width - PAUSE_BUTTON_OFFSET,
                y: PAUSE_BUTTON_OFFSET - PAUSE_BUTTON_HEIGHT
            );
    }
}
