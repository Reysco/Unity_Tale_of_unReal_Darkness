using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    [Serializable]
    public class AttackFireDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}