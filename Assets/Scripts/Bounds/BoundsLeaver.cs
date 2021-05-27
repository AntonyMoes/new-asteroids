using System;
using UnityEngine;

public abstract class BoundsLeaver : MonoBehaviour {
    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("CameraBounds")) {
            return;
        }

        var colliderBounds = ((BoxCollider2D)other).bounds;
        OnBoundsLeave(colliderBounds);
    }

    protected abstract void OnBoundsLeave(Bounds bounds);
}
