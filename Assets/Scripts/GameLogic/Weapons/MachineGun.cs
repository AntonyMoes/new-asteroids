using System;

namespace GameLogic {
    public class MachineGun : Weapon {
        public MachineGun(float cooldownTime, ISizedTransformProvider weaponWielder,
            Func<ISizedTransformProvider, ITransformProvider> projectileCreator) : base(cooldownTime, weaponWielder,
            projectileCreator) { }
    }
}
