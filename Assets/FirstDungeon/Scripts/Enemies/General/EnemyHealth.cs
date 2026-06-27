using System;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    int _hp;
    public event Action OnDeath;
    public event Action OnHurt;

    void Awake()
    {
        _hp = GetComponent<Enemy>().Config.Hp;
    }

    public int TakeDamage(Damage damage)
    {
        _hp -= damage.Amount;
        if (_hp <= 0) OnDeath?.Invoke();
        if (_hp > 0) OnHurt?.Invoke();
        
        return damage.Amount;
    }
}
