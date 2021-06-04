using System;
using GameLogic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : DestroyableBehaviour, IShootable {
    public float minSpeed;
    public float maxSpeed;
    [SerializeField] float points;
    BoundsLeaveLogic _boundsLeaveLogic;
    ConstantMovementLogic _constantVelocityLogic;
    ShootableLogic<DestroyableRigidbodyAdapter> _shootableLogic;
    ShotLogic _shotLogic;

    void Awake() {
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformAdapter(transform));
        _shootableLogic = new ShootableLogic<DestroyableRigidbodyAdapter>(
            new DestroyableRigidbodyAdapter(this), points);
        _shotLogic = new ShotLogic("Player");
    }

    void Start() {
        _constantVelocityLogic = new ConstantMovementLogic(new RigidbodyAdapter(transform),
            Random.Range(minSpeed, maxSpeed));
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
}
