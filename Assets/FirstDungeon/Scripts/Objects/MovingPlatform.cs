using System.Collections.Generic;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class MovingPlatform : MonoBehaviour
    { 
        [SerializeField] float _speed = 1;
        
        Rigidbody2D _rb;
        Collider2D _col;
        PlayerMovement _playerMovement;
        Vector2 _currentTarget;
        List<Vector2> _childrenPositions = new();
        
        public Vector2 PlatformShift { get; private set; }
        

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<Collider2D>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                _childrenPositions.Add(child.position);
                Destroy(child.gameObject);
            }
            
            _currentTarget = _childrenPositions[1];
        }
        
        void FixedUpdate()
        {
            Vector2 targetPosition1 = _childrenPositions[0];
            Vector2 targetPosition2 = _childrenPositions[1];
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
            PlayerMovement playerMovement =  other.GetComponent<PlayerMovement>();
            if (playerMovement == null) return;
            _playerMovement = playerMovement;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (_playerMovement == null) return;

            Vector2 playerPos = other.bounds.center;

            if (!_col.OverlapPoint(playerPos) && _playerMovement.Platform == this)
            {
                _playerMovement.SetPlatform(null);
                return;
            }
            
            if (!_col.OverlapPoint(playerPos)) return;
            
            _playerMovement.SetPlatform(this);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (_playerMovement == null) return;
            if (_playerMovement.gameObject != other.gameObject) return;
            if (_playerMovement.Platform == this)
            {
                _playerMovement.SetPlatform(null);
            } 
            
            _playerMovement = null;
        }
    }
}
