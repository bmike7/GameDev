using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public BaseLevel(Texture2D texture, Texture2D background, Texture2D portalTexture, Viewport viewport, int size)
        {
            _texture = texture;
            _background = background;
            _portalTexture = portalTexture;
            _viewport = viewport;
            Size = size;
        }

        public int Size { get; private set; }
        public FinisherPortal FinisherPortal { get; protected set; }

        // In the create function the tiles should be added to the _tiles list.
        public abstract void Create();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int amountOfBackgrounds = Size / _background.Width + 1;
            for (int backgroundNumber = 0; backgroundNumber < amountOfBackgrounds; backgroundNumber++)
                spriteBatch.Draw(_background, new Vector2(backgroundNumber * _background.Width, 0), Color.White);
            foreach (Tile tile in _tiles)
                tile.Draw(spriteBatch);
            if (FinisherPortal != null)
                FinisherPortal.Draw(spriteBatch);
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
