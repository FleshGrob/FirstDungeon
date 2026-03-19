using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class DarkFlame : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            FrogProjectile frogProjectile = other.GetComponent<FrogProjectile>();
            if (frogProjectile == null) return;
            frogProjectile.TurnDark();
        }
    }
}
