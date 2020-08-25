namespace RogueSimulator.Interfaces
{
    public interface IState : IUpdatable, Interfaces.IDrawable
    {
        void LoadContent();
    }
}
