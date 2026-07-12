using System.Collections.Generic;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class Bog : MonoBehaviour
    {
        Collider2D _bogCol;
        PlayerMovement _playerMovement;
        List<IDamageable> _damageables = new();
        
        readonly int _damage = 1;
        readonly float _drownTime = 1;
        
        
        void Awake()
        {
            _bogCol = GetComponent<Collider2D>();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (_bogCol.OverlapPoint(other.bounds.center))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null && !_damageables.Contains(damageable))
                {
                    _damageables.Add(damageable);
                }
            }
            
            Damage damage = new Damage
            {
                Amount = _damage,
                StunDuration = _drownTime,
                DamageType = Damage.Type.Bog
            };
            
            for (int i = _damageables.Count - 1; i >= 0; i--)
            {
                IDamageable damageable = _damageables[i];
                int takenDamage = damageable.TakeDamage(damage);
                if (takenDamage > 0) _damageables.Remove(damageable);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            
            if (damageable == null) return;
            _damageables.Remove(damageable);
        }
    }
}
