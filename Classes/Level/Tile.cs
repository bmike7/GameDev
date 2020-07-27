using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public class Tile : ICollidable, Interfaces.IDrawable
    {
        public static int SIZE = 50;
        private Texture2D _texture;
        private Rectangle _spriteSheetRectangle;
        public Rectangle CollisionRectangle { get; private set; }

        public Tile(Texture2D texture, Vector2 position, Rectangle spriteSheetRectangle)
        {
            _texture = texture;
            _spriteSheetRectangle = spriteSheetRectangle;
            CollisionRectangle = new Rectangle(
                x: (int)position.X,
                y: (int)position.Y,
                width: spriteSheetRectangle.Width,
                height: spriteSheetRectangle.Height
            );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: new Vector2(CollisionRectangle.X, CollisionRectangle.Y),
                sourceRectangle: _spriteSheetRectangle,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: (float)SIZE / CollisionRectangle.Height,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
        }
    }
}
