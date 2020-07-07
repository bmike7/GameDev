using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Level
    {
        private const int LEVEL_HEIGHT = 2;
        private const int LEVEL_WIDTH = 10;
        private Texture2D _texture;

        private byte[,] _levelDesign = new byte[LEVEL_HEIGHT, LEVEL_WIDTH]
        {
            {0,0,0,0,0,0,1,0,0,0},
            {1,1,1,1,1,1,1,1,1,1},
        };

        private Tile[,] _tiles = new Tile[LEVEL_HEIGHT, LEVEL_WIDTH];

        public Level(Texture2D texture)
        {
            _texture = texture;
        }

        public void CreateWorld()
        {
            for (int line = 0; line < LEVEL_HEIGHT; line++)
            {
                for (int block = 0; block < LEVEL_WIDTH; block++)
                {
                    if (_levelDesign[line, block] == 1)
                        _tiles[line, block] = new Tile(_texture, new Vector2(block * 30, line * 30 + 400));
                }
            }
        }

        public void DrawLevel(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                if (tile != null)
                    tile.Draw(spriteBatch);
            }
        }
    }
}