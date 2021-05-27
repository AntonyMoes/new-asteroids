using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float thrust;
    [SerializeField] float rotationSpeed;

    Action _destroyCallback = null;
    Rigidbody2D _rb;
    Vector2 _directionInput = new Vector2();

    Weapon _weapon1;
    Weapon _weapon2;

    public void SetDestroyCallback(Action destroyCallback) {
        _destroyCallback = destroyCallback;
    }

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _weapon1 = GetComponent<MachineGun>();
        _weapon2 = GetComponent<Laser>();
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
}
