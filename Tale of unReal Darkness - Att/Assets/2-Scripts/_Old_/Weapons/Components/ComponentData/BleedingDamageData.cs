namespace Bardent.Weapons.Components
{
    public class BleedingDamageData : ComponentData<AttackBleedingDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(BleedingDamage);
        }
    }
}