using UnityEngine;

public class BoundsLeaveTeleporter : BoundsLeaver {
    protected override void OnBoundsLeave(Bounds bounds) {
        var horizontalExtended = new Bounds(bounds.center,
            new Vector3(bounds.size.x * 1.5f, bounds.size.y, bounds.size.z));
        var verticalExtended = new Bounds(bounds.center,
            new Vector3(bounds.size.x, bounds.size.y * 1.5f, bounds.size.z));

        var vectorToBoundsCenter = bounds.center - transform.position;
        var delta = new Vector3();
        if (!horizontalExtended.Contains(transform.position)) {  // left vertically
            delta.y = vectorToBoundsCenter.y * 2;
        }
        if (!verticalExtended.Contains(transform.position)) {  // left horizontally
            delta.x = vectorToBoundsCenter.x * 2;
        }

        transform.position += delta;
    }
}
