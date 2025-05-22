namespace Bardent.Weapons.Components
{
    public class FreezeDamageData : ComponentData<AttackFreezeDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(FreezeDamage);
        }
    }
}