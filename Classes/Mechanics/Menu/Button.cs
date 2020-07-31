using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class Button : ICollidable, Interfaces.IDrawable, IClickable
    {
        private Texture2D _texture;
        private Rectangle _buttonRectangle;
        private Vector2 _position;
        private float _scale;

        public Button(ButtonAction buttonAction, Texture2D buttonTexture, Vector2 position, Rectangle buttonSpriteRectangle, int height = 45)
        {
            _texture = buttonTexture;
            _position = position;
            _buttonRectangle = buttonSpriteRectangle;
            _scale = (float)height / buttonSpriteRectangle.Height;
            ButtonAction = buttonAction;
            CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(buttonSpriteRectangle.Width * _scale), (int)(buttonSpriteRectangle.Height * _scale));
        }

        public ButtonAction ButtonAction { get; set; }
        public Rectangle CollisionRectangle { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: _position,
                sourceRectangle: _buttonRectangle,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(0, 0),
                scale: _scale,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
        }

        public void UpdatePosition(Vector2 newPos) => _position = newPos;
    }
}
