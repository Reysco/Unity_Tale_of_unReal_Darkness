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
    public class EnemyLifeBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxLife(float setMaxLife)
        {
            slider.maxValue = setMaxLife;           
        }

        public void SetCurrentLife(float setCurrentLife)
        {
            slider.value = setCurrentLife;

        }

    }
}
