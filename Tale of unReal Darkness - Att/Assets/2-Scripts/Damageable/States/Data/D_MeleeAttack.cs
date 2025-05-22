using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;

    public float velocityOnAttack = 0f; //para inimigos que se movimentam ao atacar
    public float lastVelocityOnAttack = 0f; //pegar ultima velocidade (para trabalhar com ela em codigos)
    public float jumpHeightOnAttack = 0f; //altura que o inimigo irá pular ao atacar

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 10f;

    public float PoiseDamage;
    public float FireDamage;
    public float PoisonDamage;
    public float FreezeDamage;
    public float SleepDamage;
    public float BleedingDamage;


    public LayerMask whatIsPlayer;
}
