using UnityEngine;

public class CameraBounds : MonoBehaviour {
    [SerializeField] Camera cam;
    BoxCollider2D _boundsCollider;

    void Awake() {
        _boundsCollider = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        UpdateSize();
    }

    void UpdateSize() {
        var cameraHeight = cam.orthographicSize * 2;
        var cameraSize = new Vector2(cameraHeight * cam.aspect, cameraHeight);

        if (_boundsCollider.size != cameraSize) {
            _boundsCollider.size = cameraSize;
        }
    }
}
