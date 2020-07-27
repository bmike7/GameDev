using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Classes.Mechanics.Menu
{
    public abstract class Menu : Interfaces.IDrawable
    {
        protected Viewport _viewport;
        private Texture2D _background;
        protected List<Button> _buttonList;
        public Menu(Viewport viewport, Texture2D background)
        {
            _viewport = viewport;
            _background = background;
            _buttonList = new List<Button>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _background,
                position: new Vector2(0, 0),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0,
                origin: new Vector2(1, 1),
                scale: (float)_viewport.Width / _background.Width + 0.1f,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
            foreach (Button button in _buttonList)
                button.Draw(spriteBatch);
        }

        public List<Button> GetButtons()
        {
            return _buttonList;
        }
    }
}
