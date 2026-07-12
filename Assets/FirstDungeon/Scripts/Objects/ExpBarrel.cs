using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.EffectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class ExpBarrel : MonoBehaviour, IDamageable
    {
        [SerializeField] Explosion _explosion;
        Pullable _pullable;


        void Awake()
        {
            _pullable = GetComponent<Pullable>();
        }

        public int TakeDamage(Damage damage)
        {
            if ((damage.DamageType == Damage.Type.Bog || damage.DamageType == Damage.Type.GroundHazard) && _pullable.IsInAir) return 0;
            
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return damage.Amount;
        }
    }
}