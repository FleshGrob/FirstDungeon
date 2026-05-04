using UnityEngine;

[CreateAssetMenu(fileName = "MageConfig", menuName = "Enemies/Mage Config")]
public class MageConfig : EnemyConfig
{
    [Header("Projectile (Basic Attack)")]
    public float ProjectileSpeed;

    [Header("Explosion (Special Attack)")]
    public float ExplosionStunDuration;

    [Header("Teleport (Defense)")]
    public float TeleportRadius;
    public float TeleportTimerInterval;
    public int TeleportMaxAttempts;
    public float TeleportCastTime;
}