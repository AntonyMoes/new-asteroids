using UnityEngine;

public class Laser : Weapon {
    [SerializeField] BarController laserCharge;
    [SerializeField] int charges;
    [SerializeField] float secondsPerCharge;
    float _laserSize;

    void Start() {
        laserCharge.SetSize(charges);
        laserCharge.Value = charges;
        var screenBounds = GameObject.FindWithTag("GameBounds").GetComponent<BoxCollider2D>().bounds;
        _laserSize = screenBounds.size.magnitude;
    }

    void Update() {
        laserCharge.Value += Time.deltaTime / secondsPerCharge;
    }

    protected override void ShootLogic(GameObject projectile) {
        laserCharge.Value -= 1;
        var scale = projectile.transform.localScale;
        projectile.transform.localScale = scale.UpdateComponents(y: scale.y * _laserSize);
        projectile.transform.Translate(projectile.transform.up * _laserSize / 2, Space.World);
    }

    protected override bool CanShoot() {
        return laserCharge.Value >= 1;
    }
}
