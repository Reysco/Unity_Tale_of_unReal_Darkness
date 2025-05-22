using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bardent
{
    public class testCoin : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI Counter;
        [SerializeField] private int CoinNo;


        void Start()
        {
            StartCoroutine(CountCoins(10));
        }


        IEnumerator CountCoins(int coinNo)
        {
            yield return new WaitForSecondsRealtime(0.8f);

            var timer = 0f;

            for (int i = 0; i < coinNo; i++)
            {
                PlayerPrefs.SetInt("CountCoin", PlayerPrefs.GetInt("CountCoin") + coinNo);

                Counter.text = PlayerPrefs.GetInt("CountCoin").ToString();

                timer += 0.05f;

                yield return new WaitForSecondsRealtime(timer);
            }
        }
    }
}
