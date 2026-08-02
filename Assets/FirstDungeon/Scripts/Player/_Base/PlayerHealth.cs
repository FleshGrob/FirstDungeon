using System;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public int MaxHealth { get; private set; } = 6;
        public int CurrentHealth { get; private set; } = 6;
        
        public bool FullHealth => CurrentHealth == MaxHealth;
        
        public event Action OnHealthChanged;
        public event Action OnDamaged;

        public int TakeDamage(Damage damage)
        {
            if (Player.Instance.State.IsInvulnerable) return 0;

            switch (damage.DamageType)
            {
                case Damage.Type.Bog:
                    if (Player.Instance.State.IsInAir) return 0;
                    Player.Instance.Movement.Drown(damage.StunDuration);
                    break;
                
                case Damage.Type.GroundHazard:
                    if (Player.Instance.State.IsInAir) return 0;
                    Player.Instance.Movement.BackToSafe();
                    break;
                
                case Damage.Type.Trap:
                    Player.Instance.Movement.BackToSafe();
                    Player.Instance.FrogShape.StopHooking();
                    break;
            }
            
            CurrentHealth -= damage.Amount;
            if (damage.StunDuration > 0) Player.Instance.State.Stun(damage.StunDuration);

            Player.Instance.State.SetInvulnerable();
            
            OnDamaged?.Invoke();
            OnHealthChanged?.Invoke();
            
            return damage.Amount;
        }

        public void Heal(int hp)
        {
            if (CurrentHealth == MaxHealth)
                return;
            CurrentHealth += hp;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            
            OnHealthChanged?.Invoke();
        }

        public void HealFull()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke();
        }

        public void UpgradeHealth(int hpUpgrade)
        {
            MaxHealth += hpUpgrade;
        }
    }
}
