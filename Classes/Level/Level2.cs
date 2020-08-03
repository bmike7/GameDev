using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public class Level2 : BaseLevel
    {
        private enum TileType
        {
            NONE,
            GROUND,
            LEFTWALL,
            LEFTWALL_GROUND_LEVEL,
            LEFT_TOP_CORNER,
            RIGHTWALL,
            RIGHTWALL_GROUND_LEVEL,
            RIGHT_TOP_CORNER,
            BLOCK,
            LEFTSIDE_FLOATING_BLOCK,
            RIGHTSIDE_FLOATING_BLOCK,
        }

        private Dictionary<TileType, Rectangle> _tileTypes = new Dictionary<TileType, Rectangle> {
            {TileType.GROUND, new Rectangle(90, 30, 30, 30)},
            {TileType.LEFTWALL, new Rectangle(64, 56, 30, 30)},
            {TileType.LEFTWALL_GROUND_LEVEL, new Rectangle(257, 273, 30, 30)},
            {TileType.LEFT_TOP_CORNER, new Rectangle(64, 32, 30, 30)},
            {TileType.RIGHTWALL, new Rectangle(115, 56, 30, 30)},
            {TileType.RIGHTWALL_GROUND_LEVEL, new Rectangle(193, 273, 30, 30)},
            {TileType.RIGHT_TOP_CORNER, new Rectangle(114, 32, 30, 30)},
            {TileType.BLOCK, new Rectangle(160, 32, 30, 30)},
            {TileType.LEFTSIDE_FLOATING_BLOCK, new Rectangle(128, 128, 30, 30)},
            {TileType.RIGHTSIDE_FLOATING_BLOCK, new Rectangle(192, 128, 30, 30)}
        };

        private const int NUMBER_OF_LINES = 5;
        private const int NUMBER_OF_COLUMNS = 50;
        private const int NEAR_DISTANCE = 200;
        private const int BACKGROUND_PIXEL_WIDTH = 640;
        private int[,] _levelDesign = new int[NUMBER_OF_LINES, NUMBER_OF_COLUMNS]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,8,0,8,0,0,0,0,0,0,0,0,0,0,0,0,4,7,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,4,7,0,0,8,0,0,0,0,0,0,0,0,0,8,0,0,2,5,0,0,0,0,0,9,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,3,6,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,3,6,1,1,0,0,0,0,0,0,0,0,8,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };
        private List<Tile> _tiles = new List<Tile>();

        public Level2(Texture2D texture, Texture2D background, Viewport viewport)
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
