using System;
using System.Numerics;

namespace GameLogic {
    public class Laser : Weapon {
        readonly int _maxCharge;

        readonly float _maxLaserScale;
        readonly float _timePerCharge;
        float _currentCharge;

        public Action<float> OnChangeCharge;

        public Laser(float cooldownTime, ISizedTransformProvider weaponWielder,
            Func<ISizedTransformProvider, ITransformProvider> projectileCreator, int maxCharge, float timePerCharge,
            float maxLaserScale) : base(cooldownTime, weaponWielder, projectileCreator) {
            _maxCharge = maxCharge;
            _currentCharge = maxCharge;
            _timePerCharge = timePerCharge;
            _maxLaserScale = maxLaserScale;
        }

        protected override void ShootLogic(ITransformProvider projectile) {
            _currentCharge--;
            projectile.Position += projectile.Forward * _maxLaserScale / 2;
            projectile.Scale = new Vector2(projectile.Scale.X, _maxLaserScale);
        }

        protected override void AdditionalUpdate(float deltaTime) {
            var newCharge = Math.Min(_maxCharge, _currentCharge + deltaTime / _timePerCharge);
            if (newCharge == _currentCharge) {
                return;
            }

            OnChangeCharge(newCharge);
            _currentCharge = newCharge;
        }

        protected override bool CanShoot() {
            return _currentCharge >= 1;
        }
    }
}
