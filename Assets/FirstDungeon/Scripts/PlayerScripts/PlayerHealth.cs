using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        int _health = 5;
        int _maxHealth = 5;
    

        public void TakeDamage(int damage, float stunDuration = 0)
        {
            if (PlayerState.Instance.IsInvulnerable) return;
            
            _health -= damage;
            if (stunDuration > 0) 
                PlayerState.Instance.Stun(stunDuration);
            
            if (_health <= 0)
                PlayerState.Instance.Die();

            PlayerState.Instance.SetInvulnerable();
            
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
