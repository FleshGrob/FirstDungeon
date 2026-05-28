using System.Collections;
using FirstDungeon.Scripts.EffectsScripts;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.AI;

public class Mage : Enemy
{
    [SerializeField] MageConfig _config;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Explosion _explosionPrefab;
    [SerializeField] GameObject _explosionTelegraph;
    
    
    const float SpawnOffset = 1f;
    float _proximityTimer;
    
    public override EnemyConfig Config => _config;
    
    
    protected override void Update()
    {
        base.Update();
        
        if (ShouldEscape())
        {
            Defense();
            _proximityTimer = _config.PlayerProximityCooldown;
        }
        
    }
    
    public override void BasicAttack()
    {
        StartCoroutine(CastingProjectileRoutine());
    }

    public override void SpecialAttack()
    {
        StartCoroutine(CastingExplosionRoutine());
    }

    public override void Defense()
    {
        if (IsActing) return;
        IsActing = true;
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator CastingProjectileRoutine()
    {
        IsActing  = true;
        float t = Config.BasicAttackCastTime;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        LaunchProjectile();
        IsActing = false;
    }
    
    void LaunchProjectile()
    {
        Vector2 direction = ((Vector2)Player.Instance.Transform.position - (Vector2)transform.position).normalized;
        Vector2 spawnPosition = (Vector2)transform.position + direction * SpawnOffset;
        
        Projectile projectile =  Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.Launch(direction, _config.ProjectileSpeed, _config.BasicAttackDamage);
    }
    
    IEnumerator CastingExplosionRoutine()
    {
        IsActing  = true;
        Vector2 targetPosition = Player.Instance.Transform.position;
        GameObject explosionTelegraph = Instantiate(_explosionTelegraph, targetPosition, Quaternion.identity);
        float t = Config.SpecialAttackCastTime;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        Instantiate(_explosionPrefab, targetPosition, Quaternion.identity);
        Destroy(explosionTelegraph);
        IsActing = false;
    }

    IEnumerator TeleportRoutine()
    {
        _sr.enabled = false;
        _col.enabled = false;
        
        yield return new WaitForSeconds(_config.TeleportCastTime);
        
        for (int i = 0; i < _config.TeleportMaxAttempts; i++)
        {
            Vector2 candidate = (Vector2)transform.position + Random.insideUnitCircle * _config.TeleportRadius;
            
            if (!NavMesh.SamplePosition(candidate, out NavMeshHit hit, 0.5f, NavMesh.AllAreas)) continue;
            if (Vector2.Distance(hit.position, Player.Instance.Transform.position) <= _config.PlayerProximityRadius) continue;
            {
                Agent.Warp(hit.position);
                break;
            }
        }
        _sr.enabled = true;
        _col.enabled = true;
        IsActing = false;
    }

    bool ShouldEscape()
    {
        _proximityTimer -= Time.deltaTime;
        
        if (_proximityTimer > 0f) return false;
        
        if (IsActing) return false;
        
        float dist = Vector2.Distance(transform.position, Player.Instance.Transform.position);
        if (dist < Config.PlayerProximityRadius) return true;
        
        return false;
    }
    
    
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        if (_config == null) return;
    
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _config.TeleportRadius);
    }
}
