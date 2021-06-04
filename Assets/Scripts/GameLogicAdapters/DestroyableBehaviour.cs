using GameLogic;
using UnityEngine;

public class DestroyableBehaviour : MonoBehaviour, IDestroyable {
    public void Destroy() {
        Destroy(gameObject);
    }
}
