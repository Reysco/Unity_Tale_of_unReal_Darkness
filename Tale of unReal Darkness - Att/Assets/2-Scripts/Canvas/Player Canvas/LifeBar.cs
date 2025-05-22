using Bardent.CoreSystem;
using Bardent.CoreSystem.StatsSystem;
using Bardent.ProjectileSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Bardent
{
    public class LifeBar : MonoBehaviour
    {
        public Slider slider; 
        public Slider yellowSlider;

        public GameObject player;

        private float lerpSpeed = 0.0007f;

        public void SetMaxLife(float setMaxLife)
        {
            slider.maxValue = setMaxLife;

            yellowSlider.maxValue = setMaxLife;

        }

        public void SetCurrentLife(float setCurrentLife)
        {
            slider.value = setCurrentLife;
            yellowSlider.value = Mathf.Lerp(yellowSlider.value, setCurrentLife, lerpSpeed);        
        }

        public void Update()
        {
            if (player.activeSelf == false) // verifica se o gameObject do player está ativo, pois se nao tiver é pq morreu
            {
                yellowSlider.value = Mathf.Lerp(yellowSlider.value, 0, lerpSpeed);
            }
        }





        public void ChangeLife()
        {
            //slider.value = life;
        }

    }
}
