using System;

namespace GameLogic {
    public class ShootableLogic<TShootableObj> : IShootable
        where TShootableObj : IPositionVelocityProvider, IDestroyable {
        readonly TShootableObj _obj;
        readonly float _points;

        public ShootableLogic(TShootableObj obj, float points) {
            _obj = obj;
            _points = points;
        }

        public Action<float, IPositionVelocityProvider> OnShot { get; set; }

        public void GetShot() {
            OnShot(_points, _obj);
            _obj.Destroy();
        }
    }
}
