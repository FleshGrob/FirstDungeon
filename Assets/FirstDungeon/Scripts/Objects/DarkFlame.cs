using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class DarkFlame : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile == null) return;
            projectile.TurnDark();
        }
    }
}
