using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Mirror : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            FrogProjectile frogProjectile = other.GetComponent<FrogProjectile>();
            if (frogProjectile != null)
            {
                Vector2 normal = transform.right;
                Vector2 dir = frogProjectile.Rb.velocity.normalized;
                Vector2 newDir = Vector2.Reflect(dir, normal);

                frogProjectile.Reflect(newDir);
            }
        }
    }
}
