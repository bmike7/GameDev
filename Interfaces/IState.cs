namespace RogueSimulator.Interfaces
{
    public interface IState : IUpdatable, IDrawable
    {
        void LoadContent();
    }
}
