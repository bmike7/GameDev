using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RogueSimulator.Classes.Characters;
using RogueSimulator.Classes.Level;
using RogueSimulator.Classes.Mechanics.Menu;
using RogueSimulator.Classes.Mechanics.State;
using RogueSimulator.Interfaces;

namespace RogueSimulator
{
    public class Game1 : Game
    {
        private GameState _gameState;
        private Dictionary<GameState, IState> _gameStates;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameState = GameState.MAIN_MENU;
            _gameStates = new Dictionary<GameState, IState>();

            _gameStates.Add(GameState.MAIN_MENU, new MainMenuState(this));
            _gameStates.Add(GameState.PLAYING, new PlayingState(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (_spriteBatch == null)
                _spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameStates[_gameState].LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Utility.IsKeyPressed(Keys.Escape))
                Exit();

            _gameStates[_gameState].Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _gameStates[_gameState].Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        public void ChangeGameState(GameState gameState)
        {
            _gameState = gameState;
            LoadContent();
        }
    }
}
