using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.EffectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class ExpBarrel : MonoBehaviour, IDamageable
    {
        [SerializeField] Explosion _explosion;
        
        
        public int TakeDamage(Damage damage)
        {
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return damage.Amount;
        }
    }
}