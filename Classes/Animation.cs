using System;
using Microsoft.Xna.Framework;

namespace RogueSimulator.Classes
{
    public class Animation
    {
        private const int ELAPSED_MS = 90;
        private int _prevElapsed;

        public Animation(int firstX, int y, int width, int height, int offset, int numberOfFrames)
        {
            _xStart = firstX;
            _y = y;
            _width = width;
            _height = height;
            _offset = offset;
            _numberOfFrames = numberOfFrames;
            _numberOfSelectedFrame = 1;
            _prevElapsed = 0;
        }

        private int _xStart { get; }
        private int _y { get; }

        private int _width { get; }
        private int _height { get; }
        private int _offset { get; }
        private int _numberOfFrames { get; }

        private int _numberOfSelectedFrame { get; set; }


        public void Update(GameTime gameTime)
        {
            int elapsed = Convert.ToInt32(gameTime.TotalGameTime.TotalMilliseconds / ELAPSED_MS);
            if (elapsed > _prevElapsed)
            {
                _prevElapsed = elapsed;
                _numberOfSelectedFrame = _numberOfSelectedFrame++ >= _numberOfFrames
                    ? 1
                    : _numberOfSelectedFrame++;
            }
        }

        public Rectangle getAnimationFrame()
        {
            return new Rectangle
            {
                X = _xStart + (_numberOfSelectedFrame - 1) * _offset,
                Y = _y,
                Width = _width,
                Height = _height,
            };
        }
    }
}