using UnityEngine;

namespace Bardent.Combat.PoisonDamage
{
    public class PoisonDamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public PoisonDamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}