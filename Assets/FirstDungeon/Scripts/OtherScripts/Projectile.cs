using System;
using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.OtherScripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Color _darkColor;  
        
        SpriteRenderer _sr;
        float _speed;
        int _damage;
        
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
            
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.TakeDamage(_damage);
            Destroy(gameObject);  
        }
        
        void OnDestroy()
        {
            OnDisposed?.Invoke();
        }

        public void Launch(Vector2 direction, float speed, int damage)
        {
            _speed = speed;
            _damage = damage;
            Rb.linearVelocity = direction * speed;
        }

        public void Reflect(Vector2 newDirection)
        {
            Rb.linearVelocity = newDirection * _speed;
        }

        public void TurnDark()
        {
            _sr.color = _darkColor;
            IsDark = true;
            _damage *= 2;
        }
    }
}
