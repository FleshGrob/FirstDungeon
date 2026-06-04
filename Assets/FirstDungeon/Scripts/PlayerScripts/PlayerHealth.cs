using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        int _health = 5;
        int _maxHealth = 5;
    

        public void TakeDamage(int damage, float stunTime = 0)
        {
            if (Player.Instance.State.IsInvulnerable) return;
            
            _health -= damage;
            if (stunTime > 0) 
                Player.Instance.State.Stun(stunTime);
            
            if (_health <= 0)
                Player.Instance.State.Die();

            Player.Instance.State.SetInvulnerable();
            
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
