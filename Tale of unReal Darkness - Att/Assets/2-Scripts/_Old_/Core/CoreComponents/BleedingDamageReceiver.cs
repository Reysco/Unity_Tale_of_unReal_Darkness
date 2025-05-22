using Bardent.Combat.BleedingDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class BleedingDamageReceiver : CoreComponent, IBleedingDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<BleedingDamageData>, BleedingDamageData> Modifiers { get; } = new();

        public void DamageBleeding(BleedingDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            
            stats.Bleeding.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}