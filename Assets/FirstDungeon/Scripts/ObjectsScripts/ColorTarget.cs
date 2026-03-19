using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PuzzleScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class ColorTarget : MonoBehaviour
    {
        CTManager _manager;
        SpriteRenderer _spriteRenderer;
        int _colorIndex;

        public bool IsRightColor => _colorIndex == _manager.RightColorIndex;


        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _manager = GetComponentInParent<CTManager>();
            _colorIndex = _manager.CycleColors.Length - 1;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            FrogProjectile frogProjectile = collision.GetComponent<FrogProjectile>();
            if (frogProjectile != null)
                Hit();
        }
        
        void Hit()
        {
            if (_manager.IsSolved)
                return;
            AdvanceColor();
            ApplyColor();
            _manager.OnTargetHit();
        }
        
        void AdvanceColor()
        {
            _colorIndex = (_colorIndex + 1) % _manager.CycleColors.Length;
        }

        void ApplyColor()
        {
            _spriteRenderer.color = _manager.CycleColors[_colorIndex];
        }
    }
}
