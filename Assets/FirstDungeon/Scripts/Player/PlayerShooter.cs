using System;
using System.Collections;
using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerShooter : MonoBehaviour
    {
        enum ShootingState
        {
            Idle,
            Charging,
            Charged
        }
        
        [SerializeField] Projectile _normalProjectile;
        [SerializeField] Projectile _bigProjectile;
        [SerializeField] GameObject _normalBlank;
        [SerializeField] GameObject _bigBlank;
        
        [SerializeField] int _normalDamage;
        [SerializeField] float _projectileSpeed;
        [SerializeField] bool _canShoot;
        [SerializeField] float _chargeTime;

        [SerializeField] int _normalManaCost;
        [SerializeField] int _chargedManaCost;
        
        const float SpawnOffset = 0.6f;
        PlayerMovement _playerMovement;
        PlayerMana _playerMana;
        Coroutine _chargingRoutine;
        ShootingState _state;
        GameObject _blank;

        Vector2 Direction => _playerMovement.Facing;
        Vector2 SpawnPosition => _playerMovement.Rb.position + Direction * SpawnOffset;

        void Awake()
        {
            _playerMana = GetComponent<PlayerMana>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        void Start()
        {
            InputManager.Instance.OnShootKeyPressed += Charge;
            InputManager.Instance.OnShootKeyCanceled += CancelCharge;
        }

        void Update()
        {
            if (_blank != null) _blank.transform.position = SpawnPosition;
        }

        void OnDestroy()
        {
            if (InputManager.Instance == null) return;
            InputManager.Instance.OnShootKeyPressed -= Charge;
            InputManager.Instance.OnShootKeyCanceled -= CancelCharge;
        }

        public void UnlockFrogStaff()
        {
            if (_canShoot) return;

            _canShoot = true;
        }

        void ShootNormal()
        {
            _playerMana.Spend(_normalManaCost);
            Projectile projectile = Instantiate(_normalProjectile, SpawnPosition, Quaternion.identity);
            projectile.Launch(Direction, _projectileSpeed, _normalDamage);
        }

        void ShootCharged()
        {
            _playerMana.Spend(_chargedManaCost);
            Projectile projectile = Instantiate(_bigProjectile, SpawnPosition, Quaternion.identity);
            projectile.Launch(Direction, _projectileSpeed, _normalDamage * 3, () => Player.Instance.Mana.Restore(_chargedManaCost));
        }

        void Charge()
        {
            if (!_canShoot)
                return;

            switch (_playerMana.Has(_chargedManaCost))
            {
                case true:
                    _chargingRoutine = StartCoroutine(ChargingRoutine());
                    break;
                case false:
                    if (_playerMana.Has(_normalManaCost)) ShootNormal();
                    break;
            }
        }

        IEnumerator ChargingRoutine()
        {
            _state = ShootingState.Charging;
            _blank = Instantiate(_normalBlank, SpawnPosition, Quaternion.identity);
            
            float t = 0;

            while (t < _chargeTime)
            {
                t += Time.deltaTime;

                yield return null;
            }

            _state = ShootingState.Charged;
            Destroy(_blank);
            _blank = Instantiate(_bigBlank, SpawnPosition, Quaternion.identity);
            _chargingRoutine = null;
        }

        void CancelCharge()
        {
            if (_chargingRoutine != null) StopCoroutine(_chargingRoutine);
            _chargingRoutine = null;
            Destroy(_blank);
            _blank  = null;
            
            if (PauseManager.Instance.IsPaused)
            {
                _state = ShootingState.Idle;
                return;
            }

            switch (_state)
            {
                case ShootingState.Charging:
                    ShootNormal();
                    break;
                case ShootingState.Charged:
                    ShootCharged();
                    break;
            }

            _state = ShootingState.Idle;
        }
    }
}
