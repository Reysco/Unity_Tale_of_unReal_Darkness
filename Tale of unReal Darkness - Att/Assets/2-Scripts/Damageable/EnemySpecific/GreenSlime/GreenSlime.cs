using Bardent.CoreSystem;
using Bardent.ProjectileSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GreenSlime : Entity
    {
    public GS_IdleState idleState { get; private set; }
    public GS_MoveState moveState { get; private set; }
    public GS_PlayerDetectedState playerDetectedState { get; private set; }
    public GS_ChargeState chargeState { get; private set; }
    public GS_LookForPlayerState lookForPlayerState { get; private set; }
    public GS_MeleeAttackState meleeAttackState { get; private set; }
    public GS_StunState stunState { get; private set; }
    public GS_DeadState deadState { get; private set; }
    public GS_SleepState sleepState { get; private set; }

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

    public Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();

        moveState = new GS_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new GS_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new GS_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new GS_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new GS_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new GS_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new GS_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new GS_DeadState(this, stateMachine, "dead", deadStateData, this);
        sleepState = new GS_SleepState(this, stateMachine, "sleep", sleepStateData, this);


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