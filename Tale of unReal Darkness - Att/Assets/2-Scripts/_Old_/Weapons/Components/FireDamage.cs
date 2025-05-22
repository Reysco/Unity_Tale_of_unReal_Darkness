using UnityEngine;
using Bardent.Combat.FireDamage;

namespace Bardent.Weapons.Components
{
    public class FireDamage : WeaponComponent<FireDamageData, AttackFireDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IFireDamageable fireDamageable))
                {
                    fireDamageable.DamageFire(new Combat.FireDamage.FireDamageData(currentAttackData.Amount, Core.Root));
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
