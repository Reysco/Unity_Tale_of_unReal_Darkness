using Bardent.Combat.PoisonDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class PoisonDamageReceiverPlayer : CoreComponent, IPoisonDamageable
    {
        private StatsPlayer stats;

        public Modifiers<Modifier<PoisonDamageData>, PoisonDamageData> Modifiers { get; } = new();

        public void DamagePoison(PoisonDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            if (data.Amount <= 0f)
            {
                return;
            }

            stats.Poison.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
        }
    }
}