using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class FinisherPortal : LevelObject
    {
        public static int HEIGHT = 80;
        public static string ASSET_NAME = "SpriteSheets/Tileset/Portal";
        public FinisherPortal(Texture2D texture, Vector2 position)
            : base(
                texture: texture,
                position: position,
                spritesheetRectangle: new Rectangle(17, 7, 28, 50),
                height: HEIGHT
            )
        { }
    }
}
