using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        int _health = 5;
        int _maxHealth = 5;
    

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
                PlayerState.Instance.Die();
            Debug.Log(_health);
        }

        public void Heal(int hp)
        {
            if (_health == _maxHealth)
                return;
            _health += hp;
            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        public void UpgradeHealth(int hpUpgrade)
        {
            _maxHealth += hpUpgrade;
        }
    }
}
