using Bardent.Combat.FireDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class FireDamageReceiverPlayer : CoreComponent, IFireDamageable
    {
        private StatsPlayer stats;

        public Modifiers<Modifier<FireDamageData>, FireDamageData> Modifiers { get; } = new();

        public void DamageFire(FireDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            if (data.Amount <= 0f)
            {
                return;
            }

            stats.Fire.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
        }
    }
}