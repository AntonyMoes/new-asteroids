using UnityEngine;

public class Drawable : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] MeshRenderer meshRenderer;

    DrawingModeSelector _selector;

    void Awake() {
        var collider = GetComponent<Collider2D>();
        var meshFilter = meshRenderer.gameObject.GetComponent<MeshFilter>();

        var mesh = collider.CreateMesh(false, false);
        meshFilter.mesh = mesh;

        var ls = transform.localScale;
        var meshLs = meshFilter.transform.localScale;
        meshFilter.transform.localScale = meshLs.UpdateComponents(1 / ls.x, 1 / ls.y);
    }

    void OnDestroy() {
        _selector.RemoveSetter(SetDrawingMode);
    }

    public void CopySettings(Drawable other) {
        SetDrawingModeSelector(other._selector);
    }

    public void SetDrawingModeSelector(DrawingModeSelector selector) {
        _selector = selector;
        _selector.AddSetter(SetDrawingMode);
    }

    void SetDrawingMode(bool isSpriteMode) {
        spriteRenderer.enabled = isSpriteMode;
        meshRenderer.enabled = !isSpriteMode;
    }
}
