namespace GameLogic {
    public abstract class BoundsLeaveLogic {
        public void CheckBoundsLeave(string leaveObjectTag, Bounds? bounds) {
            if (leaveObjectTag != "GameBounds" || bounds == null) {
                return;
            }

            OnBoundsLeave(bounds.Value);
        }

        protected abstract void OnBoundsLeave(Bounds bounds);
    }
}
