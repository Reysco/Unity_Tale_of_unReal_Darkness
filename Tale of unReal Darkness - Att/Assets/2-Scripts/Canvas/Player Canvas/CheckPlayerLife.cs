using Bardent.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

namespace Bardent
{
    public class CheckPlayerLife : MonoBehaviour
    {
        public LifeBar lifeBar;

        public float maxLife;
        public float currentLife;

        public TMP_Text hpIndicator;

        public HearthDeath hearthDeath;

        void Update()
        {
            maxLife = gameObject.GetComponent<StatsPlayer>().Health.MaxValue; //pegar vida maxima do player
            lifeBar.SetMaxLife(maxLife);

            currentLife = gameObject.GetComponent<StatsPlayer>().Health.CurrentValue; //pegar vida atual do player
            lifeBar.SetCurrentLife(currentLife);

            if(currentLife <= 0)
            {
                lifeBar.SetCurrentLife(0);
                Debug.Log("Morreu");
                hearthDeath.anim.SetBool("death", true);

                //fazer algo aqui depois q morre, chamar cena de morte
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            hpIndicator.SetText($"{currentLife}/{maxLife}"); //Indicador de dano - texto
        }
    }
}
