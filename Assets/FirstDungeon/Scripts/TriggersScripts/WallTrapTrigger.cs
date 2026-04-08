using FirstDungeon.Scripts.ObjectsScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.TriggersScripts
{
    public class WallTrapTrigger : MonoBehaviour
    {
        WallTrap _wallTrap;
        
        
        void Awake()
        {
            _wallTrap = GetComponentInParent<WallTrap>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerMovement>() == null) return;
            _wallTrap.ShootSpear();
        }
    }
}
