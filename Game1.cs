using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RogueSimulator.Classes.Characters;
using RogueSimulator.Classes.Level;
using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Classes.Mechanics.Menu;

namespace RogueSimulator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Character _player;
        private BaseLevel _currentLevel;
        private Camera2D _camera;
        private Menu _menu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Utility.IsKeyPressed(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // _player.Update(gameTime, _currentLevel);
            // _camera.UpdatePosition(_player.GetPosition(), _currentLevel);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

            // _currentLevel.Draw(_spriteBatch);
            // _player.Draw(_spriteBatch);
            _menu.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
