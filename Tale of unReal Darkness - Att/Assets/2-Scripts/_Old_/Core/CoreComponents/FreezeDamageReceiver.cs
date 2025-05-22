using Bardent.Combat.FreezeDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class FreezeDamageReceiver : CoreComponent, IFreezeDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<FreezeDamageData>, FreezeDamageData> Modifiers { get; } = new();

        public void DamageFreeze(FreezeDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            
            stats.Freeze.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}