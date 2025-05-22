using Bardent.Combat.Damage;
using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Bardent
{
    public class FollowBall : MonoBehaviour
    {
        public GameObject player;

        private Rigidbody2D rb;
        private Animator anim;
        public float speed = 1f;

        //teste
        private ParryReceiver parryReceiver;
        protected Transform attackPosition;
        protected Core core;

        private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
        private CollisionSenses collisionSenses;
        protected D_MeleeAttack stateData;

        public BAB_MeleeAttackState meleeAttackState { get; private set; }
        [SerializeField]
        private D_MeleeAttack meleeAttackStateData;
        [SerializeField]
        private Transform meleeAttackPosition;

        public FiniteStateMachine stateMachine;


        void Start()
        {
            anim= GetComponent<Animator>();
            player = GameObject.Find("Player");
            //parryReceiver = core.GetCoreComponent<ParryReceiver>();

            stateMachine.Initialize(meleeAttackState);
        }

        
        void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*anim.SetTrigger("Explosion");
            speed = 0f;
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject, 0.2f);
            */




            //teste
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

            foreach (Collider2D collider in detectedObjects)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.Damage(new DamageData(stateData.attackDamage, core.Root));
                }
            }
        }

        public void OnDrawGizmos()
        {

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        }

        public void SetParryWindowActive(bool value) => parryReceiver.SetParryColliderActive(value);
    }
}
