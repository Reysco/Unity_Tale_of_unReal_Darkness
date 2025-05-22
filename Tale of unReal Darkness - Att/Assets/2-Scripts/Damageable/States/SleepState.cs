using System.Collections;
using System.Collections.Generic;
using Bardent.CoreSystem;
using UnityEngine;

public class SleepState : State {
	private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

	private Movement movement;
	private CollisionSenses collisionSenses;

	protected D_SleepState stateData;

	protected bool isSleepTimeOver;
    protected bool isGrounded;
	protected bool isMovementStopped;
	protected bool performCloseRangeAction;
	protected bool isPlayerInMinAgroRange;

	public SleepState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_SleepState stateData) : base(etity, stateMachine, animBoolName) {
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();

		isGrounded = CollisionSenses.Ground;
		performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter() {
		base.Enter();

        isSleepTimeOver = false;
		isMovementStopped = false;
	}

	public override void Exit() {
		base.Exit();
		entity.ResetSleepResistance();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		if (Time.time >= startTime + stateData.sleepTime) {
			isSleepTimeOver = true;
		}

		if (isGrounded && Time.time >= startTime + stateData.sleepKnockbackTime && !isMovementStopped) {
			isMovementStopped = true;
			Movement?.SetVelocityX(0f);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
