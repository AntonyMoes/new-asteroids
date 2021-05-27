using UnityEngine;

public class PlayerFollower : MonoBehaviour {
    [SerializeField] float thrust;
    Transform _player;
    Rigidbody2D _rb;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        var direction = (_player.position - transform.position).normalized;
        _rb.AddForce(direction * thrust, ForceMode2D.Impulse);
    }

    public void SetPlayer(Transform player) {
        _player = player;
    }
}
