using FirstDungeon.Scripts.Managers;
using FirstDungeon.Scripts.OtherScripts;
using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class Chest : MonoBehaviour
    {
        PlayerInventory _playerInventory;
        bool _isOpened;
        
        void Start()
        {
            InputManager.Instance.OnActionKeyPressed += Open;
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
            InputManager.Instance.OnActionKeyPressed -= Open;
        }

        void Open()
        {
            if (_isOpened) return;
            if (_playerInventory == null) return;
            
            _isOpened = true;
            _playerInventory.AddKey();

            InputManager.Instance.OnActionKeyPressed -= Open;
            Destroy(gameObject);
        }
    }
}