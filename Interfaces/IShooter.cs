using Microsoft.Xna.Framework;

using RogueSimulator.Classes.Level;
using RogueSimulator.Classes.Mechanics;

namespace RogueSimulator.Interfaces
{
    public interface IShooter
    {
        Gun Gun { get; set; }
    }
}
