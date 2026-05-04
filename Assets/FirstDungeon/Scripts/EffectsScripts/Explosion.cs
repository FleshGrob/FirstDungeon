using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;
using System.Collections.Generic;

namespace FirstDungeon.Scripts.EffectsScripts
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] float _stunTime;
        [SerializeField] int _explosionDamage;
        const float ExplosionTime = 0.5f;
        
        readonly List<IDamageable> _damageables = new();
        
        void Start()
        {
            Destroy(gameObject, ExplosionTime);
        }
        
        void OnTriggerStay2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            
            if (damageable == null) return;
            if (_damageables.Contains(damageable)) return;
            
            _damageables.Add(damageable);
            damageable.TakeDamage(_explosionDamage, _stunTime);
        }
    }
}
