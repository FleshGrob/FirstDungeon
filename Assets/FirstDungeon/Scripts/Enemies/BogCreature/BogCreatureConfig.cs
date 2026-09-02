using UnityEngine;

[CreateAssetMenu(fileName = "BogCreatureConfig", menuName = "Enemies/BogCreature Config")]
public class BogCreatureConfig : EnemyConfig
{
    [Header("Teleport")]
    public int TeleportMaxAttempts;
    public float TeleportCooldown;
    public float TeleportFailCooldown;
}
