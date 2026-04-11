using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] float _stunTime;
        
        const int SpikesDamage = 1;
        
        
        void OnTriggerEnter2D(Collider2D other)
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            IDamageable damageable = other.GetComponent<IDamageable>();
        
            if (playerMovement == null) return;
            if (PlayerState.Instance.IsInAir) return;
            
            damageable.TakeDamage(SpikesDamage, _stunTime);
            playerMovement.BackToSafe();
        }
    }
}
