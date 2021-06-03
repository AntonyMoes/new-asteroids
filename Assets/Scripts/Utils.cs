using UnityEngine;
using Vector2 = System.Numerics.Vector2;

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

    public static Vector2 Normalize(this Vector2 vector) {
        return vector / vector.Length();
    }

    public static Vector2 ToSystem(this UnityEngine.Vector2 vector) {
        return new Vector2(vector.x, vector.y);
    }

    public static UnityEngine.Vector2 ToUnity(this Vector2 vector) {
        return new UnityEngine.Vector2(vector.X, vector.Y);
    }

    public static GameLogic.Bounds ToLogic(this Bounds vector) {
        return new GameLogic.Bounds {
            Center = ((UnityEngine.Vector2) vector.center).ToSystem(),
            Size = ((UnityEngine.Vector2) vector.size).ToSystem()
        };
    }
}
