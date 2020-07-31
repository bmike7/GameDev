using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class LevelMenu : Menu
    {
        public LevelMenu(Game1 game, Texture2D background, Texture2D buttonsTexture) : base(game.GraphicsDevice.Viewport, background)
        {
            Vector2 level1Pos = new Vector2(20, 20);
            Vector2 level2Pos = new Vector2(70, 20);

            _buttonList.Add(new Button(
                onClickAction: () => game.ChangeGameState(GameState.PLAYING),
                buttonTexture: buttonsTexture,
                position: level1Pos,
                buttonSpriteRectangle: new Rectangle(5, 42, 12, 12))
            );
            _buttonList.Add(new Button(
                onClickAction: () => game.ChangeGameState(GameState.PLAYING),
                buttonTexture: buttonsTexture,
                position: level2Pos,
                buttonSpriteRectangle: new Rectangle(19, 42, 12, 12))
            );
        }
    }
}
