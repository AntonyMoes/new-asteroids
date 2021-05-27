using System;
using UnityEngine;

public class PlayerKiller : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) {
            return;
        }
        
        Destroy(other.gameObject);
    }
}
