using UnityEngine;

public class Shooter : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) {
            return;
        }
        
        other.gameObject.GetComponent<CanBeShot>().GetShot();
    }
}
