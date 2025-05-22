using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class RedEye : Entity
    {
    public RE_IdleState idleState { get; private set; }
    public RE_MoveState moveState { get; private set; }
    public RE_PlayerDetectedState playerDetectedState { get; private set; }
    public RE_ChargeState chargeState { get; private set; }
    public RE_LookForPlayerState lookForPlayerState { get; private set; }
    public RE_MeleeAttackState meleeAttackState { get; private set; }
    public RE_StunState stunState { get; private set; }
    public RE_DeadState deadState { get; private set; }
    public RE_SleepState sleepState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_SleepState sleepStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        moveState = new RE_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new RE_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new RE_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new RE_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new RE_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new RE_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new RE_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new RE_DeadState(this, stateMachine, "dead", deadStateData, this);
        sleepState = new RE_SleepState(this, stateMachine, "sleep", sleepStateData, this);


        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        stats.Sleep.OnCurrentValueZero += HandleSleepZero;
    }

    private void HandlePoiseZero()
    {
        stateMachine.ChangeState(stunState);
    }

    private void HandleSleepZero()
    {
        stateMachine.ChangeState(sleepState);
    }

    protected override void HandleParry()
    {
        base.HandleParry();

        stateMachine.ChangeState(stunState);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
        stats.Sleep.OnCurrentValueZero -= HandleSleepZero;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}