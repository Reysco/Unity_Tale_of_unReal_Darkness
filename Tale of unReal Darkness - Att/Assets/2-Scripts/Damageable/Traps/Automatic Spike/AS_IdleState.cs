using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_IdleState : IdleState
{
    private AutomaticSpike enemy;
    public AS_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, AutomaticSpike enemy) : base(etity, stateMachine, animBoolName, stateData)
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

        
        if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
            
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
