using UnityEngine;

public static class Utils {
    public static Vector3 GetRandomPointInBounds(this in Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
    }

    public static Quaternion GetRandomRotation() {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public static Vector3 UpdateComponents(this in Vector3 vector, float? x = null, float? y = null, float? z = null) {
        var res = vector;
        if (x is float xVal) {
            res.x = xVal;
        }

        if (y is float yVal) {
            res.y = yVal;
        }

        if (z is float zVal) {
            res.z = zVal;
        }

        return res;
    }

    public static T RandomElement<T>(this T[] array) {
        return array[Random.Range(0, array.Length)];
    }
}
