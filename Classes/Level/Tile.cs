using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Tile<T> where T : System.Enum
    {
        public static int SIZE = 50;
        private Texture2D _texture;
        private Rectangle _tileRectangle;
        private T _type;
        public Vector2 Position { get; set; }

        public Tile(Texture2D texture, Vector2 position, Rectangle tileRectangle, T type)
        {
            _texture = texture;
            Position = position;
            _tileRectangle = tileRectangle;
            _type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                Position,
                _tileRectangle,
                Color.White,
                0,
                new Vector2(0, 0),
                (float)SIZE / _tileRectangle.Height,
                SpriteEffects.None,
                0
            );
        }
    }
}