using System;
using System.Collections;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Barrel : MonoBehaviour, IDamageable
    {
        

        public int TakeDamage(Damage damage)
        {
            Destroy(gameObject);
            return damage.Amount;
        }

        
    }
}
