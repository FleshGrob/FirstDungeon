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
        if (other.gameObject.GetComponent<Player>() == null) return;
        if (Player.Instance.State.IsInAir) return;
        if (Player.Instance.State.IsInvulnerable) return; 
        
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        damageable.TakeDamage(_damage, _stunTime);
        Player.Instance.Movement.BackToSafe();
    }
}
