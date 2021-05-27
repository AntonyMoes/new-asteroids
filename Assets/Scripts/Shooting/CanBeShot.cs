using System;
using UnityEngine;

public class CanBeShot : MonoBehaviour {
    [SerializeField] float points;
    Action<GameObject> _generalCallback;
    Action<float> _pointsCallback;

    public void SetPointsCallback(Action<float> pointsCallback) {
        _pointsCallback = pointsCallback;
    }

    public void SetGeneralCallback(Action<GameObject> generalCallback) {
        _generalCallback = generalCallback;
    }

    public void GetShot() {
        _pointsCallback?.Invoke(points);
        _generalCallback?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
