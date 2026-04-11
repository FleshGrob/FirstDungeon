using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Barrel : MonoBehaviour, IDamageable
    {
        public void TakeDamage(int  damage, float stunTime)
        {
            Destroy(gameObject);
        }
    }
}
