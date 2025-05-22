using Bardent.ProjectileSystem.Components;

namespace Bardent.Weapons.Components
{
    public class PoisonDamageData : ComponentData<AttackPoisonDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(PoisonDamage);
        }
    }
}