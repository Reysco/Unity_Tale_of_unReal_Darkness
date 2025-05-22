using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BlueAirBall : Entity
    {
    public BAB_PlayerDetectedState playerDetectedState { get; private set; }
    public BAB_MeleeAttackState meleeAttackState { get; private set; }
    public BAB_LookForPlayerState lookForPlayerState { get; private set; }

    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    
    public GameObject player;
    private Animator anim;
    public float speed = 1f;
    

    public override void Awake()
    {
        base.Awake();

        playerDetectedState = new BAB_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        meleeAttackState = new BAB_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new BAB_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
    }


    private void Start()
    {
        stateMachine.Initialize(lookForPlayerState);

        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");       
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("explosion");
        speed = 0f;
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject, 0.22f);
    }
}