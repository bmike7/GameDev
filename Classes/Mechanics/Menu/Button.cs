using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class Button : Interfaces.IDrawable
    {
        public static int WIDTH = 150;
        private Texture2D _texture;
        private Rectangle _bottonRectangle;
        private Vector2 _position;

        public Button(Texture2D buttonTexture, Vector2 position, Rectangle buttonSpriteRectangle)
        {
            _texture = buttonTexture;
            _position = position;
            _bottonRectangle = buttonSpriteRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: _position,
                sourceRectangle: _bottonRectangle,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: (float)WIDTH / _bottonRectangle.Width,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
        }
    }
}
