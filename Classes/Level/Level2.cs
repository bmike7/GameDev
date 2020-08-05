using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private const int BACKGROUND_PIXEL_WIDTH = 640;
        private int[,] _levelDesign = new int[NUMBER_OF_LINES, NUMBER_OF_COLUMNS]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,8,0,8,0,0,0,0,0,0,0,0,0,0,0,0,4,7,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0},
            {0,0,0,0,0,4,7,0,0,8,0,0,0,0,0,0,0,0,0,8,0,0,2,5,0,0,0,0,0,9,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,3,6,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,3,6,1,1,0,0,0,0,0,0,0,0,8,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        public Level2(Texture2D texture, Texture2D background, Texture2D portalTexture, Viewport viewport)
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

                    if (_levelDesign[line, block] == -1)
                        FinisherPortal = new FinisherPortal(_portalTexture, position);
                    else
                    {
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
}
