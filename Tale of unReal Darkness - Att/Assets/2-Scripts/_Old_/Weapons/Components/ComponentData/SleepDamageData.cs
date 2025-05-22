namespace Bardent.Weapons.Components
{
    public class SleepDamageData : ComponentData<AttackSleepDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(SleepDamage);
        }
    }
}