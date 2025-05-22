using System;
using System.Reflection;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bardent.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;      
        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);

                if (currentValue <= 0f)
                {
                    currentValue = 0f;

                    //aqui é onde mata o personagem
                    OnCurrentValueZero?.Invoke();
                }
            }
        }
        
        public float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;
    }
}