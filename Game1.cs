using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RogueSimulator.Classes.Characters;
using RogueSimulator.Classes.Level;
using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Classes.Mechanics.Menu;

namespace RogueSimulator
{
    public enum GameState
    {
        MAIN_MENU,
        LEVEL_SELECTOR,
        LOADING,
        PLAYING,
        PAUSED,
    }

    public class Game1 : Game
    {
        private GameState _gameState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Character _player;
        private BaseLevel _currentLevel;
        private Camera2D _camera;
        private Menu _menu;
        private MouseState _previousMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _gameState = GameState.MAIN_MENU;
            _camera = new Camera2D(GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _player = new Character(Content.Load<Texture2D>("SpriteSheets/Wizard/allActions"), new Vector2 { X = 150, Y = 150 });
            _currentLevel = new Level1(
                texture: Content.Load<Texture2D>("SpriteSheets/Tileset/jungleTileSet"),
                background: Content.Load<Texture2D>("SpriteSheets/Background/background"),
                viewport: _graphics.GraphicsDevice.Viewport
            );
            _menu = new MainMenu(
                viewport: _graphics.GraphicsDevice.Viewport,
                background: Content.Load<Texture2D>("SpriteSheets/Background/finalNight"),
                buttonsTexture: Content.Load<Texture2D>("SpriteSheets/Buttons/Buttons")
            );

            _currentLevel.Create();
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Utility.IsKeyPressed(Keys.Back))
                Exit();
            if (_previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                mouseClicked(mouseState.X, mouseState.Y);

            // TODO: Add your update logic here
            switch (_gameState)
            {
                case GameState.PLAYING:
                    _player.Update(gameTime, _currentLevel);
                    _camera.UpdatePosition(_player.GetPosition(), _currentLevel);
                    break;
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

            switch (_gameState)
            {
                case GameState.MAIN_MENU:
                    _menu.Draw(_spriteBatch);
                    break;
                case GameState.PLAYING:
                    _currentLevel.Draw(_spriteBatch);
                    _player.Draw(_spriteBatch);
                    break;
            }

            // System.Console.WriteLine("position.y: " + _player.CollisionRectangle.Y);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void mouseClicked(int x, int y)
        {
            if (_gameState != GameState.MAIN_MENU)
                return;

            Rectangle mouseClickRectangle = new Rectangle(x, y, 10, 10);

            foreach (Button button in _menu.GetButtons())
            {
                if (mouseClickRectangle.Intersects(button.CollisionRectangle))
                {
                    switch (button.ButtonAction)
                    {
                        case ButtonAction.START:
                            _gameState = GameState.PLAYING;
                            break;
                        case ButtonAction.QUIT:
                            Exit();
                            break;
                    }
                }
            }
        }
    }
}
