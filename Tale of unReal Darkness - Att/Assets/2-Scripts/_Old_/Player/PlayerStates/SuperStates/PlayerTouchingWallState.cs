﻿using System.Collections;
using System.Collections.Generic;
using Bardent.CoreSystem;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState {

	protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

	private Movement movement;
	private CollisionSenses collisionSenses;


	protected bool isGrounded;
	protected bool isTouchingWall;
	protected bool grabInput;
	protected bool jumpInput;
	protected bool isTouchingLedge;
	protected int xInput;
	protected int yInput;

	public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void AnimationFinishTrigger() {
		base.AnimationFinishTrigger();
	}

	public override void AnimationTrigger() {
		base.AnimationTrigger();
	}

	public override void DoChecks() {
		base.DoChecks();

		if (CollisionSenses) {
			isGrounded = CollisionSenses.Ground;
			isTouchingWall = CollisionSenses.WallFront;
			isTouchingLedge = CollisionSenses.LedgeHorizontal;
		}

		if (isTouchingWall && !isTouchingLedge) {
			player.LedgeClimbState.SetDetectedPosition(player.transform.position);
		}
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		xInput = player.InputHandler.NormInputX;
		yInput = player.InputHandler.NormInputY;
		grabInput = player.InputHandler.GrabInput;
		jumpInput = player.InputHandler.JumpInput;

		if (jumpInput && wallJumpStatus) {
			player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
			stateMachine.ChangeState(player.WallJumpState);
		} else if (isGrounded && !grabInput) {
			stateMachine.ChangeState(player.IdleState);
		} else if (!isTouchingWall || (xInput != Movement?.FacingDirection && !grabInput)) {
			stateMachine.ChangeState(player.InAirState);
		} else if (isTouchingWall && !isTouchingLedge && ledgeClimbStatus) {
			stateMachine.ChangeState(player.LedgeClimbState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
