using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Bullet : Interfaces.IDrawable, IUpdatable, ICollidable
    {
        private Texture2D _texture;
        private Vector2 _position;
        public Bullet(Texture2D bulletTexture, Vector2 from, Vector2 to)
        {
            _texture = bulletTexture;
            _position = from;
            System.Console.WriteLine("Pang");
        }
        public Rectangle CollisionRectangle { get; set; }

        public void Update(GameTime gameTime)
        {
            System.Console.WriteLine("Flying bullet....");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
