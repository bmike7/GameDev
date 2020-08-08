using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public abstract class BaseLevel : Interfaces.IDrawable
    {
        private const int NEAR_DISTANCE = 200;
        protected Texture2D _texture;
        protected Texture2D _portalTexture;
        protected Texture2D _background;
        protected Viewport _viewport;
        protected List<Tile> _tiles = new List<Tile>();
        protected Game1 _game;

        public BaseLevel(Game1 game, Texture2D texture, Texture2D background, Texture2D portalTexture, Viewport viewport, int size)
        {
            _game = game;
            _texture = texture;
            _background = background;
            _portalTexture = portalTexture;
            _viewport = viewport;
            Size = size;

            Player = new Player(Utility.LoadTexture(game, Player.ASSET_NAME), game.CurrentPlayingState.Movement.Position);
            Camera = new Camera2D(game.GraphicsDevice.Viewport);
            Characters = new List<Character>();
        }

        public int Size { get; private set; }
        public Player Player { get; protected set; }
        public Camera2D Camera { get; protected set; }
        public List<Character> Characters { get; protected set; }
        public FinisherPortal FinisherPortal { get; protected set; }

        // In the create function the tiles should be added to the _tiles list.
        public abstract void Create();

        public virtual void Update(GameTime gameTime)
        {
            foreach (Character character in Characters)
                character.Update(gameTime, this);

            Player.Update(gameTime, this);
            Camera.UpdatePosition(Player.GetPosition(), this);

            if (FinisherPortal != null && Player.CollisionRectangle.Intersects(FinisherPortal.CollisionRectangle))
                _game.ChangeGameState(GameState.LEVEL_SELECTOR);
            if (Player.GetPosition().Y > _game.GraphicsDevice.Viewport.Height)
                _game.ChangeGameState(GameState.GAME_OVER);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Camera.GetViewMatrix());

            int amountOfBackgrounds = Size / _background.Width + 1;
            for (int backgroundNumber = 0; backgroundNumber < amountOfBackgrounds; backgroundNumber++)
                spriteBatch.Draw(_background, new Vector2(backgroundNumber * _background.Width, 0), Color.White);

            foreach (Tile tile in _tiles)
                tile.Draw(spriteBatch);

            foreach (Character character in Characters)
                character.Draw(spriteBatch);

            Player.Draw(spriteBatch);

            if (FinisherPortal != null)
                FinisherPortal.Draw(spriteBatch);

            spriteBatch.End();
        }

        public virtual ICollidable[] GetNearCollidableBlocks(Vector2 characterPosition)
        {
            List<ICollidable> collisionBlocks = new List<ICollidable>();
            foreach (ICollidable tile in _tiles)
            {
                float distance = Vector2.Distance(characterPosition, tile.GetPosition());
                if (distance < NEAR_DISTANCE)
                    collisionBlocks.Add(tile);
            }
            return collisionBlocks.ToArray();
        }
    }
}
