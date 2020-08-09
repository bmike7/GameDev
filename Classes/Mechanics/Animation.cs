using System;
using Microsoft.Xna.Framework;

namespace RogueSimulator.Classes.Mechanics
{
    public class Animation
    {
        private const int ELAPSED_MS = 90;
        private int _prevElapsed;
        private bool _isOneFullCicle;
        private bool _didAllFrames;

        public Animation(int firstX, int y, int width, int height, int offset, int numberOfFrames, bool isOneFullCicle = false)
        {
            _xStart = firstX;
            _y = y;
            _width = width;
            _height = height;
            _offset = offset;
            _numberOfFrames = numberOfFrames;
            _isOneFullCicle = isOneFullCicle;
            _didAllFrames = false;
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
                bool didAllFrames = _numberOfSelectedFrame++ >= _numberOfFrames;

                _didAllFrames = _isOneFullCicle && didAllFrames;

                _numberOfSelectedFrame = didAllFrames
                    ? 1
                    : _numberOfSelectedFrame++;
            }
        }

        public Rectangle getAnimationFrameRectangle()
        {
            return new Rectangle
            {
                X = _xStart + (_numberOfSelectedFrame - 1) * _offset,
                Y = _y,
                Width = _width,
                Height = _height,
            };
        }

        public bool CanChangeAnimation()
        {
            if (!_isOneFullCicle)
                return true;

            bool tempDidAllFrames = _didAllFrames;
            if (_didAllFrames)
                _didAllFrames = false;

            return tempDidAllFrames;
        }
    }
}
