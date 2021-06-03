using System.Numerics;

namespace GameLogic {
    public class PlayerMovementLogic<TPlayer> where TPlayer : class, IRotationProvider, IRelativeVelocityProvider {
        readonly float _acceleration;
        readonly TPlayer _playerObject;
        readonly float _rotationSpeed;

        public PlayerMovementLogic(TPlayer playerObject, float acceleration, float rotationSpeed) {
            _playerObject = playerObject;
            _acceleration = acceleration;
            _rotationSpeed = rotationSpeed;
        }

        public void ApplyPlayerInput(Vector2 directionInput, float deltaTime) {
            var rv = _playerObject.RelativeVelocity;
            _playerObject.RelativeVelocity = new Vector2(rv.X, rv.Y + directionInput.Y * _acceleration * deltaTime);
            _playerObject.Rotation += directionInput.X * _rotationSpeed * deltaTime;
        }
    }
}
