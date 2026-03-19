using System;
using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class FrogProjectile : MonoBehaviour
    {
        [SerializeField] Color _darkColor;   
        
        SpriteRenderer _sr;
        float _speed;
        
        public Rigidbody2D Rb { get; private set; }
        public bool IsDark { get; private set; }
        
        public event Action OnDisposed;


        void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<DarkFlame>() != null) return;
            if (other.GetComponent<Mirror>() != null) return;
            if (other.GetComponent<Ring>() != null) return;
            Destroy(gameObject);  
        }
        
        void OnDestroy()
        {
            OnDisposed?.Invoke();
        }

        public void Launch(Vector2 direction, float speed)
        {
            _speed = speed;
            Rb.velocity = direction * speed;
        }

        public void Reflect(Vector2 newDirection)
        {
            Rb.velocity = newDirection * _speed;
        }

        public void TurnDark()
        {
            _sr.color = _darkColor;
            IsDark = true;
        }
    }
}
