namespace GameLogic {
    public class MovementToTargetLogic {
        readonly float _acceleration;
        readonly IPositionVelocityProvider _followerObject;
        readonly IPositionProvider _target;

        public MovementToTargetLogic(IPositionProvider target, IPositionVelocityProvider followerObject,
            float acceleration) {
            _target = target;
            _followerObject = followerObject;
            _acceleration = acceleration;
        }

        public void UpdateFollowDirection(float deltaTime) {
            _followerObject.Velocity +=
                (_target.Position - _followerObject.Position).Normalize() * _acceleration * deltaTime;
        }
    }
}
