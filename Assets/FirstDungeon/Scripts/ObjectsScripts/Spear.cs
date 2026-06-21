using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Spear : MonoBehaviour
    {
        const int SpearDamage = 1;
        Rigidbody2D _rb;


        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            
            if (damageable != null) damageable.TakeDamage(SpearDamage);
            Destroy(gameObject);
        }

        public void Launch(float speed, Vector2 direction)
        {
            _rb.linearVelocity = direction.normalized * speed;
        }
    }
}
