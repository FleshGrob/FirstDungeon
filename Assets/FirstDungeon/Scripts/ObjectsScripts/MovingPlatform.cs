using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class MovingPlatform : MonoBehaviour
    { 
        [SerializeField] Transform _spot1;
        [SerializeField] Transform _spot2;
        [SerializeField] float _speed = 1;
        
        Rigidbody2D _rb;
        Collider2D _col;
        PlayerMovement _playerMovement;
        Vector2 _currentTarget;
        
        public Vector2 PlatformShift { get;  private set; }
        

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<Collider2D>();
            _currentTarget = _spot2.position;
        }
        
        void FixedUpdate()
        {
            Vector2 targetPosition1 = _spot1.position;
            Vector2 targetPosition2 = _spot2.position;
            Vector2 previousPosition = _rb.position;
            
            if (_rb.position == targetPosition1)
                _currentTarget = targetPosition2;
            else if (_rb.position == targetPosition2)
                _currentTarget = targetPosition1;
            
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, _currentTarget, _speed * Time.fixedDeltaTime);
            
            _rb.MovePosition(newPosition);

            PlatformShift = newPosition - previousPosition;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMovement>() == null) return;
            _playerMovement = other.GetComponent<PlayerMovement>();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (_playerMovement == null) return;

            Vector2 playerPos = other.bounds.center;

            if (!_col.OverlapPoint(playerPos))
            {
                _playerMovement.Platform = null;
                return;
            }

            _playerMovement.Platform = this;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (_playerMovement != other.GetComponent<PlayerMovement>()) return;
            _playerMovement = null;
        }
    }
}
