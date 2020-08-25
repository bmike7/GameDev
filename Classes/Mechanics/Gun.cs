using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics
{
    public class Gun
    {
        private readonly Texture2D _bulletTexture;
        public Gun(Texture2D bulletTexture, int numberOfBullets = 10, int maxAmmo = 10)
        {
            _bulletTexture = bulletTexture;
            Ammo = numberOfBullets;
            MaxAmmo = maxAmmo;
        }
        public int Ammo { get; set; }
        public int MaxAmmo { get; set; }

        public Bullet FireBullet(double initialFireTime, Vector2 from, MovementDirection direction)
        {
            if (Ammo < 1) return null;

            Ammo--;
            return new Bullet(initialFireTime, _bulletTexture, from, direction);
        }
    }
}
