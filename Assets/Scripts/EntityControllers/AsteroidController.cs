using GameLogic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
    public float minSpeed;
    public float maxSpeed;
    BoundsLeaveLogic _boundsLeaveLogic;
    ConstantMovementLogic _constantVelocityLogic;

    void Awake() {
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformProvider(this));
    }

    void Start() {
        _constantVelocityLogic = new ConstantMovementLogic(new RigidbodyProvider(this),
            Random.Range(minSpeed, maxSpeed));
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }
}
