namespace GameLogic {
    public class TargetFollowLogic<TFollower> where TFollower : class, IPositionProvider, IVelocityProvider {
        readonly TFollower _followerObject;
        readonly float _speed;
        readonly IPositionProvider _target;

        public TargetFollowLogic(IPositionProvider target, TFollower followerObject, float speed) {
            _target = target;
            _followerObject = followerObject;
            _speed = speed;
        }

        public void UpdateFollowDirection() {
            _followerObject.Velocity = (_target.Position - _followerObject.Position).Normalize() * _speed;
        }
    }
}
