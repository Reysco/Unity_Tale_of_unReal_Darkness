using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState {
	public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public bool crouch; //variavel apenas para resolver bug do player agarrar a parede estando agachado e tomar stun
    public bool noCeiling; //variavel apenas para resolver bug do player entrar em estado de stun estando no teto (agachado entre 2 ground)

    public override void Enter() {
		base.Enter();
		player.SetColliderHeight(playerData.crouchColliderHeight);
		crouch = true;


    }

	public override void Exit() {
		base.Exit();
		player.SetColliderHeight(playerData.standColliderHeight);
		crouch = false;
    }

	public override void LogicUpdate() {
		base.LogicUpdate();

        if (isTouchingCeiling) // verificar se esta tocando no teto
        {
            noCeiling = false;
        }
        else if (!isTouchingCeiling) // verificar se esta tocando no teto
        {
            noCeiling = true;
        }

        if (!isExitingState) {
			Movement?.SetVelocityX(playerData.crouchMovementVelocity * Movement.FacingDirection);
			Movement?.CheckIfShouldFlip(xInput);

            if (xInput == 0) {
				stateMachine.ChangeState(player.CrouchIdleState);
            } 
			else if (yInput != -1 && !isTouchingCeiling) {
				stateMachine.ChangeState(player.MoveState);
            }
        }

	}
}
