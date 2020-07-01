using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes
{
    public class Character
    {
        private Position _position;
        private Texture2D _texture;
        private Action _idle = new Action(87, 1035, 58, 87, 230, 6);

        public Character(Texture2D texture, Position pos)
        {
            _texture = texture;
            _position = pos;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y), _idle.getActionFrame(), Color.White);
        }
    }
}