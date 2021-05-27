using System;
using UnityEngine;

class MachineGun : Weapon {
    [SerializeField] GameObject bulletPrefab;
    Drawable _drawable;

    void Start() {
        _drawable = GetComponent<Drawable>();
    }

    protected override void ShootLogic() {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Drawable>().CopySettings(_drawable);
    }
}
