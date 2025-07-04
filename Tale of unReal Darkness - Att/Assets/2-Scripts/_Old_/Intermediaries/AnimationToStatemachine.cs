﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void TriggerChangeVelocityToZero()
    {
        attackState.TriggerChangeVelocityToZero();
    }

    private void TriggerChangeVelocityToBack()
    {
        attackState.TriggerChangeVelocityToBack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    private void SetParryWindowActive(int value)
    {
        attackState.SetParryWindowActive(Convert.ToBoolean(value));
    }
}
