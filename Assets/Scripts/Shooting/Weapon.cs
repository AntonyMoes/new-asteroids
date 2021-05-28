using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float cooldown;
    [SerializeField] AudioClip shotSound;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    AudioSource _audioSource;

    Collider2D _collider;
    Func<Func<GameObject>, GameObject> _instantiator;
    bool _isOnCooldown;

    void Awake() {
        _collider = GetComponent<Collider2D>();
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void SetObjectInstantiator(Func<Func<GameObject>, GameObject> instantiator) {
        _instantiator = instantiator;
    }

    public void Shoot() {
        if (_isOnCooldown || !CanShoot()) {
            return;
        }

        StartCoroutine(Cooldown());
        var projectile = _instantiator(() => Instantiate(projectilePrefab, transform.position, transform.rotation));
        projectile.transform.Translate(projectile.transform.up * _collider.bounds.extents.y, Space.World);
        _audioSource.clip = shotSound;
        _audioSource.pitch = Random.Range(minPitch, maxPitch);
        _audioSource.Play();
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
