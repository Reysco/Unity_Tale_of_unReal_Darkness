using Bardent.Combat.FireDamage;
 using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class FireDamageReceiver : CoreComponent, IFireDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<FireDamageData>, FireDamageData> Modifiers { get; } = new();

        public void DamageFire(FireDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            
            stats.Fire.Decrease(data.Amount);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
        }
    }
}