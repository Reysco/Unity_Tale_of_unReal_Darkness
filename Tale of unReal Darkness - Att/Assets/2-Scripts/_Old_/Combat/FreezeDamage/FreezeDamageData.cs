using UnityEngine;

namespace Bardent.Combat.FreezeDamage
{
    public class FreezeDamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public FreezeDamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}