namespace GameLogic {
    public class BoundsLeaveDestroyLogic : BoundsLeaveLogic {
        readonly IDestroyable _objectToDestroy;

        public BoundsLeaveDestroyLogic(IDestroyable objectToDestroy) {
            _objectToDestroy = objectToDestroy;
        }

        protected override void OnBoundsLeave(Bounds bounds) {
            _objectToDestroy.Destroy();
        }
    }
}
