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

    public void TakeDamage(int damage, float stunTime)
    {
        _hp -= damage;
        if (_hp <= 0) OnDeath?.Invoke();
        OnHurt?.Invoke();
    }
}
