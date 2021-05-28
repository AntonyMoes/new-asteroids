using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float cooldown;
    Func<Func<GameObject>, GameObject> _instantiator;
    bool _isOnCooldown;

    public void SetObjectInstantiator(Func<Func<GameObject>, GameObject> instantiator) {
        _instantiator = instantiator;
    }

    public void Shoot() {
        if (_isOnCooldown || !CanShoot()) {
            return;
        }

        StartCoroutine(Cooldown());
        var projectile = _instantiator(() => Instantiate(projectilePrefab, transform.position, transform.rotation));
        ShootLogic(projectile);
    }

    protected virtual bool CanShoot() {
        return true;
    }

    IEnumerator Cooldown() {
        _isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        _isOnCooldown = false;
    }

    protected abstract void ShootLogic(GameObject projectile);
}
