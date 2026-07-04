using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        int _health = 6;
        int _maxHealth = 6;
    

        public int TakeDamage(Damage damage)
        {
            if (Player.Instance.State.IsInvulnerable) return 0;
            if (damage.DamageType == Damage.Type.GroundHazard)
            {
                if (Player.Instance.State.IsInAir) return 0;
                Player.Instance.Movement.BackToSafe();
            }
            
            if (damage.DamageType == Damage.Type.Trap)
            {
                Player.Instance.Movement.BackToSafe();
            }
            
            _health -= damage.Amount;
            if (damage.StunDuration > 0) 
                Player.Instance.State.Stun(damage.StunDuration);

            Player.Instance.State.SetInvulnerable();
            
            Debug.Log(_health);
            return damage.Amount;
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
