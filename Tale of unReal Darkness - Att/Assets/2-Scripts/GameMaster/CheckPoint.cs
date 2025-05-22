using Bardent.CoreSystem;
using Bardent.Interaction;
using Bardent.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class CheckPoint : MonoBehaviour
    {
        private GameMaster gm;

        private BoxCollider2D bc;

        private Animator anim;
        public GameObject checkpoint;
        public GameObject image;


        public bool isColliding;

        void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            anim = GetComponent<Animator>();
            bc = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Interage") && isColliding == true)
            {
                gm.lastCheckPointPos = transform.position;
                gm.lastCheckPointActive = checkpoint;
            }

            if (checkpoint == gm.lastCheckPointActive)
            {
                anim.SetBool("active", true);
                bc.enabled = false;
            }
            else
            {
                anim.SetBool("active", false);
                bc.enabled = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isColliding = true;
                image.SetActive(true);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isColliding = false;
                image.SetActive(false);
            }
        }

    }
}
