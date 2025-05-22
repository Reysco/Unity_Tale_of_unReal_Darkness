using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bardent.Combat.PoisonDamage;

namespace Bardent.Weapons.Components
{
    public class PoisonDamage : WeaponComponent<PoisonDamageData, AttackPoisonDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IPoisonDamageable sleepDamageable))
                {
                    sleepDamageable.DamagePoison(new Combat.PoisonDamage.PoisonDamageData(currentAttackData.Amount, Core.Root));
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
