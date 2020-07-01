using Microsoft.Xna.Framework;

namespace RogueSimulator.Classes
{
    public struct Action
    {
        public Action(int firstX, int y, int width, int height, int offset, int numberOfFrames)
        {
            _xStart = firstX;
            _y = y;
            _width = width;
            _height = height;
            _offset = offset;
            _numberOfFrames = numberOfFrames;
            _numberOfSelectedFrame = 0;
        }
        private int _xStart { get; }
        private int _y { get; }

        private int _width { get; }
        private int _height { get; }
        private int _offset { get; }
        private int _numberOfFrames { get; }

        private int _numberOfSelectedFrame;
        private int numberOfSelectedFrame
        {
            get
            {
                _numberOfSelectedFrame = _numberOfSelectedFrame++ >= _numberOfFrames
                    ? 1
                    : _numberOfSelectedFrame++;
                return _numberOfSelectedFrame - 1;
            }
        }

        public Rectangle getActionFrame()
        {
            return new Rectangle
            {
                X = _xStart + numberOfSelectedFrame * _offset,
                Y = _y,
                Width = _width,
                Height = _height,
            };
        }
    }
}