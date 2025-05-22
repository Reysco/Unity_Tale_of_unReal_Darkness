using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;

    public float damageHopSpeed = 3f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float sleepResistance = 3f;
    public float sleepRecoveryTime = 2f;

    public float fireResistance = 3f;
    public float fireRecoveryTime = 2f;

    public float poisonResistance = 3f;
    public float poisonRecoveryTime = 2f;

    public float freezeResistance = 3f;
    public float freezeRecoveryTime = 2f;

    public float bleedingResistance = 3f;
    public float bleedingRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
