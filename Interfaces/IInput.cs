namespace RogueSimulator.Interfaces
{
    public interface IInput
    {
        bool IsRight { get; set; }
        bool IsLeft { get; set; }
        bool IsStartedJumping { get; set; }
    }
}
