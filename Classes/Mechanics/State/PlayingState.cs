using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Characters;
using RogueSimulator.Classes.Level;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.State
{
    public class PlayingState : IState
    {
        private const int PAUSE_BUTTON_HEIGHT = 20;
        private const int PAUSE_BUTTON_OFFSET = 40;
        private Character _player;
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
            _levelFactory.RegisterLevel(LevelType.LEVEL1, () => new Level1(
                        texture: game.Content.Load<Texture2D>("SpriteSheets/Tileset/jungleTileSet"),
                        background: game.Content.Load<Texture2D>("SpriteSheets/Background/background"),
                        viewport: game.GraphicsDevice.Viewport
                    ));
            _levelFactory.RegisterLevel(LevelType.LEVEL2, () => new Level2(
                        texture: game.Content.Load<Texture2D>("SpriteSheets/Tileset/jungleTileSet"),
                        background: game.Content.Load<Texture2D>("SpriteSheets/Background/background"),
                        viewport: game.GraphicsDevice.Viewport
                    ));
        }

        public void LoadContent()
        {
            _player = new Character(_game.Content.Load<Texture2D>("SpriteSheets/Wizard/allActions"), _game.CurrentPlayingState.Movement.Position);
            _currentLevel = _levelFactory.CreateLevel(_game.CurrentPlayingState.SelectedLevel);
            _camera = new Camera2D(_game.GraphicsDevice.Viewport);
            _pauseButton = new Button(
                onClickAction: () =>
                {
                    _game.CurrentPlayingState.Movement = _player.GetMovement();
                    _game.ChangeGameState(GameState.PAUSED);
                },
                buttonTexture: _game.Content.Load<Texture2D>("SpriteSheets/Buttons/PauseButton"),
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
