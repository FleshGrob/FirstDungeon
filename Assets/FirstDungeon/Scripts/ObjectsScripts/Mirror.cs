using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Mirror : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                Vector2 normal = transform.right;
                Vector2 dir = projectile.Rb.linearVelocity.normalized;
                Vector2 newDir = Vector2.Reflect(dir, normal);

                projectile.Reflect(newDir);
            }
        }
    }
}
