using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.EffectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class ExpBarrel : MonoBehaviour, IDamageable
    {
        [SerializeField] Explosion _explosion;
        
        
        public void TakeDamage(int  damage, float stunTime)
        {
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}