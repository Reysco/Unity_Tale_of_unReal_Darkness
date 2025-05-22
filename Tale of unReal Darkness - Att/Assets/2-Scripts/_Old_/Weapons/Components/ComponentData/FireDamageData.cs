namespace Bardent.Weapons.Components
{
    public class FireDamageData : ComponentData<AttackFireDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(FireDamage);
        }
    }
}