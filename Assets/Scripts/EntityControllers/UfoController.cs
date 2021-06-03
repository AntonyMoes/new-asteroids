using GameLogic;
using UnityEngine;

public class UfoController : MonoBehaviour {
    [SerializeField] float acceleration;
    BoundsLeaveLogic _boundsLeaveLogic;
    MovementToTargetLogic<RigidbodyProvider> _targetFollowLogic;

    void Awake() {
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformProvider(this));
    }

    void FixedUpdate() {
        _targetFollowLogic.UpdateFollowDirection(Time.fixedDeltaTime);
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }

    public void SetPlayer(PlayerController player) {
        _targetFollowLogic = new MovementToTargetLogic<RigidbodyProvider>(new TransformProvider(player),
            new RigidbodyProvider(this), acceleration);
    }
}
