using Bardent.FSM;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bardent.CoreSystem
{
    public class DeathPlayer : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;
        public bool canDeath;

        private ParticleManager ParticleManager =>
            particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    
        private ParticleManager particleManager;

        private StatsPlayer Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private StatsPlayer stats;
    
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }

            if (canDeath)
            {
                //chama fun��o para matar player
                StartCoroutine("contagemRegressiva");
            }
        }


        //essa fun�ao � apenas para dar tempo de a barra de vida atualizar antes de desativar o Player
        IEnumerator contagemRegressiva()
        {
            yield return new WaitForSeconds(0f); //Ou qualquer outro segundo, aparentemente 0 segundos funciona para matar na hora
            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += Die;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }
    }
}