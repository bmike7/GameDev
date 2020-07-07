using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Tile
    {
        private Texture2D _texture;
        public Vector2 Position { get; set; }

        public Tile(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                Position,
                new Rectangle(159, 31, 31, 31),
                Color.White
            );
        }
    }
}