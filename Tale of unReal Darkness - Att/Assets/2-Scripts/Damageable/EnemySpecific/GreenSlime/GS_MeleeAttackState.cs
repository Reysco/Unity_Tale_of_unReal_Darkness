using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_MeleeAttackState : MeleeAttackState
{
    private GreenSlime enemy;

    public GS_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, GreenSlime enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        //enemy.rb.velocity = Vector2.up * 5;
        //enemy.rb.AddForce(Vector2.up * 500);
        //enemy.rb.AddForce(new Vector2(enemy.distanceFromPlayer, 10), ForceMode2D.Impulse);
    }

    public override void TriggerChangeVelocityToBack()
    {
        base.TriggerChangeVelocityToBack();
        enemy.rb.velocity = Vector2.up * stateData.jumpHeightOnAttack;
    }
}
