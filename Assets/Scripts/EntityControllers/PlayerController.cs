using System;
using GameLogic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float acceleration;
    [SerializeField] float rotationSpeed;
    BoundsLeaveLogic _boundsLeaveLogic;

    Action _destroyCallback;
    Vector2 _directionInput;

    PlayerMovementLogic<RigidbodyProvider> _playerMoveLogic;

    Weapon _weapon1;
    Weapon _weapon2;

    void Awake() {
        _weapon1 = GetComponent<MachineGun>();
        _weapon2 = GetComponent<Laser>();
        _playerMoveLogic = new PlayerMovementLogic<RigidbodyProvider>(new RigidbodyProvider(this),
            acceleration, rotationSpeed);
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformProvider(this));
    }

    void Update() {
        _directionInput.x = Input.GetAxis("Horizontal");
        _directionInput.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1")) {
            _weapon1.Shoot();
        }

        if (Input.GetButtonDown("Fire2")) {
            _weapon2.Shoot();
        }
    }

    void FixedUpdate() {
        _playerMoveLogic.ApplyPlayerInput(_directionInput.ToSystem(), Time.fixedDeltaTime);
    }

    void OnDestroy() {
        _destroyCallback?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }

    public void SetDestroyCallback(Action destroyCallback) {
        _destroyCallback = destroyCallback;
    }

    public void SetObjectInstantiator(Func<Func<GameObject>, GameObject> instantiator) {
        _weapon1.SetObjectInstantiator(instantiator);
        _weapon2.SetObjectInstantiator(instantiator);
    }
}
