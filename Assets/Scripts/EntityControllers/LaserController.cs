using GameLogic;
using UnityEngine;

public class LaserController : DestroyableBehaviour {
    ShotLogic _shotLogic;

    void Awake() {
        _shotLogic = new ShotLogic(this, false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        _shotLogic.CheckShot(other.tag, other.gameObject.GetComponent<IShootable>());
    }
}
