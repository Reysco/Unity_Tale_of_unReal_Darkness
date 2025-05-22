using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSS_StunState : StunState
{
    private BlueStrangeStone enemy;

    public BSS_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, BlueStrangeStone enemy) : base(etity, stateMachine, animBoolName, stateData)
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


        if (isStunTimeOver)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
