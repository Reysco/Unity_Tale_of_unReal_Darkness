using UnityEngine;

namespace Bardent.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;


        //Dropar dinheiro
        [SerializeField] private GameObject coin; // pegar prefab do dindim
        [SerializeField] private int itemMinDrop;
        [SerializeField] private int itemMaxDrop;

        private ParticleManager ParticleManager =>
            particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    
        private ParticleManager particleManager;

        private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private Stats stats;
    
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }


            //Funçao para dropar dindim ao morrer
            int amount = Random.Range(itemMinDrop, itemMaxDrop); //Funçao para randomizar quantidade de dindim dropado por kill

            for (int i = 0; i < amount; i++)
            {
                Instantiate(coin, transform.position, transform.rotation);
            }
            //

            //desabilita tudo do personagem quando ele morre
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