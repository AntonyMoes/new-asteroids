namespace GameLogic {
    public class ShotLogic {
        const string ShootableTag = "Enemy";
        readonly bool _destroyOnHit;
        readonly IDestroyable _objectToDestroyOnShot;

        public ShotLogic(IDestroyable objectToDestroyOnShot, bool destroyOnHit) {
            _objectToDestroyOnShot = objectToDestroyOnShot;
            _destroyOnHit = destroyOnHit;
        }

        public void CheckShot(string hitObjectTag, IShootable shootable) {
            if (hitObjectTag != ShootableTag || shootable == null) {
                return;
            }

            if (_destroyOnHit) {
                _objectToDestroyOnShot.Destroy();
            }

            shootable.GetShot();
        }
    }
}
