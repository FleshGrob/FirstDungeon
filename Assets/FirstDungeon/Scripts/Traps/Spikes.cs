using System.Collections;
using System.Collections.Generic;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] int _damage;
        [SerializeField] float _stunTime;
        [SerializeField] float _startDelay;
        [SerializeField] float _activeTime;
        [SerializeField] Sprite _activeSprite;
        
        bool _isActive;
        bool _isActivating;
        SpriteRenderer _spriteRenderer;
        Sprite _inactiveSprite;
        List<IDamageable> _damageables = new();


        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _inactiveSprite = _spriteRenderer.sprite;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            if (_damageables.Contains(damageable)) return;
            
            _damageables.Add(damageable);
            
            if (_isActivating) return;
            StartCoroutine(ActivationRoutine());
        }

        void Update()
        {
            if (!_isActive) return;
            
            Damage damage = new Damage
            {
                Amount = _damage,
                StunDuration = _stunTime,
                DamageType = Damage.Type.GroundHazard,
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

        IEnumerator ActivationRoutine()
        {
            _isActivating = true;
            
            yield return new WaitForSeconds(_startDelay);
            
            _isActive = true;
            _spriteRenderer.sprite = _activeSprite;

            yield return new WaitForSeconds(_activeTime);
            
            _isActive = false;
            _isActivating = false;
            _spriteRenderer.sprite = _inactiveSprite;
        }
    }
}
