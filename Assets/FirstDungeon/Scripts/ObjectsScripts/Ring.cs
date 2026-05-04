using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PuzzleScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Ring : MonoBehaviour
    {
        RingManager _manager;
        SpriteRenderer _sr;
        Color _originalColor;

        
        public bool IsActivated { get; private set; }


        void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _manager = GetComponentInParent<RingManager>();
            _originalColor = _sr.color;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
            {
                int frogID = projectile.GetInstanceID();
                Hit(projectile, frogID);
            }
        }

        void Hit(Projectile incoming, int incomingID)
        {
            if (_manager.IsSolved)
                return;
            if (_manager.RightID == 0)
            {
                _manager.GetFrog(incomingID, incoming);
                IsActivated = true;
                _sr.color = _manager.ActiveColor;
            }
            else if (incomingID == _manager.RightID)
            {
                IsActivated = true;
                _sr.color = _manager.ActiveColor;
                _manager.CheckHit();
            }
            else
            {
                _manager.Restart();
            }
        }

        public void Cancel()
        {
            _sr.color = _originalColor;
            IsActivated = false;
        }
    }
}
