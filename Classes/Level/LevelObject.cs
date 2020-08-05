using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Level
{
    public abstract class LevelObject : ICollidable, Interfaces.IDrawable
    {
        private Texture2D _texture;
        private Rectangle _spritesheetRectangle;

        public LevelObject(Texture2D texture, Vector2 position, Rectangle spritesheetRectangle, int height)
        {
            _texture = texture;
            _spritesheetRectangle = spritesheetRectangle;
            CollisionRectangle = new Rectangle(
                x: (int)position.X,
                y: (int)position.Y,
                width: spritesheetRectangle.Width,
                height: spritesheetRectangle.Height
            );
            Height = height;
        }

        public Rectangle CollisionRectangle { get; private set; }
        public int Height { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: new Vector2(CollisionRectangle.X, CollisionRectangle.Y),
                sourceRectangle: _spritesheetRectangle,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: (float)Height / CollisionRectangle.Height,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
        }

    }
}
