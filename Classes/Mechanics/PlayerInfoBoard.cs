using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RogueSimulator.Classes.Entity;
using RogueSimulator.Interfaces;

namespace RogueSimulator.Classes.Mechanics
{
    public class PlayerInfoBoard : Interfaces.IDrawable
    {
        private const int STATBAR_WIDTH = 120;
        private const int STATBAR_HEIGHT = 15;
        private readonly Player _player;
        private readonly Texture2D _whitePixel;

        public PlayerInfoBoard(GraphicsDevice graphicsDevice, Player player)
        {
            _player = player;
            _whitePixel = new Texture2D(graphicsDevice, 1, 1);
            _whitePixel.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int barWidth = STATBAR_WIDTH - 2;
            //healthbarbackground
            spriteBatch.Draw(_whitePixel, new Rectangle(10, 10, STATBAR_WIDTH, STATBAR_HEIGHT), Color.White);
            //healtbar
            spriteBatch.Draw(_whitePixel, new Rectangle(
                    x: 11,
                    y: 11,
                    width: (int)((float)_player.Health / _player.MaxHealth * barWidth),
                    height: STATBAR_HEIGHT - 2
                ), Color.Green);
            //ammobackground
            spriteBatch.Draw(_whitePixel, new Rectangle(10, 35, STATBAR_WIDTH, STATBAR_HEIGHT), Color.White);
            //ammobar
            spriteBatch.Draw(_whitePixel, new Rectangle(
                    x: 11,
                    y: 36,
                    width: (int)((float)_player.Gun.Ammo / _player.Gun.MaxAmmo * barWidth),
                    height: STATBAR_HEIGHT - 2
                ), Color.Red);
        }
    }
}
