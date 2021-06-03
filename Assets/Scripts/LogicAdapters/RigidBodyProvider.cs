using GameLogic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class RigidbodyProvider : TransformProvider, IVelocityProvider, IRelativeVelocityProvider {
    readonly Rigidbody2D _rb;

    public RigidbodyProvider(MonoBehaviour target) : base(target) {
        _rb = target.GetComponent<Rigidbody2D>();
    }

    public Vector2 RelativeVelocity {
        get {
            UnityEngine.Vector2 rotated = Quaternion.Inverse(Target.transform.rotation) * _rb.velocity;
            return rotated.ToSystem();
        }
        set => _rb.velocity = Target.transform.rotation * value.ToUnity();
    }

    public Vector2 Velocity {
        get => _rb.velocity.ToSystem();
        set => _rb.velocity = value.ToUnity();
    }
}
