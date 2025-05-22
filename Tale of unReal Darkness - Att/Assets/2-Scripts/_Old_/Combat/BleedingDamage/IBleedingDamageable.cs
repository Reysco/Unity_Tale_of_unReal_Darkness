using Bardent.Weapons.Components;

namespace Bardent.Combat.BleedingDamage
{
    public interface IBleedingDamageable
    {
        void DamageBleeding(BleedingDamageData data);
    }
}