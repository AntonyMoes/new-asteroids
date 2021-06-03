using System.Numerics;

namespace GameLogic {
    public class ConstantMovementLogic {
        public ConstantMovementLogic(IRelativeVelocityProvider obj, float speed) {
            obj.RelativeVelocity = Vector2.UnitY * speed;
        }
    }
}
