using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Barrel : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            FrogProjectile projectile = other.gameObject.GetComponent<FrogProjectile>();
            if (projectile == null) return;
            
            Destroy(gameObject);
        }
    }
}
