using System.Collections.Generic;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

public class FrogPunch : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _punchTime;
    
    readonly List<IDamageable> _damageables = new();

    
    void Start()
    {
        Destroy(gameObject, _punchTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
            
        if (damageable == null) return;
        if (_damageables.Contains(damageable)) return;
            
        _damageables.Add(damageable);

        Damage damage = new Damage
        {
            Amount = _damage,
        };
        damageable.TakeDamage(damage);
    }
}
