using Bardent.Combat.BleedingDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class BleedingDamageReceiverPlayer : CoreComponent, IBleedingDamageable
    {
        private StatsPlayer stats;

        public Modifiers<Modifier<BleedingDamageData>, BleedingDamageData> Modifiers { get; } = new();

        public void DamageBleeding(BleedingDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            if (data.Amount <= 0f)
            {
                return;
            }

            stats.Bleeding.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
        }
    }
}