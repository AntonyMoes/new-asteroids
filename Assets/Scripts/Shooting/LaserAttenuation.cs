using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttenuation : MonoBehaviour {
    [SerializeField] float attenuationTime;

    float _attenuationRate;

    void Start() {
        _attenuationRate = transform.localScale.x / attenuationTime;
    }

    void FixedUpdate() {
        var scale = transform.localScale;
        var newXScale = Mathf.Max(scale.x - _attenuationRate * Time.fixedDeltaTime, 0);
        if (newXScale != 0) {
            transform.localScale = scale.UpdateComponents(newXScale);
            return;
        }

        Destroy(gameObject);
    }
}
