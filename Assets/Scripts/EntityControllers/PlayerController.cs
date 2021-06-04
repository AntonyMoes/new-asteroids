using System;
using System.Collections.Generic;
using GameLogic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : DestroyableBehaviour, IShootable {
    [SerializeField] float acceleration;
    [SerializeField] float rotationSpeed;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip machineGunSound;
    [SerializeField] float machineGunCooldown;

    [SerializeField] GameObject laserRayPrefab;
    [SerializeField] AudioClip laserSound;
    [SerializeField] float laserCooldown;
    [SerializeField] int laserCharges;
    [SerializeField] float timePerCharge;
    [SerializeField] BarController laserChargeUI;

    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;

    readonly List<(string, Weapon)> _weaponsWithActivators = new List<(string, Weapon)>();
    BoundsLeaveLogic _boundsLeaveLogic;

    Vector2 _directionInput;

    PlayerMovementLogic<RigidbodyAdapter> _playerMoveLogic;
    ShootableLogic<DestroyableRigidbodyAdapter> _shootableLogic;

    void Awake() {
        _playerMoveLogic = new PlayerMovementLogic<RigidbodyAdapter>(new RigidbodyAdapter(transform),
            acceleration, rotationSpeed);
        _boundsLeaveLogic = new BoundsLeaveTeleportLogic(new TransformAdapter(transform));
        _shootableLogic = new ShootableLogic<DestroyableRigidbodyAdapter>(new DestroyableRigidbodyAdapter(this), 0);
    }

    void Update() {
        _directionInput.x = Input.GetAxis("Horizontal");
        _directionInput.y = Input.GetAxis("Vertical");

        foreach (var (activator, weapon) in _weaponsWithActivators) {
            weapon.UpdateWeaponSystems(Time.deltaTime);
            if (Input.GetButtonDown(activator)) {
                weapon.Shoot();
            }
        }
    }

    void FixedUpdate() {
        _playerMoveLogic.ApplyPlayerInput(_directionInput.ToSystem(), Time.fixedDeltaTime);
    }

    void OnTriggerExit2D(Collider2D other) {
        _boundsLeaveLogic.CheckBoundsLeave(other.tag, other.bounds.ToLogic());
    }

    public Action<float, IPositionVelocityProvider> OnShot {
        get => _shootableLogic.OnShot;
        set => _shootableLogic.OnShot = value;
    }

    public void GetShot() {
        _shootableLogic.GetShot();
    }

    void SetupWeaponSound(Weapon weapon, AudioClip clip) {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        weapon.OnShoot += () => {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        };
    }

    Func<ITransformProvider, ITransformProvider> GetProjectileCreator(
        Func<Func<GameObject>, GameObject> instantiator, GameObject projectilePrefab) {
        return positionRotation => {
            var position = positionRotation.Position.ToUnity();
            var rotation = Quaternion.Euler(0, 0, -positionRotation.Rotation);
            var obj = instantiator(() => Instantiate(projectilePrefab, position, rotation));
            return new TransformAdapter(obj.transform);
        };
    }

    public void SetupWeapons(Func<Func<GameObject>, GameObject> instantiator, float maxMapDistance) {
        var weaponWielder = new ColliderAdapter(transform);

        var machineGun = new MachineGun(machineGunCooldown, weaponWielder,
            GetProjectileCreator(instantiator, bulletPrefab));
        var laser = new Laser(laserCooldown, weaponWielder,
            GetProjectileCreator(instantiator, laserRayPrefab),
            laserCharges, timePerCharge, maxMapDistance);

        laserChargeUI.SetSize(laserCharges);
        laser.OnChangeCharge += charge => laserChargeUI.Value = charge;

        var tuples = new (string, Weapon, AudioClip)[] {
            ("Fire1", machineGun, machineGunSound),
            ("Fire2", laser, laserSound)
        };
        foreach (var (activator, weapon, sound) in tuples) {
            SetupWeaponSound(weapon, sound);
            _weaponsWithActivators.Add((activator, weapon));
        }
    }
}
