using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] float cooldown;
    bool _isOnCooldown;
    
    public void Shoot() {
        if (_isOnCooldown) {
            return;
        }

        StartCoroutine(Cooldown());
        ShootLogic();
    }

    IEnumerator Cooldown() {
        _isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        _isOnCooldown = false;
    }

    protected abstract void ShootLogic();
}
