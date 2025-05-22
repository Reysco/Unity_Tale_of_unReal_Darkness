using Bardent.Combat.SleepDamage;
 using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class SleepDamageReceiver : CoreComponent, ISleepDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<SleepDamageData>, SleepDamageData> Modifiers { get; } = new();

        public void DamageSleep(SleepDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            
            stats.Sleep.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}