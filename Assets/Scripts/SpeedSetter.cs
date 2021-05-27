using UnityEngine;

public class SpeedSetter : MonoBehaviour {
    public float minSpeed;
    public float maxSpeed;

    void Start() {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector2.up * Random.Range(minSpeed, maxSpeed));
    }
}
