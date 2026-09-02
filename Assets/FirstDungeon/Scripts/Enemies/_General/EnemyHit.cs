using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] float _stunTime;
    [SerializeField] float _hitDuration;
    

    void OnTriggerStay2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable == null) return;

        Damage damage = new Damage
        {
            Amount = _damage,
            StunDuration =  _stunTime,
        };
        damageable.TakeDamage(damage);
    }
}
