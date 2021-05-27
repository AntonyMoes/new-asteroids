using UnityEngine;

public class BoundsLeaveDestroyer : BoundsLeaver {
    protected override void OnBoundsLeave(Bounds bounds) {
        Destroy(gameObject);
    }
}
