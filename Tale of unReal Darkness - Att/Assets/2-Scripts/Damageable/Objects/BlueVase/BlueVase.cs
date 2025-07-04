using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BlueVase : Entity
    {
    public BV_IdleState idleState { get; private set; }
    public BV_StunState stunState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_StunState stunStateData;


    public override void Awake()
    {
        base.Awake();

        idleState = new BV_IdleState(this, stateMachine, "idle", idleStateData, this);
        stunState = new BV_StunState(this, stateMachine, "stun", stunStateData, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }

    private void HandlePoiseZero()
    {
        stateMachine.ChangeState(stunState);
    }

    protected override void HandleParry()
    {
        base.HandleParry();

        stateMachine.ChangeState(stunState);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);

    }

    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;

    }
}