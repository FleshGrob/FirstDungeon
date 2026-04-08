using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.EffectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class ExpBarrel : MonoBehaviour
    {
        [SerializeField] Explosion _explosion;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            FrogProjectile projectile = other.GetComponent<FrogProjectile>();
            if (projectile == null) return;
            
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}