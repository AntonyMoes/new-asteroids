using GameLogic;

public class DestroyableRigidbodyAdapter : RigidbodyAdapter, IDestroyable {
    readonly IDestroyable _destroyable;

    public DestroyableRigidbodyAdapter(DestroyableBehaviour target) : base(target.transform) {
        _destroyable = target;
    }

    public void Destroy() {
        _destroyable.Destroy();
    }
}
