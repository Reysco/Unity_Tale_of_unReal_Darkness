using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bardent.Combat.Damage;
using Bardent.ModifierSystem;


namespace Bardent.CoreSystem
{
    public class DamageReceiverObject : CoreComponent, IDamageable
    {
        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();
        public bool receiveDamage;        

        public void Damage(DamageData data)
        {
            receiveDamage = true;
        }
    }
}
