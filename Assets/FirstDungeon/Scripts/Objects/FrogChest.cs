using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class FrogChest : MonoBehaviour
    {
        bool _isOpen;
        PlayerShooter _playerShooter;

        void Start()
        {
            InputManager.Instance.OnInteractKeyPressed += Open;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerShooter != null)
                return;
            _playerShooter = other.GetComponent<PlayerShooter>();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerShooter>() == null) return;

            _playerShooter = null;
        }
        
        void OnDestroy()
        {
            if (InputManager.Instance == null) return;
            InputManager.Instance.OnInteractKeyPressed -= Open;
        }

        void Open()
        {
            if (!Player.Instance.State.CanDo(PlayerState.PlayerAction.Interact)) return;
            
            if (_isOpen) return;
            if (_playerShooter == null) return;

            _isOpen = true;
            _playerShooter.UnlockFrogStaff();

            InputManager.Instance.OnInteractKeyPressed -= Open;
            Destroy(gameObject);
        }
    }
}