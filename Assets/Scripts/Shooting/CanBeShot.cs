using System;
using UnityEngine;

public class CanBeShot : MonoBehaviour {
    [SerializeField] float points;
    Action<float> _getShotCallback;

    public void SetGetShotCallback(Action<float> getShotCallback) {
        _getShotCallback = getShotCallback;
    }

    public void GetShot() {
        _getShotCallback?.Invoke(points);
        Destroy(gameObject);
    }
}
