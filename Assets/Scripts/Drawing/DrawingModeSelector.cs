using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawingModeSelector : MonoBehaviour {
    readonly HashSet<Action<bool>> _drawingModeSetters = new HashSet<Action<bool>>();
    bool _isSpriteMode;

    public void AddSetter(Action<bool> setter) {
        _drawingModeSetters.Add(setter);
        setter(_isSpriteMode);
    }
    
    public void RemoveSetter(Action<bool> setter) {
        _drawingModeSetters.Remove(setter);
    }

    void SwitchMode() {
        _isSpriteMode = !_isSpriteMode;

        foreach (var setter in _drawingModeSetters) {
           setter(_isSpriteMode);
        }
    }

    void Update() {
        if (Input.GetButtonDown("SwitchDrawingMode")) {
            SwitchMode();
        }
    }
}
