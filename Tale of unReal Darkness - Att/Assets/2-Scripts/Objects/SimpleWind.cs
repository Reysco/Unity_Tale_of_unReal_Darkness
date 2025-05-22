using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class SimpleWind : MonoBehaviour
    {
        private Animator anim;
        public Animator feetWindAnim;
        public int contador;
        private BoxCollider2D bc;
        public Player player;

        //public GameObject feetWind;

        void Start()
        {
            anim = GetComponent<Animator>();
            bc = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 11) //layer 11 é o Player
            {
                bc.enabled = false;
                //feetWind.SetActive(true);
                feetWindAnim.SetBool("active", true);
                anim.SetBool("undo", true);
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            StartCoroutine("contagemRegressiva");
        }

        IEnumerator contagemRegressiva()
        {
            yield return new WaitForSeconds(contador); //Escolher tempo para açao
            bc.enabled = true;
            //feetWind.SetActive(false);
            feetWindAnim.SetBool("active", false);
            anim.SetBool("undo", false);
        }
    }
}
