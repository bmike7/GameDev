namespace RogueSimulator.Interfaces
{
    public interface IInput
    {
        bool IsRight { get; }
        bool IsLeft { get; }
        bool IsStartedJumping { get; }
    }
}
