using System;

namespace GameLogic {
    public class BoundsLeaveTeleportLogic : BoundsLeaveLogic {
        readonly IPositionProvider _objectToTeleport;

        public BoundsLeaveTeleportLogic(IPositionProvider objectToTeleport) {
            _objectToTeleport = objectToTeleport;
        }

        protected override void OnBoundsLeave(Bounds bounds) {
            var extents = bounds.Size / 2;
            var vectorToCenter = bounds.Center - _objectToTeleport.Position;

            var newPosition = _objectToTeleport.Position;
            if (Math.Abs(vectorToCenter.X) >= extents.X) {
                newPosition.X += vectorToCenter.X * 2;
            }

            if (Math.Abs(vectorToCenter.Y) >= extents.Y) {
                newPosition.Y += vectorToCenter.Y * 2;
            }

            _objectToTeleport.Position = newPosition;
        }
    }
}
