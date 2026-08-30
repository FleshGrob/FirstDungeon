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
    
    [Header("Basic Attack")]
    public float BasicAttackCooldown;
    public float BasicAttackCastTime;
    public int BasicAttackDamage;
    public float BasicAttackRange;

    [Header("Special Attack")]
    public float SpecialAttackCooldownMin;
    public float SpecialAttackCooldownMax;
    public float SpecialAttackCastTime;
    public int SpecialAttackDamage;
    public float SpecialAttackRange;

    [Header("Defense")]
    public float DefenseCooldown;
    public float ProjectileDetectRadius;
    public float PlayerProximityRadius;
    public float PlayerProximityCooldown;
    
}