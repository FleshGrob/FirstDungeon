using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

public class BlenderTrap : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _damage;
    [SerializeField] float _stunTime;
    
    Rigidbody2D _rb;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float nextAngle = _rb.rotation + _speed * Time.fixedDeltaTime;
        _rb.MoveRotation(nextAngle);
    }
    
    void OnCollisionStay2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable == null) return;
        
        Damage damage = new Damage
        {
            Amount = _damage,
            StunDuration = _stunTime,
            DamageType = Damage.Type.Trap,
        };
        damageable.TakeDamage(damage);
    }
}
