using System;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float _escapeRadius;
    
    Enemy _enemy;
    int _hp;
    int _escapeAttempts = 10;
   
    public event Action OnDeath;
    public event Action OnHurt;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _hp = _enemy.Config.Hp;
    }

    public int TakeDamage(Damage damage)
    {
        if (damage.DamageType == Damage.Type.GroundHazard || damage.DamageType == Damage.Type.Bog)
        {
            if (_enemy.IsInAir) return 0;
            BackToSafe();
        }

        if (damage.DamageType == Damage.Type.Trap)
        {
            _enemy.CancelPulling();
            BackToSafe();
        }
        
        _hp -= damage.Amount;
        if (_hp <= 0) OnDeath?.Invoke();
        if (_hp > 0) OnHurt?.Invoke();
        
        Debug.Log(_hp);
        return damage.Amount;
    }

    void BackToSafe()
    {
        for (int i = 0; i < _escapeAttempts; i++)
        {
            Vector2 candidate = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * _escapeRadius;
            
            if (!NavMesh.SamplePosition(candidate, out NavMeshHit hit, 1f, NavMesh.AllAreas)) continue;
            if (!_enemy.RoomCol.OverlapPoint(hit.position)) continue;
            _enemy.Agent.Warp(hit.position);
            break;
        }
    }
}
