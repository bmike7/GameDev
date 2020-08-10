using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics
{
    public class Gun
    {
        private Texture2D _bulletTexture;
        public Gun(Texture2D bulletTexture, int numberOfBullets = 50)
        {
            _bulletTexture = bulletTexture;
            Ammo = numberOfBullets;
        }
        public int Ammo { get; set; }

        public Bullet FireBullet(Vector2 from, Vector2 to)
        {
            if (Ammo < 1) return null;

            return new Bullet(_bulletTexture, from, to);
        }
    }
}
