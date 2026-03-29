using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Spikes : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerSafeZone"))
                PlayerState.Instance.SetSafe(false);
        
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            IDamageable damageable = other.GetComponent<IDamageable>();
        
            if (playerMovement == null) return;
            if (PlayerState.Instance.IsInvulnerable) return;
        
            damageable.TakeDamage(1);
            playerMovement.BackToSafe();
            PlayerState.Instance.Stun(0.4f);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("PlayerSafeZone"))
                PlayerState.Instance.SetSafe(true);
        }
    }
}
