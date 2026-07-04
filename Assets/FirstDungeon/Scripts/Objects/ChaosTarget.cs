using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PuzzleScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class ChaosTarget : MonoBehaviour
    {
        ChaosTargetManager _manager;
        SpriteRenderer _spriteRenderer;

        public bool IsActivated { get; private set; }
    
    
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _manager = GetComponentInParent<ChaosTargetManager>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Projectile frog = other.GetComponent<Projectile>();
            if (frog != null && frog.IsDark)
                Hit();
        }

        void Hit()
        {
            IsActivated = true;
            _spriteRenderer.color = _manager.ActiveColor;
            _manager.OnTargetHit();
        }
    }
}
