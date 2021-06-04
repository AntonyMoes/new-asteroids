using System;
using System.Numerics;

namespace GameLogic {
    public class LaserRayAttenuationLogic<TRay> where TRay : class, IScaleProvider, IDestroyable {
        readonly float _attenuationRate;
        readonly TRay _ray;

        public LaserRayAttenuationLogic(TRay ray, float attenuationTime) {
            _ray = ray;
            _attenuationRate = ray.Scale.X / attenuationTime;
        }

        public void UpdateAttenuation(float deltaTime) {
            var newXScale = Math.Max(_ray.Scale.X - _attenuationRate * deltaTime, 0);
            if (newXScale != 0) {
                _ray.Scale = new Vector2(newXScale, _ray.Scale.Y);
                return;
            }

            _ray.Destroy();
        }
    }
}
