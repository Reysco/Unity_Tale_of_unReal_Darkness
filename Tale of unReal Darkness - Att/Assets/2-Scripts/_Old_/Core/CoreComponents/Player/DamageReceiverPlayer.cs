using Bardent.Combat.Damage;
using Bardent.ModifierSystem;

namespace Bardent.CoreSystem
{
    public class DamageReceiverPlayer : CoreComponent, IDamageable
    {
        public float percentageOfDamageFreezed;

        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();

        private StatsPlayer stats;
        private ParticleManager particleManager;


        public void Damage(DamageData data)
        {
            print($"Damage Amount Before Modifiers: {data.Amount}");

            // We must apply the modifiers before we do anything else with data. If there are no modifiers currently active, data will remain the same
            data = Modifiers.ApplyAllModifiers(data);

            print($"Damage Amount After Modifiers: {data.Amount}");

            if (data.Amount <= 0f)
            {
                return;
            }

            if (stats.onFreezed)
            {
                stats.Health.Decrease(data.Amount + (int)(data.Amount * percentageOfDamageFreezed));
                //particleManager.StartWithRandomRotation(damageParticles); // colocar uma particula de gelo
            }
            else
            {
                stats.Health.Decrease(data.Amount);
                //particleManager.StartWithRandomRotation(damageParticles);
            }

        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<StatsPlayer>();
            particleManager = core.GetCoreComponent<ParticleManager>();
        }
    }
}