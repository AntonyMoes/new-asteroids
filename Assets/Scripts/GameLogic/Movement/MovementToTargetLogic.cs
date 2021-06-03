namespace GameLogic {
    public class MovementToTargetLogic<TFollower> where TFollower : class, IPositionProvider, IVelocityProvider {
        readonly float _acceleration;
        readonly TFollower _followerObject;
        readonly IPositionProvider _target;

        public MovementToTargetLogic(IPositionProvider target, TFollower followerObject, float acceleration) {
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
