using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public class LevelMenu : Menu
    {
        public LevelMenu(Viewport viewport, Texture2D background, Texture2D buttonsTexture) : base(viewport, background)
        {
            Vector2 level1Pos = new Vector2(20, 20);
            Vector2 level2Pos = new Vector2(70, 20);

            _buttonList.Add(new Button(ButtonAction.START, buttonsTexture, level1Pos, new Rectangle(5, 42, 12, 12)));
            _buttonList.Add(new Button(ButtonAction.START, buttonsTexture, level2Pos, new Rectangle(19, 42, 12, 12)));
        }
    }
}
