using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class Tile : LevelObject
    {
        public const int SIZE = 50;
        public Tile(Texture2D texture, Vector2 position, Rectangle spritesheetRectangle)
            : base(texture, position, spritesheetRectangle, SIZE) { }
    }
}
