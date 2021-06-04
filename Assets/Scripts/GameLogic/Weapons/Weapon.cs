using System;

namespace GameLogic {
    public abstract class Weapon {
        readonly float _cooldownTime;
        readonly Func<ISizedTransformProvider, ITransformProvider> _projectileCreator;

        readonly ISizedTransformProvider _weaponWielder;
        float _remainingCooldown;

        public Action OnShoot;

        protected Weapon(float cooldownTime, ISizedTransformProvider weaponWielder,
            Func<ISizedTransformProvider, ITransformProvider> projectileCreator) {
            _cooldownTime = cooldownTime;
            _weaponWielder = weaponWielder;
            _projectileCreator = projectileCreator;
        }

        public void Shoot() {
            if (_remainingCooldown > 0 || !CanShoot()) {
                return;
            }

            _remainingCooldown = _cooldownTime;

            var projectile = _projectileCreator(_weaponWielder);
            projectile.Position += _weaponWielder.Forward * _weaponWielder.Size.Y / 2;

            ShootLogic(projectile);
            OnShoot();
        }

        protected virtual void ShootLogic(ITransformProvider projectile) { }

        public void UpdateWeaponSystems(float deltaTime) {
            _remainingCooldown = Math.Max(0, _remainingCooldown - deltaTime);
            AdditionalUpdate(deltaTime);
        }

        protected virtual void AdditionalUpdate(float deltaTime) { }

        protected virtual bool CanShoot() {
            return true;
        }
    }
}
