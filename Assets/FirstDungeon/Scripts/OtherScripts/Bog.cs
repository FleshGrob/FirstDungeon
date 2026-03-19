using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class Bog : MonoBehaviour
    {
        [SerializeField] int _damage;
        [SerializeField] float _drownTime;
        [SerializeField] float _stunTime;
        
        Collider2D _bogCol;
        PlayerMovement _playerMovement;
        IDamageable _damageable;
        
        
        void Awake()
        {
            _bogCol = GetComponent<Collider2D>();
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
            if (_damageable == null) return;
            
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
