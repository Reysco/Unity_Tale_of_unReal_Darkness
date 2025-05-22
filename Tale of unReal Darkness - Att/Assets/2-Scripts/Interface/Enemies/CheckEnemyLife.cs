using Bardent.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SocialPlatforms.Impl;

namespace Bardent
{
    public class CheckEnemyLife : MonoBehaviour
    {
        public EnemyLifeBar enemyLifeBar;

        public float maxLife;
        public float currentLife;
        public float newCurrentLife;

        public GameObject SlideLifeBar;

        private void Start()
        {
            SlideLifeBar.SetActive(false);
            newCurrentLife = gameObject.GetComponent<Stats>().Health.MaxValue;


        }

        void Update()
        {
            maxLife = gameObject.GetComponent<Stats>().Health.MaxValue; //pegar vida maxima do inimigo
            enemyLifeBar.SetMaxLife(maxLife);


            currentLife = gameObject.GetComponent<Stats>().Health.CurrentValue; //pegar vida atual do inimigo
            enemyLifeBar.SetCurrentLife(currentLife);


            if (currentLife < newCurrentLife) // Sistema para que a barra do inimigo suma após não tomar dano por determinado tempo
            {
                SlideLifeBar.SetActive(true);

                newCurrentLife -= (Time.deltaTime * 10);
                
                if (newCurrentLife <= currentLife)
                {
                    newCurrentLife = currentLife;
                    StartCoroutine("contagemRegressiva");
                }
                else if (newCurrentLife > currentLife)
                {
                    StopCoroutine("contagemRegressiva");
                }

            }

        }

        IEnumerator contagemRegressiva()
        {
            newCurrentLife = currentLife;
            yield return new WaitForSeconds(5f); //Escolher tempo para a barra de vida do inimigo sumir
            SlideLifeBar.SetActive(false);
        }
    }
}
