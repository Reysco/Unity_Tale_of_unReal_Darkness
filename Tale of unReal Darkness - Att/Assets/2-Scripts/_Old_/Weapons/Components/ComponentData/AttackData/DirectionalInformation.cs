﻿using System;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    /*
     * Information for a single region
     */
    [Serializable]
    public class DirectionalInformation
    {
        [Range(-180f, 180f)] public float MinAngle;
        [Range(-180f, 180f)] public float MaxAngle;
        [Range(0f, 1f)] public float DamageAbsorption;
        [Range(0f, 1f)] public float KnockBackAbsorption;
        [Range(0f, 1f)] public float PoiseDamageAbsorption;
        [Range(0f, 1f)] public float FireDamageAbsorption;
        [Range(0f, 1f)] public float BleedingDamageAbsorption;
        [Range(0f, 1f)] public float FreezeDamageAbsorption;
        [Range(0f, 1f)] public float PoisonDamageAbsorption;
        [Range(0f, 1f)] public float SleepDamageAbsorption;


        public bool IsAngleBetween(float angle)
        {
            if (MaxAngle > MinAngle)
            {
                return angle >= MinAngle && angle <= MaxAngle;
            }

            return (angle >= MinAngle && angle <= 180f) || (angle <= MaxAngle && angle >= -180f);
        }
    }
}