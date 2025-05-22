using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent.FSM
{
    public class PlayerSlepState : PlayerState
    {
        private readonly Movement movement;

        public PlayerSlepState(
            Player player,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName
        ) : base(player, stateMachine, playerData, animBoolName)
        {
            movement = core.GetCoreComponent<Movement>();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            movement.SetVelocityX(0f);

            if (Time.time >= startTime + playerData.sleepTime)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}