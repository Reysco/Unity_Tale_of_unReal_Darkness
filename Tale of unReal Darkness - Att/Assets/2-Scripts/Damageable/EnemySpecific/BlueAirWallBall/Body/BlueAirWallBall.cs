using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAirWallBall : Entity
{   
    public BAWB_IdleState idleState { get; private set; }
    public BAWB_PlayerDetectedState playerDetectedState { get; private set; }
    public BAWB_DeadState deadState { get; private set; }
    public BAWB_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;

    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Awake()
    {
        base.Awake();

        idleState = new BAWB_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new BAWB_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        deadState = new BAWB_DeadState(this, stateMachine, "dead", deadStateData, this);
        rangedAttackState = new BAWB_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }

    private void HandlePoiseZero()
    {
        //stateMachine.ChangeState(stunState);
    }

    protected override void HandleParry()
    {
        base.HandleParry();
        
       // stateMachine.ChangeState(stunState);
    }

    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);        
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
