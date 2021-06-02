using System.Numerics;

namespace GameLogic {
    public interface IRelativeVelocityProvider {
        Vector2 RelativeVelocity { get; set; }
    }
}
