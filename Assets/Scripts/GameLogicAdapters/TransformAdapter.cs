using GameLogic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class TransformAdapter : ITransformProvider {
    protected readonly Transform Target;

    public TransformAdapter(Transform target) {
        Target = target;
    }

    public Vector2 Forward => ((UnityEngine.Vector2) Target.up).ToSystem();

    public Vector2 Position {
        get => ((UnityEngine.Vector2) Target.position).ToSystem();
        set => Target.position = new Vector3(value.X, value.Y, Target.position.z);
    }

    public float Rotation {
        get => -Target.rotation.eulerAngles.z;
        set {
            var euler = Target.rotation.eulerAngles;
            Target.rotation = Quaternion.Euler(euler.x, euler.y, -value);
        }
    }

    public Vector2 Scale {
        get => ((UnityEngine.Vector2) Target.localScale).ToSystem();
        set => Target.localScale = new Vector3(value.X, value.Y, Target.localScale.z);
    }
}
