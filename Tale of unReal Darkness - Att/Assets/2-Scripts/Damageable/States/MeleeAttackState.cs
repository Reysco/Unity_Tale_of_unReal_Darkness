using System.Collections;
using System.Collections.Generic;
using Bardent.Combat.BleedingDamage;
using Bardent.Combat.Damage;
using Bardent.Combat.FireDamage;
using Bardent.Combat.FreezeDamage;
using Bardent.Combat.KnockBack;
using Bardent.Combat.PoiseDamage;
using Bardent.Combat.PoisonDamage;
using Bardent.Combat.SleepDamage;
using Bardent.CoreSystem;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private float lastVelocity;

    protected D_MeleeAttack stateData;

    public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(stateData.velocityOnAttack * Movement.FacingDirection);
    }

    public override void TriggerChangeVelocityToZero()
    {
        base.TriggerChangeVelocityToZero();
        stateData.velocityOnAttack = 0;
    }

    public override void TriggerChangeVelocityToBack()
    {
        base.TriggerChangeVelocityToBack();
        stateData.velocityOnAttack = stateData.lastVelocityOnAttack;
    }


    public override void TriggerAttack()
    {
        base.TriggerAttack();        

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(new DamageData(stateData.attackDamage, core.Root));
            }

            IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();
            if (knockBackable != null)
            {
                knockBackable.KnockBack(new KnockBackData(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection, core.Root));
            }

            if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.DamagePoise(new PoiseDamageData(stateData.PoiseDamage, core.Root));
            }

            IFireDamageable fireDamageable = collider.GetComponent<IFireDamageable>();
            if (damageable != null)
            {
                fireDamageable.DamageFire(new FireDamageData(stateData.FireDamage, core.Root));
            }

            IPoisonDamageable poisonDamageable = collider.GetComponent<IPoisonDamageable>();
            if (damageable != null)
            {
                poisonDamageable.DamagePoison(new PoisonDamageData(stateData.PoisonDamage, core.Root));
            }

            IFreezeDamageable freezeDamageable = collider.GetComponent<IFreezeDamageable>();
            if (damageable != null)
            {
                freezeDamageable.DamageFreeze(new FreezeDamageData(stateData.FreezeDamage, core.Root));
            }

            ISleepDamageable sleepDamageable = collider.GetComponent<ISleepDamageable>();
            if(damageable != null)
            {
                sleepDamageable.DamageSleep(new SleepDamageData(stateData.SleepDamage, core.Root));
            }

            IBleedingDamageable bleedingDamageable = collider.GetComponent<IBleedingDamageable>();
            if (damageable != null)
            {
                bleedingDamageable.DamageBleeding(new BleedingDamageData(stateData.BleedingDamage, core.Root));
            }

        }
    }
}