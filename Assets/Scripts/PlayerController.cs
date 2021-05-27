using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float thrust;
    [SerializeField] float rotationSpeed;

    Action _destroyCallback;
    Vector2 _directionInput;

    Func<Func<GameObject>, GameObject> _instantiator;
    Rigidbody2D _rb;

    Weapon _weapon1;
    Weapon _weapon2;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _weapon1 = GetComponent<MachineGun>();
        _weapon2 = GetComponent<Laser>();

        _weapon1.SetObjectInstantiator(_instantiator);
        _weapon2.SetObjectInstantiator(_instantiator);
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
        _rb.AddRelativeForce(Vector2.up * (_directionInput.y * thrust), ForceMode2D.Impulse);
        transform.Rotate(Vector3.forward, -_directionInput.x * rotationSpeed * Time.fixedDeltaTime);
    }

    void OnDestroy() {
        _destroyCallback?.Invoke();
    }

    public void SetDestroyCallback(Action destroyCallback) {
        _destroyCallback = destroyCallback;
    }

    public void SetObjectInstantiator(Func<Func<GameObject>, GameObject> instantiator) {
        _instantiator = instantiator;
    }
}
