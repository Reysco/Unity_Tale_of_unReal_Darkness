using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class Coin : MonoBehaviour
    {
        public PlayerCoinCollector playerCoinCollector;
        public SpriteRenderer sprite;
        public Collider2D colliderOfCoin;
        public int amountValue;
        public int amountCoin;

        [Header("Coin Random Splash")]
        public Transform objTrans;
        private float delay = 0;
        private float pastTime = 0;
        private float when = 2.0f;
        private Vector3 off;

        [Header("Magnet")]
        public Rigidbody2D rig;
        public GameObject player;
        private bool magnetize = false;
        public CircleCollider2D cc;

        public int MyCoinValue
        {
            get
            {
                return amountValue;
            }
            set
            {
                amountValue = value;
            }
        }

        private void Awake()
        {
            //Random spawnar esquerda ou direita
            off = new Vector3(Random.Range(-3, 3), off.y, off.z);

        }

        private void Start()
        {
            rig= GetComponent<Rigidbody2D>();
            cc = GetComponent<CircleCollider2D>();

            if(player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            StartCoroutine(Magnet());
        }

        private void Update()
        {
            if(when >= delay)
            {
                pastTime = Time.deltaTime;

                //position of coin
                objTrans.position += off * Time.deltaTime;
                delay += pastTime;
            }

            if (magnetize)
            {
                Vector3 playerPoint = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, -0.3f, 0), 200 * Time.deltaTime);
                rig.MovePosition(playerPoint);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "GoldCollector")
            {

                colliderOfCoin.enabled= false;
                sprite.color = new Color(0f, 50f, 0f, 0f);
                //amountCoin = amountValue;

                StartCoroutine(DestroyObject());
            }
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(1.1f);
            Destroy(this.gameObject);
        }

        private IEnumerator Magnet()
        {
            yield return new WaitForSeconds(2f);
            magnetize = true;
            cc.isTrigger = true;
        }
    }
}
