using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bardent.Combat.SleepDamage;

namespace Bardent.Weapons.Components
{
    public class SleepDamage : WeaponComponent<SleepDamageData, AttackSleepDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out ISleepDamageable sleepDamageable))
                {
                    sleepDamageable.DamageSleep(new Combat.SleepDamage.SleepDamageData(currentAttackData.Amount, Core.Root));
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
