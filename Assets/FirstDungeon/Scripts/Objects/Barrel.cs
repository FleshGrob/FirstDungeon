using System;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Barrel : MonoBehaviour, IDamageable
    {
        Pullable _pullable;


        void Awake()
        {
            _pullable = GetComponent<Pullable>();
        }

        public int TakeDamage(Damage damage)
        {
            if ((damage.DamageType == Damage.Type.Bog || damage.DamageType == Damage.Type.GroundHazard) && _pullable.IsInAir) return 0;
            
            Destroy(gameObject);
            return damage.Amount;
        }
    }
}
