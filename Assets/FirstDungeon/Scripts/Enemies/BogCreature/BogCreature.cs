using UnityEngine;
using System.Collections;
using FirstDungeon.Scripts.EffectsScripts;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine.AI;

public class BogCreature : Enemy
{
    [SerializeField] BogCreatureConfig _config;
    
    public override EnemyConfig Config => _config;
    

    public override void BasicAttack()
    {
        
    }

    public override void SpecialAttack()
    {
        
    }

    public override void Defense()
    {
        if (IsActing) return;
        SetShielded(true);
    }
}
