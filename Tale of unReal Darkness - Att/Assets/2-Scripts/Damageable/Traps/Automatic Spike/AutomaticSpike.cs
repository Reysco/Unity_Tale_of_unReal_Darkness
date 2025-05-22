using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class AutomaticSpike : Entity
    {
    public AS_IdleState idleState { get; private set; }
    public AS_PlayerDetectedState playerDetectedState { get; private set; }
    public AS_MeleeAttackState meleeAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        idleState = new AS_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new AS_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        meleeAttackState = new AS_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    }


    private void Start()
    {
        stateMachine.Initialize(idleState);
    }



    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}