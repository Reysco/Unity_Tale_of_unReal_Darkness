using Bardent.Combat.PoiseDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class PoiseDamageReceiverPlayer : CoreComponent, IPoiseDamageable
    {
        private StatsPlayer stats;

        public Modifiers<Modifier<PoiseDamageData>, PoiseDamageData> Modifiers { get; } = new();

        public void DamagePoise(PoiseDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            stats.Poise.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
        }
    }
}