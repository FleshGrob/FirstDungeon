using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] Projectile _projectilePrefab;
        [SerializeField] float _projectileSpeed = 7;
        [SerializeField] int _projectileDamage = 1;
        [SerializeField] bool _canShoot;
        
        PlayerMovement _playerMovement;
        const float SpawnOffset = 0.6f;


        void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        void Start()
        {
            InputManager.Instance.OnShootKeyPressed += Shoot;
        }

        void OnDestroy()
        {
            if (InputManager.Instance == null) return;
            InputManager.Instance.OnShootKeyPressed -= Shoot;
        }

        public void UnlockFrogStaff()
        {
            if (_canShoot) return;

            _canShoot = true;
        }

        void Shoot()
        {
            if (!_canShoot)
                return;

            Vector2 direction = _playerMovement.Facing;
            Vector2 playerPosition = _playerMovement.Rb.position;
            Vector2 spawnPosition = playerPosition + direction * SpawnOffset;

            Projectile projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);
            projectile.Launch(direction, _projectileSpeed, _projectileDamage);
        }
    }
}
