using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackPoisonDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}