using Bardent.Combat.FreezeDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class FreezeDamageReceiverPlayer : CoreComponent, IFreezeDamageable
    {
        private StatsPlayer stats;

        public Modifiers<Modifier<FreezeDamageData>, FreezeDamageData> Modifiers { get; } = new();

        public void DamageFreeze(FreezeDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            if (data.Amount <= 0f)
            {
                return;
            }

            stats.Freeze.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
        }
    }
}