using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class Bullet : Interfaces.IDrawable, IUpdatable, ICollidable
    {
        public Rectangle CollisionRectangle { get; set; }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

    }
}
