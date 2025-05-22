using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSleepStateData", menuName = "Data/State Data/Sleep State")]
public class D_SleepState : ScriptableObject
{
    public float sleepTime = 3f;

    public float sleepKnockbackTime = 0.2f;
    public float sleepKnockbackSpeed = 20f;
    public Vector2 sleepKnockbackAngle;

}
