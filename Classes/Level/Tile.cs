using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Tile<T> where T : System.Enum
    {
        public static int SIZE = 50;
        private Texture2D _texture;
        private T _type;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }

        public Tile(Texture2D texture, Vector2 position, Rectangle tileRectangle, T type)
        {
            _texture = texture;
            Position = position;
            Rectangle = tileRectangle;
            _type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: Position,
                sourceRectangle: Rectangle,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: (float)SIZE / Rectangle.Height,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
        }
    }
}