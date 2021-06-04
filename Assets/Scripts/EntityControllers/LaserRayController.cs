using GameLogic;
using UnityEngine;

public class LaserRayController : DestroyableBehaviour {
    [SerializeField] float attenuationTime;
    LaserRayAttenuationLogic<DestroyableRigidbodyAdapter> _attenuationLogic;

    ShotLogic _shotLogic;

    void Awake() {
        _shotLogic = new ShotLogic("Enemy");
        _attenuationLogic =
            new LaserRayAttenuationLogic<DestroyableRigidbodyAdapter>(new DestroyableRigidbodyAdapter(this),
                attenuationTime);
    }

    void Update() {
        _attenuationLogic.UpdateAttenuation(Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        _shotLogic.CheckShot(other.tag, other.gameObject.GetComponent<IShootable>());
    }
}
