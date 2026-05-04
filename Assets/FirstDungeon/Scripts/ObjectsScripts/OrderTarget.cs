using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PuzzleScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class OrderTarget : MonoBehaviour
    {
        OTManager _manager;
        SpriteRenderer _sr;
        Color _idleColor;


        void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _manager = GetComponentInParent<OTManager>();
            _idleColor = _sr.color;
        }
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
                Hit();
        }

        void Hit()
        {
            _manager.OnTargetHit(this);
        }
        
        public void SetIdle()
        {
            _sr.color = _idleColor;
        }

        public void SetActive()
        {
            _sr.color = _manager.ActiveColor;
        }
    }
}
