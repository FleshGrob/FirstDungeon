using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class Bog : MonoBehaviour
    {
        Collider2D _bogCol;
        PlayerMovement _playerMovement;
        IDamageable _damageable;
        
        readonly int _damage = 1;
        readonly float _drownTime = 1;
        float _stunTime;
        
        
        void Awake()
        {
            _bogCol = GetComponent<Collider2D>();
            _stunTime = _drownTime;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerMovement != null) return;
            
            _playerMovement = other.GetComponent<PlayerMovement>();
            _damageable = other.GetComponent<IDamageable>();
            
            if (_playerMovement == null) return;
            
            PlayerState.Instance.SetInBog(true);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (_playerMovement == null) return;
            
            Vector2 playerPos = _playerMovement.Rb.position;
            
            if (!_bogCol.OverlapPoint(playerPos)) return;
            if (!PlayerState.Instance.InBog) return;
            if (PlayerState.Instance.IsStunned) return;
            
            _damageable.TakeDamage(_damage);
            _playerMovement.Drown(_drownTime);
            PlayerState.Instance.Stun(_stunTime);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMovement>() == null) return;
            
            PlayerState.Instance.SetInBog(false);
            _playerMovement = null;
            _damageable = null;
        }
    }
}
