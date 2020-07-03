using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Level
    {
        private Texture2D _texture;
        private Tile _tile;

        public Level(Texture2D texture)
        {
            _texture = texture;
        }

        //Tile pos: x:159,  y:31,  w:33,  h:33

        public void CreateWorld()
        {
            _tile = new Tile(_texture, new Vector2(0, 0));
        }

        public void DrawLevel(SpriteBatch spriteBatch)
        {
            _tile.Draw(spriteBatch);
        }
    }
}