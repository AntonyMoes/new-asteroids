using GameLogic;
using UnityEngine;

public class BulletController : DestroyableBehaviour {
    [SerializeField] float speed;
    BoundsLeaveLogic _boundsLeaveLogic;
    ConstantMovementLogic _constantVelocityLogic;
    ShotLogic _shotLogic;

    void Awake() {
        _constantVelocityLogic = new ConstantMovementLogic(new RigidbodyAdapter(transform), speed);
        _boundsLeaveLogic = new BoundsLeaveDestroyLogic(this);
        _shotLogic = new ShotLogic(this, true);
    }

    void OnTriggerEnter2D(Collider2D other) {
        _shotLogic.CheckShot(other.tag, other.gameObject.GetComponent<IShootable>());
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }
}
