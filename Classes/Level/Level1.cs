using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Level1 : BaseLevel
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
        private const int BACKGROUND_PIXEL_WIDTH = 640;
        private int[,] _levelDesign = new int[NUMBER_OF_LINES, NUMBER_OF_COLUMNS]
        {
            {0,0,0,0,0,0,4,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,2,3,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        public Level1(Texture2D texture, Texture2D background, Texture2D portalTexture, Viewport viewport)
            : base(
                texture: texture,
                background: background,
                portalTexture: portalTexture,
                viewport: viewport,
                size: NUMBER_OF_COLUMNS * Tile.SIZE
            )
        { }

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
    }
}
