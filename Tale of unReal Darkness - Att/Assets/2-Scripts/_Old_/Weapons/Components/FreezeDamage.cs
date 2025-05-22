using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bardent.Combat.FreezeDamage;

namespace Bardent.Weapons.Components
{
    public class FreezeDamage : WeaponComponent<FreezeDamageData, AttackFreezeDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IFreezeDamageable freezeDamageable))
                {
                    freezeDamageable.DamageFreeze(new Combat.FreezeDamage.FreezeDamageData(currentAttackData.Amount, Core.Root));
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();

            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }

    }
}
