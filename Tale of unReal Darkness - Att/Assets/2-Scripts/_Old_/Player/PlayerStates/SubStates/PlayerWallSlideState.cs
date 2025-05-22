using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState {
	public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		if (!isExitingState && wallJumpStatus) { //Caso queira que o player deslize devagar mesmo sem habilidade de wallJump, retire o wallJumpStatus
			Movement?.SetVelocityY(-playerData.wallSlideVelocity);

			if (grabInput && yInput == 0) {
				stateMachine.ChangeState(player.WallGrabState);
			}
		}

		else if(!isExitingState) //fiz isso para que caso o grabState esteja ativo, o player conseguir grudar mesmo deslizando rapido para baixo
		{
            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }

	}
}
