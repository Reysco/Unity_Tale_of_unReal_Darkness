using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bardent
{
    public class PlayerPos : MonoBehaviour //Posição do player
    {
        private GameMaster gm;
        private CheckPlayerLife cpl;
        public float currentLife;

        void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            transform.position = gm.lastCheckPointPos;
        }

        void Update()
        {

            currentLife = gameObject.GetComponentInChildren<StatsPlayer>().Health.CurrentValue; //pegar vida atual do player

            if (currentLife <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
