using Bardent.Combat.SleepDamage;
 using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class SleepDamageReceiverPlayer : CoreComponent, ISleepDamageable
    {
        private StatsPlayer stats;

        public Modifiers<Modifier<SleepDamageData>, SleepDamageData> Modifiers { get; } = new();

        public void DamageSleep(SleepDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            if (data.Amount <= 0f)
            {
                return;
            }

            stats.Sleep.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
        }
    }
}