using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Level
{
    public class LevelFactory
    {
        public static BaseLevel CreateLevel(Game1 game)
        {
            switch (game.SelectedLevel)
            {
                case 1:
                    return new Level1(
                        texture: game.Content.Load<Texture2D>("SpriteSheets/Tileset/jungleTileSet"),
                        background: game.Content.Load<Texture2D>("SpriteSheets/Background/background"),
                        viewport: game.GraphicsDevice.Viewport
                    );
                case 2:
                    return new Level2(
                        texture: game.Content.Load<Texture2D>("SpriteSheets/Tileset/jungleTileSet"),
                        background: game.Content.Load<Texture2D>("SpriteSheets/Background/background"),
                        viewport: game.GraphicsDevice.Viewport
                    );
                default:
                    return new Level1(
                        texture: game.Content.Load<Texture2D>("SpriteSheets/Tileset/jungleTileSet"),
                        background: game.Content.Load<Texture2D>("SpriteSheets/Background/background"),
                        viewport: game.GraphicsDevice.Viewport
                    );
            }
        }
    }
}
