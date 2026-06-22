using System;
using System.Collections;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] float _stunTime;
        [SerializeField] float _startDelay;
        [SerializeField] float _activeTime;
        [SerializeField] Sprite _activeSprite;
        
        const int SpikesDamage = 1;
        bool _isActive;
        bool _isActivating;
        PlayerMovement _playerMovement;
        IDamageable _damageable;
        SpriteRenderer _spriteRenderer;
        Sprite _inactiveSprite;


        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _inactiveSprite = _spriteRenderer.sprite;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerMovement != null) return;
            
            _playerMovement = other.GetComponent<PlayerMovement>();
            _damageable = other.GetComponent<IDamageable>();
            

            if (_isActivating) return;
            StartCoroutine(ActivationRoutine());
        }

        void Update()
        {
            if (!_isActive) return;
            if (_playerMovement == null) return;
            if (Player.Instance.State.IsInAir) return;
            if (Player.Instance.State.IsInvulnerable) return; 
            
            Player.Instance.Movement.BackToSafe();
            _damageable.TakeDamage(SpikesDamage, _stunTime);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (_playerMovement == null) return;
            if (_playerMovement.gameObject != other.gameObject) return;
            
            _playerMovement = null;
            _damageable = null;
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
