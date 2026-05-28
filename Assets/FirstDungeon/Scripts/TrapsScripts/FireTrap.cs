using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;
using System.Collections;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class FireTrap : MonoBehaviour
    {
        [SerializeField] LayerMask _stopLayer;
        [SerializeField] float _trapRange;
        [SerializeField] float _stunTime;
        [SerializeField] float _fireTimer;
        [SerializeField] float _startDelay;
    
        const int FireDamage = 1;
        SpriteRenderer _fireRaySr;
        GameObject _fireRay;
        int _playerLayer;
        bool _isBurning;


        void Awake()
        {
            _fireRay =  transform.GetChild(0).gameObject;
            _fireRaySr = _fireRay.GetComponent<SpriteRenderer>();
            _playerLayer =  LayerMask.NameToLayer("Player");
        }

        void OnEnable()
        {
            StartCoroutine(FireRoutine());
        }

        void FixedUpdate()
        {
            if (!_isBurning) return;
            
            RaycastHit2D fireHit = Physics2D.Raycast(_fireRaySr.transform.position, transform.right, _trapRange, _stopLayer);
            Collider2D fireHitCol = fireHit.collider;
            float fireRange = fireHit.distance;
            
            if (fireHitCol == null) 
            {
                _fireRaySr.size = new Vector2(_trapRange, _fireRaySr.size.y); 
                return; 
            }
            
            _fireRaySr.size = new Vector2(fireRange, _fireRaySr.size.y);
            
            IDamageable damageable = fireHitCol.GetComponent<IDamageable>(); 
            
            if (damageable == null) return;
            damageable.TakeDamage(FireDamage, _stunTime);
            
            if (fireHitCol.gameObject.layer == _playerLayer)
            {
                Player.Instance.Movement.BackToSafe();
            }
        }
        
        IEnumerator FireRoutine()
        {
            yield return new WaitForSeconds(_startDelay);
            while (true)
            {
                _isBurning = true;
                _fireRay.SetActive(true);
                yield return new WaitForSeconds(_fireTimer);
                        
                _isBurning = false;
                _fireRay.SetActive(false);
                yield return new WaitForSeconds(_fireTimer);
            }
        }
    }
}   
