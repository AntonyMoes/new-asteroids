namespace GameLogic {
    public abstract class BoundsLeaveLogic {
        const string BoundsTag = "GameBounds";

        public void CheckBoundsLeave(string leaveObjectTag, Bounds? bounds) {
            if (leaveObjectTag != BoundsTag || bounds == null) {
                return;
            }

            OnBoundsLeave(bounds.Value);
        }

        protected abstract void OnBoundsLeave(Bounds bounds);
    }
}
