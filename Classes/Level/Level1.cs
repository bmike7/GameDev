using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public class Level1 : BaseLevel, Interfaces.IDrawable
    {
        private enum TileType
        {
            NONE,
            GROUND,
            LEFTWALL,
            RIGHTWALL,
            BLOCK
        }

        private Dictionary<TileType, Rectangle> _tileTypes = new Dictionary<TileType, Rectangle> {
            {TileType.GROUND, new Rectangle(90, 30, 30, 30)},
            {TileType.LEFTWALL, new Rectangle(64, 56, 30, 30)},
            {TileType.RIGHTWALL, new Rectangle(115, 56, 30, 30)},
            {TileType.BLOCK, new Rectangle(160, 32, 30, 30)},
        };

        private const int NUMBER_OF_LINES = 3;
        private const int NUMBER_OF_COLUMNS = 30;
        private const int NEAR_DISTANCE = 200;
        private const int BACKGROUND_PIXEL_WIDTH = 640;
        private int[,] _levelDesign = new int[NUMBER_OF_LINES, NUMBER_OF_COLUMNS]
        {
            {0,0,0,0,0,0,4,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,2,3,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };
        private List<Tile> _tiles = new List<Tile>();

        public Level1(Texture2D texture, Texture2D background, Viewport viewport)
        {
            _texture = texture;
            _background = background;
            _viewport = viewport;
        }

        public override void Create()
        {
            for (int line = 0; line < NUMBER_OF_LINES; line++)
            {
                for (int block = 0; block < NUMBER_OF_COLUMNS; block++)
                {
                    int startY = _viewport.Height - NUMBER_OF_LINES * Tile.SIZE;
                    Vector2 position = new Vector2(block * Tile.SIZE, line * Tile.SIZE + startY);

                    TileType type = (TileType)_levelDesign[line, block];

                    if (type != TileType.NONE)
                    {
                        Tile newTile = new Tile(_texture, position, _tileTypes[type]);
                        _tiles.Add(newTile);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int amountOfBackgrounds = (NUMBER_OF_COLUMNS * Tile.SIZE) / BACKGROUND_PIXEL_WIDTH + 1;
            for (int backgroundNumber = 0; backgroundNumber < amountOfBackgrounds; backgroundNumber++)
                spriteBatch.Draw(_background, new Vector2(backgroundNumber * BACKGROUND_PIXEL_WIDTH, 0), Color.White);
            foreach (Tile tile in _tiles)
                tile.Draw(spriteBatch);
        }

        public override ICollidable[] GetNearCollidableBlocks(Vector2 characterPosition)
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

        public override int GetSize() => NUMBER_OF_COLUMNS * Tile.SIZE;
    }
}
