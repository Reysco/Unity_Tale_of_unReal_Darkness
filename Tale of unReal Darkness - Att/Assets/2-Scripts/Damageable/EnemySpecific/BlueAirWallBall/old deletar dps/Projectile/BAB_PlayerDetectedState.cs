using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAB_PlayerDetectedState : PlayerDetectedState {
	private BlueAirBall enemy;

	public BAB_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, BlueAirBall enemy) : base(etity, stateMachine, animBoolName, stateData) {
		this.enemy = enemy;
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();


        if (performCloseRangeAction || performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (!isPlayerInMaxAgroRange && !isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
