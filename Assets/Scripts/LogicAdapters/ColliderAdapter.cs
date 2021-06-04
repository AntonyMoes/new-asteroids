using GameLogic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace LogicAdapters {
    public class ColliderAdapter : TransformAdapter, ISizedTransformProvider {
        readonly Collider2D _collider;

        public ColliderAdapter(Transform target) : base(target) {
            _collider = target.GetComponent<Collider2D>();
        }

        public Vector2 Size => ((UnityEngine.Vector2) _collider.bounds.size).ToSystem();
    }
}
