using UnityEngine;

public class Laser : Weapon {
    float _laserSize;

    void Start() {
        var screenBounds = GameObject.FindWithTag("CameraBounds").GetComponent<BoxCollider2D>().bounds;
        _laserSize = screenBounds.size.magnitude;
    }

    protected override void ShootLogic(GameObject projectile) {
        var scale = projectile.transform.localScale;
        projectile.transform.localScale = scale.UpdateComponents(y: scale.y * _laserSize);
        projectile.transform.Translate(projectile.transform.up * _laserSize / 2, Space.World);
    }
}
