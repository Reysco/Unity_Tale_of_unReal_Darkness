using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BV_IdleState : IdleState
{
    private BlueVase enemy;
    public BV_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, BlueVase enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
