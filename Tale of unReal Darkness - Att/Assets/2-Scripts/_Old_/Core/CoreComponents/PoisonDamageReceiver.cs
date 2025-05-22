using Bardent.Combat.PoisonDamage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class PoisonDamageReceiver : CoreComponent, IPoisonDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<PoisonDamageData>, PoisonDamageData> Modifiers { get; } = new();

        public void DamagePoison(PoisonDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            
            stats.Poison.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}