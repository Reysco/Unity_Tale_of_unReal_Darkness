using UnityEngine;

namespace Bardent.Combat.BleedingDamage
{
    public class BleedingDamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public BleedingDamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}