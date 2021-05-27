using System;
using UnityEngine;

public class Laser : Weapon {
    [SerializeField] GameObject laserPrefab;
    Drawable _drawable;
    float _laserSize;

    void Start() {
        _drawable = GetComponent<Drawable>();
        var screenBounds = GameObject.FindWithTag("CameraBounds").GetComponent<BoxCollider2D>().bounds;
        _laserSize = screenBounds.size.magnitude;
    }

    protected override void ShootLogic() {
        var laser = Instantiate(laserPrefab, transform.position, transform.rotation);
        laser.GetComponent<Drawable>().CopySettings(_drawable);
        var scale = laser.transform.localScale;
        laser.transform.localScale = scale.UpdateComponents(y: scale.y * _laserSize);
        laser.transform.Translate(laser.transform.up * _laserSize / 2, Space.World);
    }
}