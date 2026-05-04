using UnityEngine;

public abstract class EnemyConfig : ScriptableObject
{
    [Header("Health")]
    public int Hp;

    [Header("Patrol")]
    public float PatrolRadius;
    public float PatrolIdleTimeMin;
    public float PatrolIdleTimeMax;
    public float PatrolMoveSpeed;

    [Header("Aggro")]
    public float AggroRadius;

    [Header("Chase")]
    public float ChaseMoveSpeed;
    public float AttackRange;

    [Header("Basic Attack")]
    public float BasicAttackCooldown;
    public float BasicAttackCastTime;
    public int BasicAttackDamage;

    [Header("Special Attack")]
    public float SpecialAttackCooldownMin;
    public float SpecialAttackCooldownMax;
    public float SpecialAttackCastTime;
    public int SpecialAttackDamage;

    [Header("Defense")]
    public float DefenseCooldown;
    public float ProjectileDetectRadius;
    public float PlayerProximityRadius;
    public float PlayerProximityCooldown;
    
}