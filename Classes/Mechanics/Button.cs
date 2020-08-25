using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Button : ICollidable, Interfaces.IDrawable, IClickable
    {
        private readonly Action _onClickAction;
        private readonly Texture2D _texture;
        private readonly float _scale;
        private Rectangle _buttonRectangle;
        private Vector2 _position;

        public Button(Action onClickAction, Texture2D buttonTexture, Vector2 position, Rectangle buttonSpriteRectangle, int height = 45)
        {
            _onClickAction = onClickAction;
            _texture = buttonTexture;
            _position = position;
            _buttonRectangle = buttonSpriteRectangle;
            _scale = (float)height / buttonSpriteRectangle.Height;
            CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(buttonSpriteRectangle.Width * _scale), (int)(buttonSpriteRectangle.Height * _scale));
        }
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

        public void ExecuteOnClickAction() => _onClickAction();
    }
}
