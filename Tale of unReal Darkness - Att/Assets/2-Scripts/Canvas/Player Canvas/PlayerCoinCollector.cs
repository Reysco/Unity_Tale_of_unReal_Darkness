using Bardent.CoreSystem;
using Bardent.ProjectileSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bardent
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        //text provisorios
        public TextMeshProUGUI provCoin;
        public GameObject provText;
        //text principal   
        public TextMeshProUGUI textCoin;

        //variaveis
        public int coin;
        public int totalCurrentValue;

        private void Start()
        {
            //PlayerPrefs.SetInt("CountCoin", PlayerPrefs.GetInt("CountCoin") * 0); // se multiplicar por 0, ele zera o ouro
            textCoin.text = PlayerPrefs.GetInt("CountCoin").ToString(); //começar o jogo mostrando a quantidade total de ouro coletada
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Coin")
            {
                provText.SetActive(true);
                StopCoroutine("PrevTextVisual");
                coin += 1;
                provCoin.SetText($"+{coin}");
            }
            StartCoroutine("PrevTextVisual");
        }

        IEnumerator CountCoin(int coinNo)
        {
            yield return new WaitForSecondsRealtime(0f);

            var timer = 0f;

            for (int i = 0; i < coinNo; i++)
            {
                PlayerPrefs.SetInt("CountCoin", PlayerPrefs.GetInt("CountCoin") + 1);
                textCoin.text = PlayerPrefs.GetInt("CountCoin").ToString();
                totalCurrentValue += 1; // pegar valor total para usar de referencia apos morrer e perder tudo
                timer += 0.01f;

                yield return new WaitForSecondsRealtime(timer);
            }
        }

        IEnumerator PrevTextVisual()
        {
            yield return new WaitForSeconds(3f);
            provText.SetActive(false);
            StartCoroutine(CountCoin(coin));
            coin = 0;
        }
    }
}
