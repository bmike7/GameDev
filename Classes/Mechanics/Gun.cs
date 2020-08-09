using System.Collections.Generic;

namespace RogueSimulator.Classes.Mechanics
{
    public class Gun
    {
        private List<Bullet> _ammo;
        public Gun(int numberOfBullets = 50)
        {
            _ammo = new List<Bullet>();

            for (int amount = 0; amount < numberOfBullets; amount++)
                _ammo.Add(new Bullet());
        }

        public void FireBullet()
        {
            System.Console.WriteLine("Pang");
        }
    }
}
