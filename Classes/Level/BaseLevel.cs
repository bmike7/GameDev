using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Classes.Mechanics;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public abstract class BaseLevel : Interfaces.IDrawable, IUpdatable
    {
        private const int NEAR_DISTANCE = 200;
        protected Game1 _game;
        protected Texture2D _texture;
        protected Texture2D _portalTexture;
        protected Texture2D _background;
        protected Viewport _viewport;
        protected List<Tile> _tiles;
        private List<Bullet> _shotsFired;

        public BaseLevel(Game1 game, Texture2D texture, Texture2D background, Texture2D portalTexture, int size)
        {
            _game = game;
            _texture = texture;
            _background = background;
            _portalTexture = portalTexture;
            _viewport = game.GraphicsDevice.Viewport;
            _tiles = new List<Tile>();
            _shotsFired = new List<Bullet>();
            Size = size;

            Player = new Player(
                texture: Utility.LoadTexture(game, Player.ASSET_NAME),
                position: game.CurrentPlayingState.Movement.Position,
                bulletTexture: Utility.LoadTexture(game, "SpriteSheets/Wizard/wizardBullet")
            );
            Camera = new Camera2D(_viewport);
            Characters = game.CurrentPlayingState.Characters.Count > 0 ? game.CurrentPlayingState.Characters : new List<Character>();
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

            foreach (Bullet bullet in _shotsFired)
                bullet.Update(gameTime);

            Player.Update(gameTime, this);
            Camera.UpdatePosition(Player.GetPosition(), this);

            if (Player.GetPosition().Y > _viewport.Height)
                _game.ChangeGameState(GameState.GAME_OVER);

            checkAndHandleCollisions();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Camera.GetViewMatrix());
            drawBackGround(spriteBatch);

            foreach (Tile tile in _tiles)
                tile.Draw(spriteBatch);

            foreach (Character character in Characters)
                character.Draw(spriteBatch);

            foreach (Bullet bullet in _shotsFired)
                bullet.Draw(spriteBatch);

            Player.Draw(spriteBatch);
            FinisherPortal?.Draw(spriteBatch);
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

        public void AddFiredShot(Bullet bullet)
        {
            if (bullet != null) _shotsFired.Add(bullet);
        }

        private void drawBackGround(SpriteBatch spriteBatch)
        {
            int amountOfBackgrounds = Size / _background.Width + 1;
            for (int backgroundNumber = 0; backgroundNumber < amountOfBackgrounds; backgroundNumber++)
                spriteBatch.Draw(_background, new Vector2(backgroundNumber * _background.Width, 0), Color.White);
        }

        private void checkAndHandleCollisions()
        {
            List<ICollidable> collidablesToRemove = new List<ICollidable>();

            foreach (Bullet bullet in _shotsFired)
            {
                foreach (Character character in Characters)
                {
                    if (bullet.CollisionRectangle.Intersects(character.CollisionRectangle))
                    {
                        collidablesToRemove.Add(bullet);
                        collidablesToRemove.Add(character);
                    }
                }
            }

            foreach (ICollidable collidable in collidablesToRemove)
            {
                if (collidable is Character) Characters.Remove((Character)collidable);
                if (collidable is Bullet) _shotsFired.Remove((Bullet)collidable);
            }

            if (FinisherPortal != null && Player.CollisionRectangle.Intersects(FinisherPortal.CollisionRectangle))
                _game.ChangeGameState(GameState.LEVEL_SELECTOR);
        }
    }
}
