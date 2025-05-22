using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_PlayerDetectedState : PlayerDetectedState {
	private AutomaticSpike enemy;

	public AS_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, AutomaticSpike enemy) : base(etity, stateMachine, animBoolName, stateData) {
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

		stateMachine.ChangeState(enemy.meleeAttackState);
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
