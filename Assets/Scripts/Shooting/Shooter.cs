using UnityEngine;

public class Shooter : MonoBehaviour {
    [SerializeField] bool destroyOnHit;

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) {
            return;
        }

        other.gameObject.GetComponent<CanBeShot>().GetShot();

        if (destroyOnHit) {
            Destroy(gameObject);
        }
    }
}
