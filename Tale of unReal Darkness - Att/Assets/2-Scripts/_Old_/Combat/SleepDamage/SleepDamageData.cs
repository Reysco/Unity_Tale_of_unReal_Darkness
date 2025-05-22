﻿using UnityEngine;

namespace Bardent.Combat.SleepDamage
{
    public class SleepDamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public SleepDamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}