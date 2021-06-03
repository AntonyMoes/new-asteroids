using GameLogic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class TransformProvider : IPositionProvider, IRotationProvider, IForwardProvider {
    protected readonly MonoBehaviour Target;

    public TransformProvider(MonoBehaviour target) {
        Target = target;
    }

    public Vector2 Forward => ((UnityEngine.Vector2) Target.transform.up).ToSystem();

    public Vector2 Position {
        get => ((UnityEngine.Vector2) Target.transform.position).ToSystem();
        set => Target.transform.position = new Vector3(value.X, value.Y, Target.transform.position.z);
    }

    public float Rotation {
        get => -Target.transform.rotation.eulerAngles.z;
        set {
            var euler = Target.transform.rotation.eulerAngles;
            Target.transform.rotation = Quaternion.Euler(euler.x, euler.y, -value);
        }
    }
}
