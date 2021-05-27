using UnityEngine;

public class SpeedSetter : MonoBehaviour {
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    void Start() {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector2.up * Random.Range(minSpeed, maxSpeed));
    }
}
