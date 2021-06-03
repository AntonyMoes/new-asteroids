using GameLogic;
using UnityEngine;

public class BulletController : MonoBehaviour, IDestroyable {
    [SerializeField] float speed;
    BoundsLeaveLogic _boundsLeaveLogic;
    ConstantMovementLogic _constantVelocityLogic;

    void Awake() {
        _constantVelocityLogic = new ConstantMovementLogic(new RigidbodyProvider(this), speed);
        _boundsLeaveLogic = new BoundsLeaveDestroyLogic(this);
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
