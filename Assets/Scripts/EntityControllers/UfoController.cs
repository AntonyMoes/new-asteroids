using System;
using GameLogic;
using UnityEngine;

public class UfoController : DestroyableBehaviour, IDestroyable, IShootable {
    [SerializeField] float acceleration;
    [SerializeField] float points;
    BoundsLeaveLogic _boundsLeaveLogic;
    ShootableLogic<DestroyableRigidbodyAdapter> _shootableLogic;
    MovementToTargetLogic<RigidbodyAdapter> _targetFollowLogic;

    void Awake() {
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformAdapter(this));
        _shootableLogic = new ShootableLogic<DestroyableRigidbodyAdapter>(
            new DestroyableRigidbodyAdapter(this), points);
    }

    void FixedUpdate() {
        _targetFollowLogic.UpdateFollowDirection(Time.fixedDeltaTime);
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

    public void SetPlayer(PlayerController player) {
        _targetFollowLogic = new MovementToTargetLogic<RigidbodyAdapter>(new TransformAdapter(player),
            new RigidbodyAdapter(this), acceleration);
    }
}
