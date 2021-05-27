using UnityEngine;

public class PlayerKiller : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }

        Destroy(other.gameObject);
    }
}
