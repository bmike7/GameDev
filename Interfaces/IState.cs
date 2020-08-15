using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueSimulator.Interfaces
{
    public interface IState : IUpdatable, Interfaces.IDrawable
    {
        void LoadContent();
    }
}
