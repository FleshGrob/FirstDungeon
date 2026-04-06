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
        
        
        void Awake()
        {
            _bogCol = GetComponent<Collider2D>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerMovement != null) return;
            
            _playerMovement = other.GetComponent<PlayerMovement>();
            _damageable = other.GetComponent<IDamageable>();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (_playerMovement == null) return;
            PlayerState.Instance.SetInBog(true);
            
            if (PlayerState.Instance.IsInAir) return;
            
            Vector2 playerPos = other.bounds.center;
            
            if (!_bogCol.OverlapPoint(playerPos)) return;
            if (PlayerState.Instance.IsStunned) return;
            
            _damageable.TakeDamage(_damage);
            _playerMovement.Drown(_drownTime);
            PlayerState.Instance.Stun(_drownTime);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (_playerMovement == null) return;
            if (_playerMovement.gameObject != other.gameObject) return;
            
            PlayerState.Instance.SetInBog(false);
            _playerMovement = null;
            _damageable = null;
        }
    }
}
