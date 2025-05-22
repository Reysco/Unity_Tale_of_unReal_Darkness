using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAWB_RangedAttackState : RangedAttackState
{
    private BlueAirWallBall enemy;

    public BAWB_RangedAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, BlueAirWallBall enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
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

            if (!isPlayerInMinAgroRange && !isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.idleState);
            }

            else
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
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
    }
}
    
