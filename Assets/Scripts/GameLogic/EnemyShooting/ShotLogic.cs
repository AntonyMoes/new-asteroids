namespace GameLogic {
    public class ShotLogic {
        readonly IDestroyable _objectToDestroyOnShot;
        readonly string _shootableTag;

        public ShotLogic(string shootableTag, IDestroyable objectToDestroyOnShot = null) {
            _shootableTag = shootableTag;
            _objectToDestroyOnShot = objectToDestroyOnShot;
        }

        public void CheckShot(string hitObjectTag, IShootable shootable) {
            if (hitObjectTag != _shootableTag || shootable == null) {
                return;
            }

            _objectToDestroyOnShot?.Destroy();

            shootable.GetShot();
        }
    }
}
