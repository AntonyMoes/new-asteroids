using System;

namespace GameLogic {
    public interface IShootable {
        Action<float, IPositionVelocityProvider> OnShot { get; set; }
        void GetShot();
    }
}
