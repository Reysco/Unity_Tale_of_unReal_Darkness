using UnityEngine;

namespace Bardent.Combat.FireDamage
{
    public class FireDamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public FireDamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}