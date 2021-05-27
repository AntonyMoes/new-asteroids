using UnityEngine;

public class CameraBounds : MonoBehaviour {
    Camera _camera;
    BoxCollider2D _boundsCollider;

    void Awake() {
        _camera = GetComponentInParent<Camera>();
        _boundsCollider = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        UpdateSize();
    }

    void UpdateSize() {
        var cameraHeight = _camera.orthographicSize * 2;
        var cameraSize = new Vector2(cameraHeight * _camera.aspect, cameraHeight);

        if (_boundsCollider.size != cameraSize) {
            _boundsCollider.size = cameraSize;
        }
    }
}
