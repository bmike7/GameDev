using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Interfaces
{
    public interface IShooter
    {
        Gun Gun { get; set; }
        void Shoot();
    }
}
