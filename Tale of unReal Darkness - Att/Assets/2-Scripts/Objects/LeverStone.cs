using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class LeverStone : MonoBehaviour
    {

        private Animator anim;
        private DamageReceiverObject damageReceiver;
        public GameObject particle;
        public MovingPlataform movingPlatform;
        public SpriteRenderer sprite;
        public Bobber bobber;
        private bool desactiveCourotine;
        public BoxCollider2D bcCombat;


        void Start()
        {
            anim = GetComponent<Animator>();
            damageReceiver = GetComponentInChildren<DamageReceiverObject>();
            //sprite.color = Color.gray;
        }

        void Update()
        {
            if (damageReceiver.receiveDamage && !desactiveCourotine)
            {
                anim.SetBool("active", true);
                particle.SetActive(true);
                bcCombat.enabled = false;
                StartCoroutine("Active");
            }
        }

        private IEnumerator Active()
        {
            bobber.StartBobbing();
            yield return new WaitForSeconds(1);
            //sprite.color = Color.white;
            movingPlatform.isActivated = true;            
            desactiveCourotine = true;
        }

    }
}
