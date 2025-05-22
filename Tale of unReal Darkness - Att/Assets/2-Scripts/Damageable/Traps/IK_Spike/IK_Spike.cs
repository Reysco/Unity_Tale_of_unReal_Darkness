using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class IK_Spike : Entity
    {
    public IKSS_MeleeAttackState meleeAttackState { get; private set; }

    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        meleeAttackState = new IKSS_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    }


    private void Start()
    {
        stateMachine.Initialize(meleeAttackState);
    }



    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}