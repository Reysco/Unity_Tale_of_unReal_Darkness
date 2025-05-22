using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BlueWindBall : Entity
    {
    public BWB_MeleeAttackState meleeAttackState { get; private set; }

    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public GameObject player;
    private Animator anim;
    public float speed = 1f;

    public override void Awake()
    {
        base.Awake();

        meleeAttackState = new BWB_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    }


    private void Start()
    {
        stateMachine.Initialize(meleeAttackState);

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
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        speed = 0f;
        Destroy(gameObject, 0.22f);
    }
}