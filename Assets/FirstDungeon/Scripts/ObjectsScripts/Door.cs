using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Door : MonoBehaviour
    {
        PlayerInventory _playerInventory;
        bool _isOpen;

        
        void Start()
        {
            InputManager.Instance.OnActionKeyPressed += TryOpen;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerInventory != null) return;
            _playerInventory = other.GetComponent<PlayerInventory>();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerInventory>() == null) return;

            _playerInventory = null;
        }

        void OnDestroy()
        {
            if (InputManager.Instance == null) return;
            InputManager.Instance.OnActionKeyPressed -= TryOpen;
        }

        void TryOpen()
        {
            if (_isOpen) return;
            if (_playerInventory == null) return;
            if (!_playerInventory.UseKey())  return;
           
            Open();
        }

        void Open()
        {
            _isOpen = true;
            Destroy(gameObject);
        }
    }
}