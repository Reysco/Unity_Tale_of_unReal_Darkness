using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAWB_PlayerDetectedState : PlayerDetectedState
{
    private BlueAirWallBall enemy;

    public BAWB_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, BlueAirWallBall enemy) : base(etity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        stateMachine.ChangeState(enemy.rangedAttackState);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
