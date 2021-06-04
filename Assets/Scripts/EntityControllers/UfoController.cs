using System;
using GameLogic;
using UnityEngine;

public class UfoController : DestroyableBehaviour, IShootable {
    [SerializeField] float acceleration;
    [SerializeField] float points;
    BoundsLeaveLogic _boundsLeaveLogic;
    ShootableLogic<DestroyableRigidbodyAdapter> _shootableLogic;
    ShotLogic _shotLogic;
    MovementToTargetLogic _targetFollowLogic;

    void Awake() {
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformAdapter(transform));
        _shootableLogic = new ShootableLogic<DestroyableRigidbodyAdapter>(
            new DestroyableRigidbodyAdapter(this), points);
        _shotLogic = new ShotLogic("Player");
    }

    void FixedUpdate() {
        _targetFollowLogic.UpdateFollowDirection(Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        _shotLogic.CheckShot(other.tag, other.gameObject.GetComponent<IShootable>());
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }

    public void GetShot() {
        _shootableLogic.GetShot();
    }

    public Action<float, IPositionVelocityProvider> OnShot {
        get => _shootableLogic.OnShot;
        set => _shootableLogic.OnShot = value;
    }

    public void SetPlayer(Transform player) {
        _targetFollowLogic = new MovementToTargetLogic(new TransformAdapter(player),
            new RigidbodyAdapter(transform), acceleration);
    }
}
