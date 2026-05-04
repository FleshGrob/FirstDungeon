using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PuzzleScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class TimerTarget : MonoBehaviour
    { 
        TTManager _manager;
        Color _idleColor;
        SpriteRenderer _sr;

        float _timer;
        
        public bool IsActivated { get; private set; }
        
        
        void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _manager = GetComponentInParent<TTManager>();
            _idleColor = _sr.color;
        }

        void Update()
        {
            if (_manager.IsSolved) return;

            if (_timer > 0f) _timer -= Time.deltaTime;
            else IsActivated = false;

            if (!IsActivated) _sr.color = _idleColor;
        }
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
                Hit();
        }

        void Hit()
        {
            if (IsActivated)
                return;
            IsActivated = true;
            _sr.color = _manager.ActiveColor;
            _timer = _manager.FullTimer;

            _manager.OnTargetHit();
        }
    }
}
