using UnityEngine;

public class Shooter : MonoBehaviour {
    [SerializeField] bool _destroyOnHit;

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) {
            return;
        }

        other.gameObject.GetComponent<CanBeShot>().GetShot();

        if (_destroyOnHit) {
            Destroy(gameObject);
        }
    }
}
